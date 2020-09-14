using System;
using System.Collections.Generic;
using System.Text;

namespace DocxConverterService.Interfaces
{
    public interface IDocxConvertable
    {
        IGeneratable ToDocxForm();
    }
}
