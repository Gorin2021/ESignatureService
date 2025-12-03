using System.ComponentModel.DataAnnotations;
using ESignatureService.Common;
using ESignatureService.Extensions;
using Microsoft.AspNetCore.Diagnostics;

namespace ESignatureService.Entities;

public class ESignaturePublicCertificateErrorLog
{

    public int Id { get; set; }
    public string? URI { get; set; }
    public string? Message { get; set; }
    public string? StackTrace { get; set; }
    [Range(typeof(DateTime), "1753-01-01", "9999-12-31", ErrorMessage = "Значение для {0} должно быть между {1} и {2}")]
    public DateTime Created { get; set; } = DateTime.Now;
    //Это исключение. Не использовать связь с контекстом в других сущностях.
    public async Task AddAsync(IExceptionHandlerFeature? exceptionHandlerFeature, SNSIDbContext dbContext)
    {
        dbContext.DetachAllEntities();
        if (exceptionHandlerFeature != null)
        {
            URI = exceptionHandlerFeature.Endpoint?.DisplayName;
            Message = exceptionHandlerFeature.Error.GetAllMessages();
            StackTrace = exceptionHandlerFeature.Error.StackTrace;
        }
        else
        {
            URI = nameof(ESignaturePublicCertificateErrorLog);
            Message = $"{Constant.ERROR_SAVE_IN_METHOD} {nameof(AddAsync)}";
            StackTrace = exceptionHandlerFeature?.Error.StackTrace;
        }
        await dbContext.AddAsync(this);
        await dbContext.SaveChangesAsync();
    }
}