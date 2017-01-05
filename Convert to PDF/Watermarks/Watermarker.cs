using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iTextSharp.text.pdf;
using iTextSharp.text;

namespace Convert_to_PDF.Watermarks {
    internal abstract class Watermarker : IDisposable {
        protected Position _position;

        protected abstract void Prep(Watermark watermark);

        protected abstract void AddWatermark(PdfContentByte content);

        public byte[] Watermark(byte[] bPdf) {
            PdfGState gstate = new PdfGState() {
                FillOpacity = 0.1f,
                StrokeOpacity = 0.3f
            };

            using (MemoryStream stream = new MemoryStream(10 * 1024)) {
                using (PdfReader reader = new PdfReader(bPdf))
                using (PdfStamper stamper = new PdfStamper(reader, stream)) {
                    for (int i = 1; i <= stamper.Reader.NumberOfPages; i++) {
                        Rectangle pageSize = reader.GetPageSizeWithRotation(i);
                        _position = new Position() {
                            X = (pageSize.Right + pageSize.Left) / 2,
                            Y = (pageSize.Bottom + pageSize.Top) / 2
                        };
                        
                        PdfContentByte content = stamper.GetOverContent(i);
                        content.SaveState();
                        content.SetGState(gstate);

                        AddWatermark(content);
                    }
                }

                return stream.ToArray();
            }
        }



        public virtual void Dispose() { }

        public static Watermarker Get(Watermark watermark) {
            Watermarker watermarker = null;

            switch (watermark.Type) {
                case WatermarkType.Text:
                    watermarker = new TextWatermarker();
                    break;
                case WatermarkType.Image:
                    watermarker = new ImageWatermarker();
                    break;
            }

            if(watermarker != null)
                watermarker.Prep(watermark);

            return watermarker;
        }
        /*
        public static byte[] AddWatermark(byte[] bytes, Watermark watermark) {
            float angle = 45;
            BaseFont font = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.EMBEDDED);
            float fontSize = 48;
            BaseColor fontColor = BaseColor.RED;

            PdfGState gstate = new PdfGState() {
                FillOpacity = 0.1f,
                StrokeOpacity = 0.3f
            };

            using (MemoryStream stream = new MemoryStream(10 * 1024)) {
                using (PdfReader reader = new PdfReader(bytes))
                using (PdfStamper stamper = new PdfStamper(reader, stream)) {
                    for (int i = 1; i <= stamper.Reader.NumberOfPages; i++) {
                        Rectangle pageSize = reader.GetPageSizeWithRotation(i);
                        float x = (pageSize.Right + pageSize.Left) / 2;
                        float y = (pageSize.Bottom + pageSize.Top) / 2;

                        PdfContentByte data = stamper.GetOverContent(i);
                        data.SaveState();
                        data.SetGState(gstate);

                        switch (watermark.Type) {
                            case WatermarkType.Text:
                                AddTextmark(data, x, y, watermark);
                                break;
                            case WatermarkType.Image:
                                AddImagemark(data, x, y, watermark);
                                break;
                        }

                        data.RestoreState();
                    }
                }

                return stream.ToArray();
            }
        }

        private static void AddTextmark(PdfContentByte content, float x, float y, Watermark watermark) {
            content.SetColorFill(watermark.FontColor);
            content.BeginText();
            content.SetFontAndSize(watermark.Font, watermark.FontSize);

            content.ShowTextAligned(Element.ALIGN_CENTER, watermark.Text, x, y, watermark.Angle);
            content.EndText();
        }

        private static void AddImagemark(PdfContentByte content, float x, float y, Watermark watermark) {
            Image image = Image.GetInstance(WatermarkLocation);
            image.SetAbsolutePosition(x, y);

            content.AddImage(image);
        }
        */
    }
}
