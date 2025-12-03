namespace ESignatureService.Models
{
    public class PublicCertificateViewModel
    {
        public int Id { get; set; }
        public required string CertificateName { get; set; }
        public required string CertificateSerialNumber { get; set; }
        public DateTime BeginCertificate { get; set; }
        public DateTime EndCertificate { get; set; }
        public DateTime? InvalidationCertificate { get; set; }
        public DateTime? DtInvalidationCertificate { get; set; }
    }
}
