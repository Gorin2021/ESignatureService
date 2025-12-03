using ESignatureService.Interfaces;

namespace ESignatureService.Common
{
    public class SystemDateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
    }
}
