using QRcodePOC.Models.QRCode.Request;
using QRcodePOC.Models.QRCode.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QRcodePOC.Services.QRCodeService
{
    public interface IQRCodeService
    {
        GenerateQRCodeResponse GenerateQRCode(GenerateQRCodeRequest request);

        GetDataFromQRCodeResponse GetDataFromQRCode(GetDataFromQRCodeRequest request);
    }
}
