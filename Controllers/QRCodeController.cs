using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QRcodePOC.Models.QRCode.Request;
using QRcodePOC.Models.QRCode.Response;
using QRcodePOC.Services.QRCodeService;

namespace QRcodePOC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QRCodeController : ControllerBase
    {
        private readonly IQRCodeService _qRCodeService;

        public QRCodeController(IQRCodeService qRCodeService)
        {
            _qRCodeService = qRCodeService;
        }
        [HttpPost("[action]")]
        public GenerateQRCodeResponse GenerateQRCode(GenerateQRCodeRequest request)
        {
           var output= _qRCodeService.GenerateQRCode(request);
            return output;
        }
        [HttpPost("[action]")]
        public GetDataFromQRCodeResponse GetDataFromQRCode(GetDataFromQRCodeRequest request) {
            var output=_qRCodeService.GetDataFromQRCode(request);
            return output;
        }
    }
}