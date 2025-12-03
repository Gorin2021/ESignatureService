using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESignatureService.Entities.Configurations
{
    public class ESignaturePublicCertificateConfiguration: IEntityTypeConfiguration<ESignaturePublicCertificate>
    {
        public void Configure(EntityTypeBuilder<ESignaturePublicCertificate> builder)
        {
            builder.Property(s => s.BeginCertificate).IsRequired().HasColumnType("datetime"); 
            builder.Property(s => s.EndCertificate).IsRequired().HasColumnType("datetime");
            builder.Property(s => s.PublicKey).HasColumnName("OpenKey");
            builder.ToTable("ESignaturePublicCertificateRegistry", "dbo", s => s.ExcludeFromMigrations());
        }
    }
}
