using ESignatureService.Common;
using ESignatureService.Entities;
using ESignatureService.Interfaces;
using ESignatureService.Interfaces.CQRS.Queries;
using ESignatureService.Models;
using Microsoft.EntityFrameworkCore;

namespace ESignatureService.CQRS.Queries;

public class GetCertificatesBySnilsQueryHandler(SNSIDbContext dbContext, IDateTimeProvider systemDateTimeProvider) : IQueryHandler<GetCertificatesBySnilsQuery, IEnumerable<PublicCertificateViewModel>>
{
    public async Task<IEnumerable<PublicCertificateViewModel>> ExecuteAsync(GetCertificatesBySnilsQuery query)
    {
        var eSignaturePublicCertificates = query.IsValid switch
        {
            true => dbContext.ESignaturePublicCertificates
                .Where(s => s.SNILS == query.Snils && s.DtInvalidationCertificate == null && s.EndCertificate > systemDateTimeProvider.Now)
                ,
            false => dbContext.ESignaturePublicCertificates
                .Where(s => s.SNILS == query.Snils && s.DtInvalidationCertificate != null || s.SNILS == query.Snils && s.EndCertificate <= systemDateTimeProvider.Now),

            _ =>  dbContext.ESignaturePublicCertificates.Where(s => s.SNILS == query.Snils)
                
        };
        return CreatePublicCertificateViewModels(await eSignaturePublicCertificates.AsNoTracking().ToArrayAsync());
    }

    private List<PublicCertificateViewModel> CreatePublicCertificateViewModels(IEnumerable<ESignaturePublicCertificate> eSignaturePublicCertificateDtoCollection)
    {
        var publicCertificates = new List<PublicCertificateViewModel>();
        foreach (var eSignaturePublicCertificateDto in eSignaturePublicCertificateDtoCollection)
        {
            publicCertificates.Add(new PublicCertificateViewModel
            {
                BeginCertificate = eSignaturePublicCertificateDto.BeginCertificate,
                EndCertificate = eSignaturePublicCertificateDto.EndCertificate,
                CertificateName = eSignaturePublicCertificateDto.CertificateName,
                CertificateSerialNumber = eSignaturePublicCertificateDto.CertificateSerialNumber,
                Id = eSignaturePublicCertificateDto.Id,
                DtInvalidationCertificate = eSignaturePublicCertificateDto.DtInvalidationCertificate
            });
        }

        return publicCertificates;
    }
}