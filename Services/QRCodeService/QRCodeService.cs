using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using QRcodePOC.Helpers.QRCodeHelper;
using QRcodePOC.Models.QRCode.Request;
using QRcodePOC.Models.QRCode.Response;
using QRCoder;
using ZXing;
using ZXing.Common;

namespace QRcodePOC.Services.QRCodeService
{
    public class QRCodeService : IQRCodeService
    {
        public GenerateQRCodeResponse GenerateQRCode(GenerateQRCodeRequest request)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(request.Name, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            MemoryStream stream = new MemoryStream();
            qrCodeImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            var bitmapBytes = stream.ToArray();


            return SaveQrImage(bitmapBytes, request.Id.ToString());
        }

        public GetDataFromQRCodeResponse GetDataFromQRCode(GetDataFromQRCodeRequest request)
        {
            var PathWithFolderName = Path.Combine (Directory.GetCurrentDirectory (), "../Picture_QrCodes/" + request.Id.ToString());

            if (!Directory.Exists(PathWithFolderName))
            {
                throw new Exception("Path Not Valid");
            }

            Bitmap image = new Bitmap(@$"{PathWithFolderName}\{request.Id.ToString()}.PNG");
            LuminanceSource source = new BitmapLuminanceSource(image);
            BinaryBitmap bitmap = new BinaryBitmap(new HybridBinarizer(source));
            Result result = new MultiFormatReader().decode(bitmap);
            return new GetDataFromQRCodeResponse() { Data = result.Text };

        }
        private GenerateQRCodeResponse SaveQrImage(byte[] bytes, string qrDirectoryName)
        {
            var PathWithFolderName = Path.Combine(Directory.GetCurrentDirectory(), "../Picture_QrCodes/" + qrDirectoryName);

            if (!Directory.Exists(PathWithFolderName))
            {
                DirectoryInfo di = Directory.CreateDirectory(PathWithFolderName);
            }

            System.Drawing.Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = System.Drawing.Image.FromStream(ms);
            }

            string LocalQrLocation = PathWithFolderName + "/" + qrDirectoryName.ToString() + ".png";
            image.Save(LocalQrLocation);

            // Check if the QrCode is saved successfully.
            if (!File.Exists(LocalQrLocation))
            {
                throw new System.Exception("A problem in saving Qrcode has occured");
            }


            GenerateQRCodeResponse createdQrDto = new GenerateQRCodeResponse
            {
                Location = LocalQrLocation
            };

            return createdQrDto;
        }
    }
}
