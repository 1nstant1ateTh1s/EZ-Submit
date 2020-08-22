using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocxConverterService.Interfaces
{
    public interface IDocxConverter
    {
        Task<byte[]> Convert<T>(T model) where T : IGeneratable;
    }
}
