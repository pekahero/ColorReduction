using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ColorReduction
{
    public partial class FormColorReduction : Form
    {
        public FormColorReduction()
        {
            InitializeComponent();
        }
        #region |Fields|
        public struct HSB
        {
            public float H, S, B;
        }
        public struct ARGB
        {
            public Int32 A, R, G, B;
            public Point position;
        }
        public struct Palette
        {
            public ARGB color;
            public int count;
        }

        public List<Palette> SourcePalette;
        public List<Palette> SamplePalette;
        public List<Palette> ResultPalette;
        public ARGB[,] SourcePixels;
        public ARGB[,] SamplePixels;
        public ARGB[,] ResultPixels;
        #endregion

        #region |Events|
        private void buttonBrowseSource_Click(object sender, EventArgs e)
        {
            var dlg1 = new OpenFileDialog();
            dlg1.Filter = "Image files (*.BMP, *.JPG, *.GIF, *.TIF, *.PNG, *.ICO, *.EMF, *.WMF)|*.bmp;*.jpg;*.gif; *.tif; *.png; *.ico; *.emf; *.wmf";
            if (dlg1.ShowDialog() == DialogResult.OK)
            {
                pictureBoxSource.Image = Image.FromFile(dlg1.FileName);
                SourcePixels = GetBitmapARGB(new Bitmap(pictureBoxSource.Image));
                SourcePalette = GetColorPalette(SourcePixels);
                textBoxSourceInfo.Text ="Размер: "+Convert.ToString(SourcePixels.GetLength(0))+
                    " х"+ Convert.ToString(SourcePixels.GetLength(1))+
                    " , всего цветов: "+Convert.ToString(SourcePalette.Count());
            }
        }                 

        private void buttonBrowseSample_Click(object sender, EventArgs e)
        {
            var dlg2 = new OpenFileDialog();
            dlg2.Filter = "Image files (*.BMP, *.JPG, *.GIF, *.TIF, *.PNG, *.ICO, *.EMF, *.WMF)|*.bmp;*.jpg;*.gif; *.tif; *.png; *.ico; *.emf; *.wmf";
            if (dlg2.ShowDialog() == DialogResult.OK)
            {
                pictureBoxSample.Image = Image.FromFile(dlg2.FileName);
                SamplePixels = GetBitmapARGB(new Bitmap(pictureBoxSample.Image));
                SamplePalette = GetColorPalette(SamplePixels);
                
                textBoxSampleInfo.Text = "Размер: " + Convert.ToString(SamplePixels.GetLength(0)) +
                    " х" + Convert.ToString(SamplePixels.GetLength(1)) +
                    " , всего цветов: " + Convert.ToString(SamplePalette.Count());
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (pictureBoxResult.Image == null)
                    return;
            else
            {
                var ResultSaveDialog = new SaveFileDialog();
                ResultSaveDialog.DefaultExt = ".jpg";
                if (ResultSaveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pictureBoxResult.Image.Save(ResultSaveDialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    return;
            }
        }

        private void buttonProcess_Click(object sender, EventArgs e)
        {
            GetResultPixels();
            pictureBoxResult.Image = CreateBitmapFromPixels(ResultPixels);
        }
        #endregion

        #region |ColoringMethods|
        public ARGB Euclidean(int i, int j)
        {
            double min = 442;
            int index = 0;
            for(int k=0; k<SourcePalette.Count();k++)
            {
                double distance = Math.Sqrt(Math.Pow((SourcePalette[k].color.A - SamplePixels[i, j].A), 2) +
                    Math.Pow((SourcePalette[k].color.R - SamplePixels[i, j].R), 2) +
                    Math.Pow((SourcePalette[k].color.G - SamplePixels[i, j].G), 2) +
                    Math.Pow((SourcePalette[k].color.B - SamplePixels[i, j].B), 2));
                if (distance < min)
                {
                    min = distance;
                    index = k;
                }
            }
            return SourcePalette[index].color;
        }

        public Bitmap CreateBitmapFromPixels( ARGB[,] pixels)
        {
            Bitmap bmp = new Bitmap(pixels.GetLength(0), pixels.GetLength(1));

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    bmp.SetPixel(i, j, Color.FromArgb(pixels[i, j].A,  pixels[i, j].R, pixels[i, j].G, pixels[i, j].B));
                }
            }

            return bmp;
        }
        #endregion

        # region |ColorFunctions|

        private List<Palette> DiscardColorsPalette(List<Palette> palette, int treshold)
        {
            List<Palette> tmppalette =(from p in palette orderby p.count select p).ToList<Palette>();
            /*palette.Sort(delegate (Palette palette1, Palette palette2)
            { return palette1.count.CompareTo(palette2.count); });*/
            List<Palette> newpalette = new List<Palette> { };
            for(int i=palette.Count()-1; i>= palette.Count()-treshold;i--)
            {
                newpalette.Add(tmppalette[i]);
            }
            return newpalette;
        }
        private void GetResultPixels()
        {
            ResultPixels = new ARGB[SamplePixels.GetLength(0), SamplePixels.GetLength(1)];
            //List<Palette> newSourcePalette = DiscardColorsPalette(SourcePalette, SourcePalette.Count()/1000);
            ClothestColorFinder CCF = new ClothestColorFinder(SourcePalette.Select(s => 
            new RGBColor((Int16)s.color.R, (Int16)s.color.G, (Int16)s.color.B)), 1000);
            progressBarResult.Maximum = SamplePixels.GetLength(0) * SamplePixels.GetLength(1);
            progressBarResult.Value = 0;
            for (int i = 0; i < SamplePixels.GetLength(0); i++)
            {
                for (int j = 0; j < SamplePixels.GetLength(1); j++)
                {
                    /*//ResultPixels[i,j]=Euclidean(i,j);
                    double min = 256*256*256;
                    int index = 0;
                    for (int k = 0; k < newSourcePalette.Count(); k++)
                    {
                        double distance = Math.Pow((newSourcePalette[k].color.A - SamplePixels[i, j].A),2) +
                            Math.Pow((newSourcePalette[k].color.R - SamplePixels[i, j].R),2) +
                            Math.Pow((newSourcePalette[k].color.G - SamplePixels[i, j].G),2) +
                            Math.Pow((newSourcePalette[k].color.B - SamplePixels[i, j].B),2);
                        if (distance < min)
                        {
                            min = distance;
                            index = k;
                        }
                    }
                    //ResultPixels[i, j] = newSourcePalette[index].color;*/
                    RGBColor tmp = CCF.apply(new RGBColor((Int16)SamplePixels[i, j].R, (Int16)SamplePixels[i, j].G, (Int16)SamplePixels[i, j].B));
                    ResultPixels[i, j] = new ARGB { A = 255, R = tmp.r, G = tmp.g, B = tmp.b };
                    progressBarResult.Value++;
                        
                }
            }           
        }

        private List<Palette> GetColorPalette(ARGB[,] pixels)
        {
            List<Palette> palette = new List<Palette> { };
            List<int> ColorInt = new List<int> { };
            const int Million = 1000000;
            const int Thousand = 1000;
            for (int i=0; i<pixels.GetLength(0);i++)
            {
                for (int j = 0; j < pixels.GetLength(1); j++)
                {
                    ColorInt.Add(Million * pixels[i, j].R + Thousand * pixels[i, j].G + pixels[i, j].B);
                }
            }
            List<int> ColorIntTmp = ColorInt.Distinct().ToList();

            Dictionary<int, int> tmpdict = new Dictionary<int, int> { };

            foreach (var c in ColorInt)
            {
                if (tmpdict.ContainsKey(c))
                {
                    tmpdict[c]++;
                }
                else
                {
                    tmpdict.Add(c, 1);
                }
            }
            foreach (KeyValuePair<int,int> kvp in tmpdict)
            {
                palette.Add(new Palette { color = new ARGB { A = 255,
                    R = (int)(kvp.Key / Million),
                    G = (int)((kvp.Key % Million) / Thousand),
                    B = (int)(kvp.Key % Thousand) },
                    count = kvp.Value});
            }

            return palette;
        }

        public static ARGB[,] GetBitmapARGB(Bitmap bmp)
        {
            var width = bmp.Width;
            var height = bmp.Height;

            var pxf = bmp.PixelFormat;
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            var bmpData = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, pxf);
            IntPtr ptr = bmpData.Scan0;
            int numBytes = bmpData.Stride * bmp.Height;
            int widthBytes = bmpData.Stride;
            byte[] rgbValues = new byte[numBytes];
            Marshal.Copy(ptr, rgbValues, 0, numBytes);

            int n = 0;
            int m = 0;
            int d = 0;
            if (pxf == System.Drawing.Imaging.PixelFormat.Format24bppRgb)
            {
                d = 3;
            }
            else
            {
                if (pxf == System.Drawing.Imaging.PixelFormat.Format32bppArgb)
                {
                    d = 4;
                }
                else
                {
                    throw new Exception("Wrong pixel format.");
                }
            }
            ARGB[,] pixels = new ARGB[bmp.Width, bmp.Height];

            for (int counter = 0; counter < rgbValues.Length; counter += d)
            {
                if (m == bmp.Width)
                {
                    n++;
                    m = 0;
                }
                if (d == 4)
                    pixels[m, n].A = Convert.ToInt32(rgbValues[counter + 3]);
                else
                    pixels[m, n].A = 255;
                pixels[m, n].R = Convert.ToInt32(rgbValues[counter + 2]);
                pixels[m, n].G = Convert.ToInt32(rgbValues[counter + 1]);
                pixels[m, n].B = Convert.ToInt32(rgbValues[counter]);
                pixels[m, n].position.X = m;
                pixels[m, n].position.Y = n;

                m++;
            }
            Marshal.Copy(rgbValues, 0, ptr, numBytes);
            bmp.UnlockBits(bmpData);
            return pixels;
        }
        #endregion


    }
}