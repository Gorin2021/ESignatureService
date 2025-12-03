namespace ESignatureService.Entities
{
    public class ESignaturePublicCertificateIdResponse
    {
        public int Id { get; set; }
        public int ESignaturePublicCertificateIdRequestId { get; set; }
        public int? ErrorCode { get; set; }
        public string? ErrorMessage { get; set; }
        public string? RowCount { get; set; }
        public string? Addition { get; set; }
        public long? ResponseDataId { get; set; }
        public bool? Success { get; set; }
        public required string ResponseJSON { get; set; }
        public required DateTime Created { get; set; }
        public ESignaturePublicCertificateIdRequest? ESignaturePublicCertificateIdRequest { get; set; }
        public ICollection<ESignaturePublicCertificateIdResponseError>? ESignaturePublicCertificateIdResponseErrors { get; set; }
    }
}
