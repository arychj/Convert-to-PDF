using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Office.Interop.Excel;

namespace Convert_to_PDF.Converters {
    internal class ConverterExcel : Converter {

        public override byte[] Convert(byte[] bDocument) {
            string filenameInput = Path.GetTempFileName();
            string filenameOutput = Path.GetTempFileName();

            Application excel = new Application();
            excel.Visible = false;
            excel.ScreenUpdating = false;

            Workbook workbook = excel.Workbooks.Open(filenameInput);

            workbook.Activate();
            workbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, filenameOutput);

            workbook = null;

            // word has to be cast to type _Application so that it will find
            // the correct Quit method.
            ((_Application)excel).Quit();
            excel = null;

            byte[] bPdf = File.ReadAllBytes(filenameOutput);

            File.Delete(filenameInput);
            File.Delete(filenameOutput);

            return bPdf;
        }
    }
}
