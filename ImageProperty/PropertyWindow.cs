using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
namespace PropertyWindow
{
    public partial class PropertyWindow : UserControl
    {
       

        private string imagePath;
        private string[] sysCollPath;
        public string ImagePath { get { return imagePath; } }
        private int i=0;
        public PropertyWindow()
        {
            InitializeComponent();
        }
        private void setPreviewImage()
        {try
            {
                previewPictureBox.Image = pictureBox1.Image;
                //   MessageBox.Show(sysCollPath[i]+"Value of i:"+i); //for testing
                imagePath = sysCollPath[i];
            }
            catch(Exception ex)
            {
                imagePath = null;
            }
           
        }
        private void Prev(string [] pathArray, ref int counter,ref PictureBox pictureBox)
        {try
            {
                if (counter > 0)
                {
                    pictureBox.Image = Image.FromFile(pathArray[--counter]);
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Next(string [] pathArray, ref int counter,ref PictureBox pictureBox) //pass by reference
        {
            try
            {
                if (i < pathArray.Length - 1)
                {
                    pictureBox.Image = Image.FromFile(pathArray[++i]);
                }
                else
                { //later...
                }
            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message); }

        }
       
        private void PropertyWindow_Load(object sender, EventArgs e)
        { try
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                previewPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                sysCollPath = Directory.GetFiles(Environment.CurrentDirectory + @"\Collection");
                pictureBox1.Image = Image.FromFile(sysCollPath[0]);
            }
            catch(Exception ex)
            { }
          }

        private void SysPrevBtn_Click(object sender, EventArgs e)
        {
            Prev(sysCollPath,ref i,ref pictureBox1);
           // Form1.tempProp.Text =""+i;   //for testing purpose
         }
        private void SysNextBtn_Click(object sender, EventArgs e)
        {
            Next(sysCollPath, ref i,ref pictureBox1);
          //  Form1.tempProp.Text = "" + i; //for testing purpose
        }

        private void button1_Click(object sender, EventArgs e)
        { try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Title = "Select Images to Add to Collection...";
               
                    ofd.Filter = "Image Files(*.JPG; *.JPEG; *.PNG; *.BMP; *.GIF)|*.JPG; *.JPEG; *.PNG; *.BMP; *.GIF ";

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        string imgpath = ofd.FileName;
                        //       string newFileName = (Directory.GetFiles(Environment.CurrentDirectory + @"\Collection").Count() ).ToString()+Path.GetExtension(ofd.SafeFileName);
                        File.Copy(imgpath, Environment.CurrentDirectory + @"\Collection\" + ofd.SafeFileName);
                        sysCollPath = Directory.GetFiles(Environment.CurrentDirectory + @"\Collection");
                        pictureBox1.Image = Image.FromFile(ofd.FileName);
                    }
                }
            }
            catch(IOException ex)
            {
                
                MessageBox.Show("File with SAME Name already exists.", "Exception Occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.Cursor = Cursors.Hand;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.Cursor = Cursors.Arrow;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Focus();
            setPreviewImage();
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            setPreviewImage();
        }
    }
}
