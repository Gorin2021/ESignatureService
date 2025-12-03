namespace ESignatureService.Models.ESignaturePublicCertificateIdResponseJson
{
    public class ESignaturePublicCertificateIdResponseModel
    {
        public int ErrorCode { get; set; } // Код ответа
        public string? ErrorMessage { get; set; } // Статус ответа
        public string? RowCount { get; set; } // Количество записей
        public string? Addition { get; set; } // Дополнительная информация
        public ResponseData? ResponseData { get; set; } // Ответ при успешном выполнении
        public List<Error>? Errors { get; set; } // Список ошибок
        public bool? Success { get; set; } // Признак успешного выполнения
    }
}
