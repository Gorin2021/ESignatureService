using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESignatureService.Entities.Configurations
{
    public class ESignaturePublicCertificateIdRequestConfiguration : IEntityTypeConfiguration<ESignaturePublicCertificateIdRequest>
    {
        public void Configure(EntityTypeBuilder<ESignaturePublicCertificateIdRequest> builder)
        {
            builder.Property(r => r.Created).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");
        }
    }
}
