using ESignatureService.Common;
using ESignatureService.CQRS.Commands;
using ESignatureService.Interfaces.CQRS.Commands;
using Microsoft.AspNetCore.Mvc;

namespace ESignatureService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AssignEcpCertificateIdController(ICommandDispatcher commandDispatcher) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post(AssignCertificateIdCommand assignCertificateIdCommand)
        {
            if (assignCertificateIdCommand.ESignatureCertificateId <= 0) return BadRequest(Constant.INVALID_ID);
            await commandDispatcher.ExecuteAsync(assignCertificateIdCommand);
            return Ok();
        }
    }
}
