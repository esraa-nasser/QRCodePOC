using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QRcodePOC.Models.QRCode.Request
{
    public class GenerateQRCodeRequest
    {
        public long Id { get; set; }

        public string Name { get; set; }
    }
}
