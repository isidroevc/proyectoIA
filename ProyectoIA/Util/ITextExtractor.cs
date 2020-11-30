using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIA.Util
{
    interface ITextExtractor
    {
        void StartWorking();
        string GetChunk();

        void StopWorking();
    }
}
