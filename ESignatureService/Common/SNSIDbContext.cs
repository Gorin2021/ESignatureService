using ESignatureService.Entities;
using ESignatureService.Entities.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ESignatureService.Common;

public sealed class SNSIDbContext(DbContextOptions<SNSIDbContext> options) : DbContext(options)
{
    public required DbSet<ESignaturePublicCertificate> ESignaturePublicCertificates { get; set; }
    public required DbSet<ESignaturePublicCertificateErrorLog> ESignaturePublicCertificateErrorLogs { get; set; }
    public required DbSet<ESignaturePublicCertificateHistory> ESignaturePublicCertificateHistories { get; set; }
    public required DbSet<ESignaturePublicCertificateIdRequest> ESignatureCertificateIdRequests { get; set; }
    public required DbSet<ESignaturePublicCertificateIdResponse> ESignatureCertificateIdResponses { get; set; }
    public required DbSet<ESignaturePublicCertificateIdResponseError> ESignatureCertificateIdResponseErrors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ESignaturePublicCertificateConfiguration());
        modelBuilder.ApplyConfiguration(new ESignaturePublicCertificateHistoryConfiguration());
        modelBuilder.ApplyConfiguration(new ESignaturePublicCertificateIdRequestConfiguration());
        modelBuilder.ApplyConfiguration(new ESignaturePublicCertificateIdResponseConfiguration());
    }
    public void DetachAllEntities()
    {
        var entriesCopy = ChangeTracker.Entries()
            .Where(e => e.State != EntityState.Detached)
            .ToList();

        foreach (var entry in entriesCopy)
            entry.State = EntityState.Detached;
    }
}