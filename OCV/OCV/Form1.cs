using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; 

using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;

namespace OCV
{
    public partial class Form1 : Form
    {
        VideoCapture capture; 

        public Form1()
        {
            InitializeComponent();
            Run();
        }

        private void Run()
        {
            try
            {
                capture = new VideoCapture();
            }catch(Exception ex)
            {
                MessageBox.Show("Error at Run");
                return;
            }

          
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private CascadeClassifier _cascadeClassifier;
        private void timer1_Tick(object sender, EventArgs e)
        {
            string[] files = { "\\face.xml", "\\haarcascade_frontalface_alt.xml" , "\\haarcascade_eye.xml",
                             "\\haarcascade_frontalface_alt2.xml" , "\\haarcascade_frontalface_default.xml" , "\\smile.xml","\\cas1.xml"};
        _cascadeClassifier = new CascadeClassifier(Application.StartupPath + "\\haarcascade" + files[4]);
        using (var imageFrame = capture.QuerySmallFrame().ToImage<Bgr, Byte>())
                {
                    if (imageFrame != null)
                    {
                        var grayframe = imageFrame.Convert<Gray, byte>();
                        var faces = _cascadeClassifier.DetectMultiScale(grayframe, 1.1, 10, Size.Empty); //the actual face detection happens here
                        foreach (var face in faces)
                        {
                            imageFrame.Draw(face, new Bgr(Color.Red), 1); //the detected face(s) is highlighted here using a box that is drawn around it/them
                           
                        }
                    }
                imageBox2.Image = imageFrame;
            }
            Image<Gray, Byte> imgProcessed1;
            Image<Bgr, Byte> imgProcessed = capture.QueryFrame().ToImage<Bgr, Byte>();
            imgProcessed1 = imgProcessed.Convert<Gray, byte>();
            Image<Gray, Single> img_final = (imgProcessed1.Sobel(1, 0, 5));
            imageBox1.Image = img_final;
           // imageBox1.Image = capture.QuerySmallFrame();
        }
    }
}
