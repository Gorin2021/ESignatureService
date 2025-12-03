using System.ComponentModel.DataAnnotations;

namespace ESignatureService.Entities;

public class ESignaturePublicCertificate
{
    public int Id { get; set; }
    public long? RTMISCertificateId { get; set; }
    public required string CertificateName { get; set; }
    public required string CertificateSerialNumber { get; set; }
    public int DocumentVersion { get; set; }
    [Range(typeof(DateTime), "1753-01-01", "9999-12-31", ErrorMessage = "Значение для {0} должно быть между {1} и {2}")]
    public DateTime BeginCertificate { get; set; }
    [Range(typeof(DateTime), "1753-01-01", "9999-12-31", ErrorMessage = "Значение для {0} должно быть между {1} и {2}")]
    public DateTime EndCertificate { get; set; }
    [Range(typeof(DateTime), "1753-01-01", "9999-12-31", ErrorMessage = "Значение для {0} должно быть между {1} и {2}")]
    public string? CertificatePublisher { get; set; }
    public string? SignAlgorithm { get; set; }
    public string? PublicKey { get; set;}
    public string? SNILS { get; set; }
    public string? Subject { get; set; }
    [Range(typeof(DateTime), "1753-01-01", "9999-12-31", ErrorMessage = "Значение для {0} должно быть между {1} и {2}")]
    public DateTime? DtInvalidationCertificate { get; set; }
    public ICollection<ESignaturePublicCertificateHistory>? ESignaturePublicCertificateHistories { get; set; }
    public ICollection<ESignaturePublicCertificateIdRequest>? ESignaturePublicCertificateIdRequests { get; set; }

    public void AddToHistory( DateTime created, int UserId)
    {
        AddToHistory(created, UserId, null);
    }

    public void AddToHistory(DateTime created, int UserId, DateTime? dTInvalidation)
    {
        ESignaturePublicCertificateHistories ??= new List<ESignaturePublicCertificateHistory>();
        ESignaturePublicCertificateHistories.Add(new ESignaturePublicCertificateHistory
        {
            Created = created,
            UserId = UserId,
            DtInvalidationCertificate = dTInvalidation ?? DtInvalidationCertificate,
            ESignaturePublicCertificate = this
        });
    }

    public static string GetSNILS(string subject)
    {
        var subjectArr = subject.Split(',')
            .Select(part => part.Trim()).ToArray();
        string? snils = subjectArr.FirstOrDefault(part => part.StartsWith("SNILS=", StringComparison.OrdinalIgnoreCase))
                        ?? subjectArr.FirstOrDefault(part => part.StartsWith("СНИЛС=", StringComparison.OrdinalIgnoreCase));
        return snils?.Substring(6)!;
    }

    public static string GetCN(string subject)
    {
        string cn = subject.Split(',')
            .Select(part => part.Trim())
            .FirstOrDefault(part => part.StartsWith("CN=", StringComparison.OrdinalIgnoreCase))!;
        return cn.Substring(3, cn.Length - 3).Replace("\"\"", "\"");
    }

    public static string GetOGRN(string subject)
    {
        var subjectArr = subject.Split(',')
            .Select(part => part.Trim()).ToArray();
        string? ogrn = subjectArr.FirstOrDefault(part => part.StartsWith("OGRN=", StringComparison.OrdinalIgnoreCase))
                       ?? subjectArr.FirstOrDefault(part => part.StartsWith("ОГРН=", StringComparison.OrdinalIgnoreCase));
        return ogrn?.Substring(5)!;
    }
}