namespace ESignatureService.Common
{
    public class Constant
    {
        public const string DEPENDENCY_RESOLVER_ERROR = "Произошла ошибка при созданни объекта в контейнере DI";
        public const string INVALID_CERTIFICATE_FILE = "Некорректный файл сертификата.";
        public const string CERTIFICATE_EXIST_IN_REGISTRY = "Такой сертификат существует в реестре";
        public const string INVALID_SNILS = "Некорректный снилс.";
        public const string CERTIFICATE_NOT_FOUND = "Сертификат не найден.";
        public const string INVALID_ID = "Id не можит быть равным или меннее 0";
        public const string NULL_REFERENCE_EXCEPTION = "Сылка не указывает на объект";
        public const string ERROR_WHEN_GET_CERTIFICATE_FOR_ASSIGN_ID =
            "При извлечении сертификата, для получения ID, произошла ошибка";
        public const string ERROR_SAVE_IN_METHOD = "При сохранении ошибки произошла ошибка в методе";
    }
}
