using ESignatureService.Entities;
using ESignatureService.Services;
using Moq;
using Newtonsoft.Json.Linq;
using System.Net;
using ESignatureService.Interfaces.Services;

namespace ESignatureServiceTests.Services;

[TestClass]
public class EcpServiceTests
{
    private Mock<IHttpClientService> _httpClientServiceMock = new();//Инициализируем для удаления предупреждения компилятора.
    [TestInitialize]
    public void Setup()
    {
        _httpClientServiceMock = new Mock<IHttpClientService>();
    }

    [TestMethod]
    public void CreateJsonTest()
    {
        // Arrange
        var certificate = CreateCertificate();

        var expectedJson = CreateExpectedJson(certificate);
        IECPService ecpService = new EcpService(_httpClientServiceMock.Object);

        // Act
        var jsonResult = ecpService.CreateJson(certificate);

        // Assert
        Assert.AreEqual(expectedJson, jsonResult);
    }

    [TestMethod]
    public async Task SendRequestCertificateIdSuccessTest()
    {
        // Arrange
        var jsonCertificate = "jsonCertificate";
        var emdUrl = "http://example.com";
        var responseContent = CreateExpectedResponseJson();
        var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
        httpResponseMessage.Content = new StringContent(responseContent);

        _httpClientServiceMock.Setup(x => x.SendRequestAsync(HttpMethod.Post, emdUrl, It.IsAny<StringContent>()))
            .ReturnsAsync(httpResponseMessage);

        IECPService ecpService = new EcpService(_httpClientServiceMock.Object);

        // Act
        var result = await ecpService.SendRequestCertificateId(jsonCertificate, emdUrl);

        // Assert
        var contentResult = await result.Content.ReadAsStringAsync();
        Assert.AreEqual(contentResult, responseContent);
    }

    [TestMethod]
    public async Task SendRequestCertificateIdFailureTest()
    {
        // Arrange
        var jsonCertificate = CreateExpectedJson(CreateCertificate());
        var emdUrl = "http://example.com";
        var responseContent = CreateExpectedErrorResponseJson();
        var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
        httpResponseMessage.Content = new StringContent(responseContent);

        _httpClientServiceMock.Setup(x => x.SendRequestAsync(HttpMethod.Post, emdUrl, It.IsAny<StringContent>()))
            .ReturnsAsync(httpResponseMessage);

        IECPService ecpService = new EcpService(_httpClientServiceMock.Object);

        // Action & Assert
        await Assert.ThrowsExceptionAsync<Exception>(() => ecpService.SendRequestCertificateId(jsonCertificate, emdUrl));
    }

    [TestMethod]
    public async Task SendRequestCertificateIdErrorsTest()
    {
        // Arrange
        var jsonCertificate = CreateExpectedJson(CreateCertificate());
        var emdUrl = "http://example.com";
        var responseContent = CreateExpectedErrorResponseJson();
        var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
        httpResponseMessage.Content = new StringContent(responseContent);

        _httpClientServiceMock.Setup(x => x.SendRequestAsync(HttpMethod.Post, emdUrl, It.IsAny<StringContent>()))
            .ReturnsAsync(httpResponseMessage);

        IECPService ecpService = new EcpService(_httpClientServiceMock.Object);

        // Action
        var result = await ecpService.SendRequestCertificateId(jsonCertificate, emdUrl);

        //Assert
        var resultContent = await result.Content.ReadAsStringAsync();
        Assert.AreEqual(await httpResponseMessage.Content.ReadAsStringAsync(), resultContent);
    }


    private ESignaturePublicCertificate CreateCertificate()
    {
        return new ESignaturePublicCertificate
        {
            CertificateSerialNumber = "1234567890",
            DocumentVersion = 1,
            BeginCertificate = new DateTime(2023, 1, 1),
            EndCertificate = new DateTime(2024, 1, 1),
            CertificateName = "Certificate Name",
            CertificatePublisher = "Certificate Publisher",
            SignAlgorithm = "Sign Algorithm",
            PublicKey = "Public Key",
            Subject = "Subject"
        };
    }

    private string CreateExpectedJson(ESignaturePublicCertificate certificate)
    {
        JObject certificateJson = JObject.FromObject(new
        {
            serial = certificate.CertificateSerialNumber,
            version = certificate.DocumentVersion.ToString(),
            begDT = certificate.BeginCertificate.ToString("yyyy-MM-ddTHH:mm:ss"),
            name = certificate.CertificateName,
            openKey = certificate.PublicKey
        });
        string ogrnValue = ESignaturePublicCertificate.GetOGRN(certificate.Subject!);
        if (!string.IsNullOrWhiteSpace(ogrnValue))
        {
            certificateJson["ogrn"] = ogrnValue;
        }
        return certificateJson.ToString();
    }

    private string CreateExpectedResponseJson()
    {
        var jObject = JObject.FromObject(new
        {

            errorCode = 200,
            errorMessage = "OK",
            rowCount = "1",
            addition = "Дополнительная информация",
            responseData = JObject.FromObject(new { id = 12345 }),
            errors = new JArray(),
            success = true
        });
        return jObject.ToString();
    }

    private string CreateExpectedErrorResponseJson()
    {
        var jObject = JObject.FromObject(new
        {
            errorCode = 400,
            errorMessage = "OK",
            rowCount = 1,
            addition = "Дополнительная информация",
            responseData = JObject.FromObject(new { }),
            errors = new JArray
            {
                new JObject
                {
                    { "errorCode", "Error1" },
                    { "message", "Error message 1" }
                },
                new JObject
                {
                    { "errorCode", "Error2" },
                    { "message", "Error message 2" }
                }
            },
            success = false
        });
        return jObject.ToString();
    }
}

