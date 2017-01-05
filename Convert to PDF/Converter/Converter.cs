using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convert_to_PDF.Converters {

    internal abstract class Converter {

        public abstract byte[] Convert(byte[] bDocument);

        public static Converter Get(KnownFileType type) {
            switch (type) {
                case KnownFileType.Word:
                    return new ConverterWord();
                case KnownFileType.Excel:
                    return new ConverterExcel();
                default:
                    return null;
            }
        }

    }
}
