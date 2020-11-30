using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScreenRecorderLib;
using System.IO;
namespace ProyectoIA
{
    public partial class TestRecord : Form
    {
        
        private Recorder recorder;
        private Stream stream;
        public TestRecord()
        {
            
            InitializeComponent();
        }

        private void TestRecord_Load(object sender, EventArgs e)
        {
            ScreenRecorderLib.
            RecorderOptions recorderOptions = new RecorderOptions()
            { 
                RecorderMode = RecorderMode.Slideshow,
                VideoOptions = new VideoOptions()
                {
                    
                    SnapshotsInterval = 1,
                    SnapshotFormat = ImageFormat.PNG,
                    SnapshotsWithVideo = true,
                    Framerate = 1
                }
            };
            stream = new MemoryStream();
            
            recorder = Recorder.CreateRecorder(recorderOptions);
            recorder.OnRecordingComplete += Rec_OnRecordingComplete;
            recorder.OnRecordingFailed += Rec_OnRecordingFailed;
            recorder.OnStatusChanged += Rec_OnStatusChanged;
            recorder.OnSnapshotSaved += Rec_OnSnapShotSaved;
            
            recorder.Record("algo.mp4");
        }

        

        private void Rec_OnSnapShotSaved(object sender, SnapshotSavedEventArgs e)
        {
            //Get the file path if recorded to a file
            Image image = Image.FromStream(stream);
            this.Text =  "StreanLengthh: " + stream.Length;
            Console.WriteLine("StreanLengthh: " + stream.Length);
            MessageBox.Show("StreanLengthh: " + stream.Length);
            this.screen.Image = image;
       }

        private void Rec_OnRecordingComplete(object sender, RecordingCompleteEventArgs e)
        {
            //Get the file path if recorded to a file
            string path = e.FilePath;
            //or do something with your stream
            //... something ...
            stream?.Dispose();
        }
        private void Rec_OnRecordingFailed(object sender, RecordingFailedEventArgs e)
        {
            string error = e.Error;
            stream?.Dispose();
            MessageBox.Show("Error recordeando: " + error);
        }
        private void Rec_OnStatusChanged(object sender, RecordingStatusEventArgs e)
        {
            RecorderStatus status = e.Status;
        }
    }
}
