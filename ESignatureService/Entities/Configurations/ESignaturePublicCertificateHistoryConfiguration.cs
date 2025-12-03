using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESignatureService.Entities.Configurations
{
    public class ESignaturePublicCertificateHistoryConfiguration : IEntityTypeConfiguration<ESignaturePublicCertificateHistory>
    {
        public void Configure(EntityTypeBuilder<ESignaturePublicCertificateHistory> builder)
        {
            builder.Property(h => h.DtInvalidationCertificate).HasColumnType("datetime");
            builder.Property(h => h.Created).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");
            builder.HasOne(c => c.ESignaturePublicCertificate).WithMany(c => c.ESignaturePublicCertificateHistories).HasForeignKey(s => s.ESignaturePublicCertificateId);
        }
    }
}
