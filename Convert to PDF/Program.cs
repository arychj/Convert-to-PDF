using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Convert_to_PDF.Converters;
using Convert_to_PDF.Watermarks;

namespace Convert_to_PDF {
    class Program {
        static void Main(string[] args) {
            string filename = @"C:\temp\test.docx";
            string watermark = @"C:\temp\you dont say.png";

            string sContents = Convert.ToBase64String(File.ReadAllBytes(filename));
            string sWatermark = Convert.ToBase64String(File.ReadAllBytes(watermark));

            //string sPayload = $"{{\"type\":\"Word\",\"content\":\"{sContents}\",\"watermark\":{{\"type\":\"Text\",\"font\":\"Helvetica\",\"fontSize\":\"48\",\"fontColor\":\"#00FF00\",\"text\":\"this is a textmark\"}}}}";
            string sPayload = $"{{\"type\":\"Word\",\"content\":\"{sContents}\",\"watermark\":{{\"type\":\"Image\",\"image\":\"{sWatermark}\"}}}}";

            Payload payload = Payload.Parse(sPayload);

            byte[] bDocument = Converter.Get(payload.Type).Convert(payload.Content);

            if(payload.Watermark != null) {
                using(Watermarker watermarker = Watermarker.Get(payload.Watermark)) {
                    bDocument = watermarker.Watermark(bDocument);
                }
            }

            File.WriteAllBytes(@"C:\temp\test.pdf", bDocument);
        }
    }
}
