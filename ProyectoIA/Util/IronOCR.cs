using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronOcr;

namespace ProyectoIA.Util
{
    class IronOCR : IOCR
    {
        public string GetText(Stream image)
        {
            return new IronTesseract().Read(Image.FromStream(image)).Text;
        }        
    }
}
