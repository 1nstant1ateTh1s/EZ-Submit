
namespace DocxConverterService.Interfaces
{
    public interface IGeneratable
    {
        string TemplateDocument { get; }
        string OutputDocument { get; }
        string FileName { get; }
        string FilePath { get; }
    }
}
