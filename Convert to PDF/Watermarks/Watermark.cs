using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Convert_to_PDF.Watermarks {

    [DataContract]
    internal class Watermark {

        [DataMember(Name = "type", IsRequired = true)]
        private string _type = null;

        [DataMember(Name = "font")]
        private string _font = "Helvetica";

        [DataMember(Name = "fontColor")]
        private string _fontColor = "#FF0000";

        [DataMember(Name = "fontSize")]
        private float _fontSize = 48;

        [DataMember(Name = "text")]
        private string _text = null;

        [DataMember(Name = "angle")]
        private float _angle = 45;

        [DataMember(Name = "image")]
        private string _image = null;

        public WatermarkType Type {
            get { return (WatermarkType)Enum.Parse(typeof(WatermarkType), _type, true); }
        }

        public BaseFont Font {
            get { return BaseFont.CreateFont(_font, BaseFont.WINANSI, BaseFont.EMBEDDED); }
        }

        public BaseColor FontColor {
            get { return new BaseColor(ColorTranslator.FromHtml(_fontColor)); }
        }

        public float FontSize {
            get { return _fontSize; }
        }

        public string Text {
            get { return _text; }
        }

        public float Angle {
            get { return _angle; }
        }

        public byte[] Image {
            get { return Convert.FromBase64String(_image); }
        }
    }
}
