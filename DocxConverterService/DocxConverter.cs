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
            string templateDocument = model.TemplateDocument;
            string outputDocument = model.OutputDocument;

            #region Option #1 - File.Copy()
            // Make a copy of templateDocument then populate with new data (overwrite any existing file)
            File.Copy(templateDocument, outputDocument, true);

            using (WordprocessingDocument wordDoc =
                WordprocessingDocument.Open(outputDocument, true))
            {
                // Get the main part of the document which contains CustomXMLParts
                MainDocumentPart mainPart = wordDoc.MainDocumentPart;

                // Delete all CustomXMLParts in the document. If needed, only specific CustomXMLParts can be deleted using the CustomXmlParts IEnumerable
                mainPart.DeleteParts<CustomXmlPart>(mainPart.CustomXmlParts);

                // Add new CustomXMLPart with data from new XML
                CustomXmlPart myXmlPart = mainPart.AddCustomXmlPart(CustomXmlPartType.CustomXml);
                using (var stream = SerializeObject(model))
                {
                    myXmlPart.FeedData(stream);
                    wordDoc.Save();
                }
            }

            byte[] byteArray = File.ReadAllBytes(outputDocument);

            // TODO: File.Delete(outputDocument);

            return byteArray;
            #endregion
        }

        /// <summary>
        /// Serialize the model into XML.
        /// </summary>
        private MemoryStream SerializeObject<T>(T model)
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
