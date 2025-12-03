namespace ESignatureService.Entities;

public class ESignaturePublicCertificateIdRequest
{
    public int Id { get; set; }
    public int ESignaturePublicCertificateId { get; set; }
    public int UserId { get; set; }
    public required string CertificateJSON { get; set; }
    public required DateTime Created { get; set; }
    public ESignaturePublicCertificate? ESignaturePublicCertificate { get; set; }
    public ICollection<ESignaturePublicCertificateIdResponse>? ESignaturePublicCertificateIdResponse { get; set; }
}