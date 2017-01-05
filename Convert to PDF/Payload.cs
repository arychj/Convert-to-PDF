using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

using Convert_to_PDF.Watermarks;

namespace Convert_to_PDF {

    [DataContract]
    internal class Payload {

        [DataMember(Name = "type", IsRequired = true)]
        private string _type = null;

        [DataMember(Name = "content", IsRequired = true)]
        private string _content = null;

        [DataMember(Name = "watermark")]
        private Watermark _watermark = null;

        public KnownFileType Type {
            get { return (KnownFileType)Enum.Parse(typeof(KnownFileType), _type, true); }
        }

        public byte[] Content {
            get { return Convert.FromBase64String(_content); }
        }

        public Watermark Watermark {
            get { return _watermark; }
        }

        public static Payload Parse(string sPayload) {
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(sPayload))) {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Payload));

                stream.Position = 0;
                Payload payload = (Payload)serializer.ReadObject(stream);

                return payload;
            }
        }
    }
}
