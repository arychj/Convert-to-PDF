using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace Convert_to_PDF.Watermarks {
    internal class ImageWatermarker : Watermarker {
        private Watermark _watermark;
        private string _imagePath;

        protected override void Prep(Watermark watermark) {
            _watermark = watermark;

            _imagePath = Path.GetTempFileName();
            File.WriteAllBytes(_imagePath, watermark.Image);
        }

        protected override void AddWatermark(PdfContentByte content) {
            Image image = Image.GetInstance(_imagePath);
            image.SetAbsolutePosition(_position.X, _position.Y);

            content.AddImage(image);
        }

        public override void Dispose() {
            if(File.Exists(_imagePath))
                File.Delete(_imagePath);
        }
    }
}
