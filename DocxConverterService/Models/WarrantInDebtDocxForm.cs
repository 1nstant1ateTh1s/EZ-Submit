using DocxConverterService.Interfaces;
using System.IO;
using System.Xml.Serialization;

namespace DocxConverterService.Models
{
    [XmlRoot("WarrantInDebtMsWordForm", Namespace = "http://schemas.cityofchesapeake.net/ezsubmit/warrant-in-debt-properties")]
    public class WarrantInDebtDocxForm : IGeneratable
    {
        public string TemplateDocument { get; }
        public string OutputDocument { get; }
        public string FileName { get; }
        public string FilePath { get; }
        public WarrantInDebtDocxFormFields Fields { get; set; }

        public WarrantInDebtDocxForm(DocxConverterOptions docxConverterConfig)
        {
            TemplateDocument = docxConverterConfig.TemplateLocation + docxConverterConfig.WarrantInDebtTemplateDocument;
            FilePath = docxConverterConfig.OutputDirectory;
            FileName = string.Format("{0}_{1}.docx", "Generated_Warrant_In_Debt", "TODO"); // TODO: Incorporate case number to make the FileName unique / less likely to encounter a concurrency conflict
            OutputDocument = Path.Combine(
                FilePath, 
                string.Format($"{FileName}"));

            Fields = new WarrantInDebtDocxFormFields();
        }


    }
}
