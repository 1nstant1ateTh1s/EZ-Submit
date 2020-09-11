using DocumentFormat.OpenXml.Packaging;
using DocxConverterService.Interfaces;
using Microsoft.Extensions.Options;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DocxConverterService
{
    public class DocxConverter : IDocxConverter
    {
        private readonly IOptions<DocxConverterOptions> _docxConverterConfig;

        public DocxConverter(IOptions<DocxConverterOptions> docxConverterConfig)
        {
            _docxConverterConfig = docxConverterConfig;
        }

        public async Task<byte[]> Convert<T>(T model) where T : IGeneratable
        {
            string templateSource = model.TemplateFile;
            string outputPath = _docxConverterConfig.Value.OutputDirectory;
            string outputFile = Path.Combine(
               outputPath,
               model.FileName);

            #region Option #1 - File.Copy()

            // Create a new output folder.
            // If directory already exists, this method does nothing
            Directory.CreateDirectory(outputPath);

            // Make a copy of templateSource then populate with new data (overwrite any existing file)
            File.Copy(templateSource, outputFile, true);

            using (WordprocessingDocument wordDoc =
                WordprocessingDocument.Open(outputFile, true))
            {
                // Get the main part of the document which contains CustomXMLParts
                MainDocumentPart mainPart = wordDoc.MainDocumentPart;

                // Delete all CustomXMLParts in the document. If needed, only specific CustomXMLParts can be deleted using the CustomXmlParts IEnumerable
                mainPart.DeleteParts<CustomXmlPart>(mainPart.CustomXmlParts);

                // Add new CustomXMLPart with data from new XML
                CustomXmlPart myXmlPart = mainPart.AddCustomXmlPart(CustomXmlPartType.CustomXml);
                using (var stream = SerializeXml(model))
                {
                    myXmlPart.FeedData(stream);
                    wordDoc.Save();
                }
            }

            byte[] byteArray = File.ReadAllBytes(outputFile);

            // Delete the file on disk
            File.Delete(outputFile);

            return byteArray;
            #endregion
        }

        /// <summary>
        /// Serialize the model into XML.
        /// </summary>
        private MemoryStream SerializeXml<T>(T model)
        {
            XmlSerializer serializer = new XmlSerializer(model.GetType());
            var stream = new MemoryStream();

            serializer.Serialize(stream, model);

            // TESTING THIS - NOT WORKING
            //XmlTextWriter xmlTextWriter = new XmlTextWriter(stream, Encoding.UTF8);
            //serializer.Serialize(xmlTextWriter, model);
            //stream = (MemoryStream)xmlTextWriter.BaseStream;

            //stream.Position = 0;
            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }


    }
}
