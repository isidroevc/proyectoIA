using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIA.Util
{
    public interface ICapturador
    {
        int Frecuencia { get; set; }
        
        List<Stream> GetImage();
        
        void StartCapture();

        EventHandler<Stream> OnScreenShot { get; set; }
    }
}
