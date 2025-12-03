using ESignatureService.Common;
using ESignatureService.Entities;
using ESignatureService.Interfaces;
using ESignatureService.Interfaces.CQRS.Commands;
using ESignatureService.Interfaces.Services;
using ESignatureService.Models.ESignaturePublicCertificateIdResponseJson;
using Newtonsoft.Json;

namespace ESignatureService.CQRS.Commands;

public class AssignCertificateIdCommandHandler(IECPService ecpService, SNSIDbContext dbContext, IConfiguration configuration, IDateTimeProvider dateTimeProvider)
    : ICommandHandler<AssignCertificateIdCommand>
{
    public async Task ExecuteAsync(AssignCertificateIdCommand command)
    {
        ESignaturePublicCertificate? eSignaturePublicCertificate =
            await dbContext.ESignaturePublicCertificates.FindAsync(command.ESignatureCertificateId);
        if (eSignaturePublicCertificate == null)
        {
            throw new NullReferenceException(
                $"{Constant.ERROR_WHEN_GET_CERTIFICATE_FOR_ASSIGN_ID}: {Constant.CERTIFICATE_NOT_FOUND}");
        }

        string? emdUrl = configuration["EMDCertificateServiceAddress"];
        string requestJson = ecpService.CreateJson(eSignaturePublicCertificate);
        await SaveRequest(requestJson, eSignaturePublicCertificate.Id, command.UserId);//проверяем логирование запроса, если ошибка, то ошибку обработает верхний уровень (контроллер error). 

        HttpResponseMessage response =
            await ecpService.SendRequestCertificateId((requestJson), emdUrl!);
        ESignaturePublicCertificateIdResponse responseEntity =
            MapResponse(await response.Content.ReadAsStringAsync(), eSignaturePublicCertificate.Id,
                dateTimeProvider);
        dbContext.ESignatureCertificateIdResponses.Add(responseEntity);
        if (responseEntity.ESignaturePublicCertificateIdResponseErrors == null ||
            !responseEntity.ESignaturePublicCertificateIdResponseErrors.Any() && responseEntity.ResponseDataId != null)
        {
            eSignaturePublicCertificate.RTMISCertificateId = responseEntity.ResponseDataId!;
            dbContext.ESignaturePublicCertificateHistories.Add(new ESignaturePublicCertificateHistory
            {
                Created = dateTimeProvider.Now,
                UserId = command.UserId,
                DtInvalidationCertificate = eSignaturePublicCertificate.DtInvalidationCertificate,
                RTMISCertificateId = eSignaturePublicCertificate.RTMISCertificateId,
                ESignaturePublicCertificate = eSignaturePublicCertificate
            });
        }

        await dbContext.SaveChangesAsync();
    }

    public static ESignaturePublicCertificateIdResponse MapResponse(string responseJson, int requestId, IDateTimeProvider dateTimeProvider)
        {
            ESignaturePublicCertificateIdResponseModel model;
            ICollection<ESignaturePublicCertificateIdResponseError>? eSignaturePublicCertificateIdResponseErrors = null;
        try
            {
                model =
                    JsonConvert.DeserializeObject<ESignaturePublicCertificateIdResponseModel>(responseJson)!;

                if (model.Errors is {Count: > 0})
                {
                    eSignaturePublicCertificateIdResponseErrors =
                        new List<ESignaturePublicCertificateIdResponseError>();
                    foreach (var error in model.Errors)
                    {
                        eSignaturePublicCertificateIdResponseErrors.Add(new ESignaturePublicCertificateIdResponseError
                        {
                            ErrorCode = error.ErrorCode,
                            Message = error.Message
                        });
                    }
                }
            }
            catch
            {
                return new ESignaturePublicCertificateIdResponse
                {
                    ResponseJSON = responseJson,
                    Created = dateTimeProvider.Now
                };
            }

            return new ESignaturePublicCertificateIdResponse
            {
                ESignaturePublicCertificateIdRequestId = requestId,
                Created = dateTimeProvider.Now,
                ResponseJSON = responseJson,
                Addition = model.Addition,
                ErrorCode = model.ErrorCode,
                ErrorMessage = model.ErrorMessage,
                ResponseDataId = model.ResponseData?.Id,
                RowCount = model.RowCount,
                Success = model.Success,
                ESignaturePublicCertificateIdResponseErrors = eSignaturePublicCertificateIdResponseErrors
            };
        }

        private  Task SaveRequest(string certificateJson, int eSignaturePublicCertificateId, int userId)
        {
            var request = new ESignaturePublicCertificateIdRequest
            {
                CertificateJSON = certificateJson,
                Created = dateTimeProvider.Now,
                ESignaturePublicCertificateId = eSignaturePublicCertificateId,
                UserId = userId
            };
            dbContext.ESignatureCertificateIdRequests.Add(request);
            return dbContext.SaveChangesAsync();//Логируем запрос сразу, во избежании ошибок и утери истории
    }
}