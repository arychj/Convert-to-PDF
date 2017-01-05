using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Office.Interop.Word;

namespace Convert_to_PDF.Converters {
    internal class ConverterWord : Converter {

        public override byte[] Convert(byte[] bDocument) {
            string filenameInput = Path.GetTempFileName();
            string filenameOutput = Path.GetTempFileName();

            object oMissing = System.Reflection.Missing.Value;
            object oFilenameInput = (object)filenameInput;
            object oFilenameOutput = (object)filenameOutput;
            object oFileFormat = WdSaveFormat.wdFormatPDF;

            Application word = new Application();
            word.Visible = false;
            word.ScreenUpdating = false;

            File.WriteAllBytes(filenameInput, bDocument);
            Document doc = word.Documents.Open(
                ref oFilenameInput, 
                ref oMissing,
                ref oMissing, 
                ref oMissing,
                ref oMissing, 
                ref oMissing, 
                ref oMissing,
                ref oMissing,
                ref oMissing, 
                ref oMissing,
                ref oMissing, 
                ref oMissing,
                ref oMissing,
                ref oMissing, 
                ref oMissing, 
                ref oMissing
            );

            doc.Activate();

            doc.SaveAs(
                ref oFilenameOutput,
                ref oFileFormat,
                ref oMissing,
                ref oMissing,
                ref oMissing, 
                ref oMissing, 
                ref oMissing, 
                ref oMissing,
                ref oMissing,
                ref oMissing, 
                ref oMissing, 
                ref oMissing,
                ref oMissing, 
                ref oMissing, 
                ref oMissing, 
                ref oMissing
            );

            // Close the Word document, but leave the Word application open.
            // doc has to be cast to type _Document so that it will find the
            // correct Close method.                
            object saveChanges = WdSaveOptions.wdDoNotSaveChanges;
            ((_Document)doc).Close(ref saveChanges, ref oMissing, ref oMissing);

            doc = null;

            // word has to be cast to type _Application so that it will find
            // the correct Quit method.
            ((_Application)word).Quit(ref oMissing, ref oMissing, ref oMissing);
            word = null;

            byte[] bPdf = File.ReadAllBytes(filenameOutput);

            File.Delete(filenameInput);
            File.Delete(filenameOutput);

            return bPdf;
        }
    }
}
