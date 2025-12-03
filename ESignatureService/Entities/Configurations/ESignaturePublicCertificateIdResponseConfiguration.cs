using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESignatureService.Entities.Configurations
{
    public class ESignaturePublicCertificateIdResponseConfiguration: IEntityTypeConfiguration<ESignaturePublicCertificateIdResponse>
    {
        public void Configure(EntityTypeBuilder<ESignaturePublicCertificateIdResponse> builder)
        {
            builder.Property(r => r.Created).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");
        }
    }
}
