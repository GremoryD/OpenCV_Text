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

            imageBox1.Image = capture.QuerySmallFrame();

            Image<Bgr, Byte> imgProcessed = capture.QueryFrame().ToImage<Bgr, Byte>();
            if (checkedListBox1.GetItemChecked(0))
            {
                Image<Gray, Byte> imgProcessed1;
                imgProcessed1 = imgProcessed.Convert<Gray, byte>(); 
                imageBox1.Image =  (imgProcessed1.Sobel(1, 0, 5));
            }
            if (checkedListBox1.GetItemChecked(1))
            {
                Image<Bgr, byte> mediansmooth = imgProcessed.SmoothMedian(15);
                imageBox1.Image = mediansmooth;
            }
            if (checkedListBox1.GetItemChecked(2))
            { 
                Image<Bgr, byte> blur = imgProcessed.SmoothBlur(10, 10, true);
                imageBox1.Image = blur;
            }
            if (checkedListBox1.GetItemChecked(3))
            {
                Image<Bgr, byte> not_img = imgProcessed.Not();
                imageBox1.Image = not_img;
            }
            if (checkedListBox1.GetItemChecked(4))
            { 
                Image<Bgr, float> bilat = imgProcessed.Laplace(15);
                imageBox1.Image = bilat ;
            } 
            if (checkedListBox1.GetItemChecked(5))
            {
                Image<Bgr, float> Sobel = imgProcessed.Sobel(1, 0, 3);
                imageBox1.Image = Sobel;
            }
            if (checkedListBox1.GetItemChecked(6))
            {
                Image<Bgr, byte> ThresholdBinary = imgProcessed.ThresholdBinary(new Bgr(100, 100,100), new Bgr(200, 200, 200));
                imageBox1.Image = ThresholdBinary;
            }

            if (checkedListBox1.GetItemChecked(7))
            {
                Image<Bgr, byte> ThresholdTrunc = imgProcessed.ThresholdTrunc(new Bgr(125, 125, 100));
                imageBox1.Image = ThresholdTrunc;
            }





        }
    }
}
