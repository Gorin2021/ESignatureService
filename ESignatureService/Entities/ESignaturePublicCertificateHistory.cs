using System.ComponentModel.DataAnnotations;

namespace ESignatureService.Entities
{
    public class ESignaturePublicCertificateHistory
    {
        public int Id { get; set; }
        public int ESignaturePublicCertificateId { get; set; }
        /// <summary>
        /// ESignaturePublicCertificate.DtInvalidationCertificate
        /// </summary>
        /// [Range(typeof(DateTime), "1753-01-01", "9999-12-31", ErrorMessage = "Значение для {0} должно быть между {1} и {2}")]
        public DateTime? DtInvalidationCertificate { get; set; }
        public long? RTMISCertificateId { get; set; }
        public int UserId { get; set; }
        [Range(typeof(DateTime), "1753-01-01", "9999-12-31", ErrorMessage = "Значение для {0} должно быть между {1} и {2}")]
        public DateTime Created { get; set; } = DateTime.Now;
        [Range(typeof(DateTime), "1753-01-01", "9999-12-31", ErrorMessage = "Значение для {0} должно быть между {1} и {2}")]
        public required ESignaturePublicCertificate ESignaturePublicCertificate { get; set; }
    }
}
