using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIA.Util
{
    public interface IOCR
    {
        String GetText(Stream image);

    }
}
