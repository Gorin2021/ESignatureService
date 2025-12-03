using ESignatureService.Entities;

namespace ESignatureServiceTests.Entities
{
    [TestClass()]
    public class ESignaturePublicCertificateTests
    {
        [TestMethod()]
        public void GetCNTest()
        {
            //Arrange
            string subject = "CN=МИНИСТЕРСТВО ЗДРАВООХРАНЕНИЯ МОСКОВСКОЙ ОБЛАСТИ, SN=ИВАНОВ, G=ИВАН МИХАЙЛОВИЧ, C=RU, S=50 Московская область, L=Красногорск, O=МИНИСТЕРСТВО ЗДРАВООХРАНЕНИЯ МОСКОВСКОЙ ОБЛАСТИ, T=Врач-терапевт, SNILS=02755123433, OGRN=1037700260222";
            //Action
            string result = ESignaturePublicCertificate.GetCN(subject);
            //Assert
            Assert.AreEqual(result, "МИНИСТЕРСТВО ЗДРАВООХРАНЕНИЯ МОСКОВСКОЙ ОБЛАСТИ");
        }
    }
}