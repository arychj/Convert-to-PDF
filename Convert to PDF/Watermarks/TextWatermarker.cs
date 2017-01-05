using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iTextSharp.text;
using iTextSharp.text.pdf;


namespace Convert_to_PDF.Watermarks {
    internal class TextWatermarker : Watermarker {
        private Watermark _watermark;

        protected override void Prep(Watermark watermark) {
            _watermark = watermark;
        }

        protected override void AddWatermark(PdfContentByte content) {
            content.SetColorFill(_watermark.FontColor);
            content.BeginText();
            content.SetFontAndSize(_watermark.Font, _watermark.FontSize);

            content.ShowTextAligned(Element.ALIGN_CENTER, _watermark.Text, _position.X, _position.Y, _watermark.Angle);
            content.EndText();
        }
    }
}
