using ESignatureService.Common;
using ESignatureService.Interfaces.CQRS.Queries;
using Microsoft.EntityFrameworkCore;

namespace ESignatureService.CQRS.Queries
{
    public class GetRTMISCertificateIdBySNILSOrIdQueryHandler(SNSIDbContext dbContext) : IQueryHandler<GetRTMISCertificateIdBySNILSOrIdQuery, long?>
    {
        public async Task<long?> ExecuteAsync(GetRTMISCertificateIdBySNILSOrIdQuery query)
        {
            var cer = dbContext.ESignaturePublicCertificates.AsQueryable().AsNoTracking();

            if (query.Id != null)
                cer = cer.Where(w => w.Id == query.Id);
            else if (query.Snils != null)
                cer = cer.Where(w => w.SNILS == query.Snils);
            else return null;

            return (await cer.FirstOrDefaultAsync())?.RTMISCertificateId;
        }
    }
}
