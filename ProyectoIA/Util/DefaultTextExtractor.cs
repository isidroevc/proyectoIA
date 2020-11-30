using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoIA.Util
{
    class DefaultTextExtractor : ITextExtractor
    {
        private List<string> chunks;
        public int Workers { get; set; }
        private ICapturador capturador;
        private IOCR ocr;
        List<Thread> threads;
        public DefaultTextExtractor(int workers)
        {
            Workers = workers;
            chunks = new List<string>();
            this.capturador = new CapturadorDefault();
            this.ocr = new IronOCR();
        }
        public string GetChunk()
        {
            lock(chunks)
            {
                StringBuilder resultBuilder = new StringBuilder();
                chunks.ForEach(chunk => resultBuilder.Append(chunk.ToLower()));
                chunks.Clear();
                return resultBuilder.ToString();
                
            }
        }

        public void StartWorking()
        {
            threads = new List<Thread>();
            for(int i = 0; i < Workers; i++)
            {
                Thread thread = new Thread(Work);
                threads.Add(thread);
                thread.Start();
            }
            
        }

        public void StopWorking()
        {
            threads.ForEach(th => th.Abort());
        }

        private void Work()
        {
            while (true)
            {
                Thread.Sleep(100);
                List<Stream> streams = capturador.GetImage();
                List<string> rawTexts = new List<string>();
                streams.ForEach(stream => rawTexts.Add(ocr.GetText(stream)));

                lock (chunks)
                {
                    for (int i = 0, c = rawTexts.Count; i < c; i++)
                    {
                        //System.IO.File.WriteAllText($"./rawText_{i}.txt", rawTexts[i]);
                        chunks.Add(rawTexts[i]);
                    }
                }
            }
        }
    }
}
