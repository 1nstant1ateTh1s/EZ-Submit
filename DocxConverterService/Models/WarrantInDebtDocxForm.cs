using DocxConverterService.Interfaces;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace DocxConverterService.Models
{
    [XmlRoot("WarrantInDebtMsWordForm", Namespace = "http://schemas.cityofchesapeake.net/ezsubmit/warrant-in-debt-properties")]
    public class WarrantInDebtDocxForm : IGeneratable
    {
        private const string TEMPLATE_LOCATION = @"\Templates\Warrant_In_Debt-Template.docx";
        private const string BASE_FILE_NAME = "Generated_Warrant_In_Debt";
        private const string BASE_FILE_TYPE = "docx";

        public string TemplateFile { get; }

        public string FileName { 
            get
            {
                return string.Format("{0}_{1}.{2}",
                    BASE_FILE_NAME, 
                    (!string.IsNullOrWhiteSpace(Fields.CaseNumber) ? Fields.CaseNumber : "NoCaseNumber"),
                    BASE_FILE_TYPE);
            }
        }

        public WarrantInDebtDocxFormFields Fields { get; set; }

        public WarrantInDebtDocxForm()
        {
            // Get the path to the build directory at runtime in order to have full path to Template file
            var buildDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            TemplateFile = buildDir + TEMPLATE_LOCATION;

            Fields = new WarrantInDebtDocxFormFields();
        }

    }
}
