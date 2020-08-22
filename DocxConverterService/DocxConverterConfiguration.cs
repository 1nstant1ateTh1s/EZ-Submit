
namespace DocxConverterService
{
    public class DocxConverterConfiguration
    {
        //public const string WarrantInDebtConfiguration = "WarrantInDebtConfiguration";
        //public const string SummonsForUnlawfulDetainerConfiguration = "SummonsForUnlawfulDetainerConfiguration";

        //public string TemplateLocation { get; set; }
        //public string XmlDataFile { get; set; }
        //public string TemplateDocument { get; set; }
        //public string OutputDirectory { get; set; }
        //public string OutputDocumentName { get; set; }

        public string TemplateLocation { get; set; }
        public string WarrantInDebtTemplateDocument { get; set; }
        public string SummonsForUnlawfulDetainerTemplateDocument { get; set; }
        public string OutputDirectory { get; set; }
        public string CourtName { get; set; }
        public string CourtAddress { get; set; }
    }
}
