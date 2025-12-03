namespace ESignatureService.Entities
{
    public class ESignaturePublicCertificateIdResponseError
    {
        public int Id { get; set; }
        public int ESignaturePublicCertificateIdResponseId { get; set; }
        public string? ErrorCode { get; set; }
        public string? Message { get; set; }

        public ESignaturePublicCertificateIdResponse? ESignaturePublicCertificateIdResponse { get; set; }
    }
}
