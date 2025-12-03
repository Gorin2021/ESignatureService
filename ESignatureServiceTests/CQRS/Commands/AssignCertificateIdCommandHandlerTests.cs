using ESignatureService.Common;
using ESignatureService.CQRS.Commands;
using ESignatureService.Entities;
using Newtonsoft.Json.Linq;

namespace ESignatureServiceTests.CQRS.Commands
{
    [TestClass()]
    public class AssignCertificateIdCommandHandlerTests
    {
        private int errorCode = 200;
        private string errorMessage = "OK";
        private string rowCount = "1";
        private string addition = "Дополнительная информация";
        private long id = 12345;
        private bool? success = true;

        [TestMethod()]
        public void MapResponseTest()
        {
            //Arrange
            var jsonStr = CreateExpectedResponseJson();

            //Action
            ESignaturePublicCertificateIdResponse result = AssignCertificateIdCommandHandler.MapResponse(jsonStr, 1, new SystemDateTimeProvider());

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.ErrorCode, errorCode);
            Assert.AreEqual(result.Addition, addition);
            Assert.AreEqual(result.ErrorMessage, errorMessage);
            Assert.AreEqual(result.RowCount, rowCount);
            Assert.AreEqual(result.ResponseDataId, id);
            Assert.AreEqual(result.Success, success);
        }

        private string CreateExpectedResponseJson()
        {
            var jObject = JObject.FromObject(new
            {
                errorCode,
                errorMessage ,
                rowCount,
                addition,
                responseData = JObject.FromObject(new { id }),
                errors = (JArray)null!,
                success
            });
            return jObject.ToString();
        }
    }
}