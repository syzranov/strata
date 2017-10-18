﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace Strata
{
    public static class BitmapTools
    {
        /// <summary>  
        /// Returns a transparent background GIF image from the specified Bitmap.  
        /// </summary>  
        /// <param name="bitmap">The Bitmap to make transparent.</param>  
        /// <param name="color">The Color to make transparent.</param>  
        /// <returns>New Bitmap containing a transparent background gif.</returns>  
        private static Bitmap MakeTransparentGif(Bitmap bitmap, Color color)
        {
            byte R = color.R;
            byte G = color.G;
            byte B = color.B;
            MemoryStream fin = new MemoryStream();
            bitmap.Save(fin, System.Drawing.Imaging.ImageFormat.Gif);
            MemoryStream fout = new MemoryStream((int)fin.Length);
            int count = 0;
            byte[] buf = new byte[256];
            byte transparentIdx = 0;
            fin.Seek(0, SeekOrigin.Begin);
            //header  
            count = fin.Read(buf, 0, 13);
            if ((buf[0] != 71) || (buf[1] != 73) || (buf[2] != 70)) return null; //GIF  
            fout.Write(buf, 0, 13);
            int i = 0;
            if ((buf[10] & 0x80) > 0)
            {
                i = 1 << ((buf[10] & 7) + 1) == 256 ? 256 : 0;
            }
            for (; i != 0; i--)
            {
                fin.Read(buf, 0, 3);
                if ((buf[0] == R) && (buf[1] == G) && (buf[2] == B))
                {
                    transparentIdx = (byte)(256 - i);
                }
                fout.Write(buf, 0, 3);
            }
            bool gcePresent = false;
            while (true)
            {
                fin.Read(buf, 0, 1);
                fout.Write(buf, 0, 1);
                if (buf[0] != 0x21) break;
                fin.Read(buf, 0, 1);
                fout.Write(buf, 0, 1);
                gcePresent = (buf[0] == 0xf9);
                while (true)
                {
                    fin.Read(buf, 0, 1);
                    fout.Write(buf, 0, 1);
                    if (buf[0] == 0) break;
                    count = buf[0];
                    if (fin.Read(buf, 0, count) != count) return null;
                    if (gcePresent)
                    {
                        if (count == 4)
                        {
                            buf[0] |= 0x01;
                            buf[3] = transparentIdx;
                        }
                    }
                    fout.Write(buf, 0, count);
                }
            }
            while (count > 0)
            {
                count = fin.Read(buf, 0, 1);
                fout.Write(buf, 0, 1);
            }
            fin.Close();
            fout.Flush();
            return new Bitmap(fout);
        }
        public static Bitmap CombineBitmap(int width, int height)
        {
            Bitmap finalImage = null;
            try
            {
                finalImage = new Bitmap(width, height);
                using (Graphics g = Graphics.FromImage(finalImage))
                {
                    g.Clear(Color.Transparent);

                    foreach (Layer layer in Project.Instance.Layers
                        .Where(x => x.Visible)
                        .OrderByDescending(x => x.OrderId))
                    {
                        g.DrawImage(layer.Bmp,
                            new Rectangle(
                                layer.Location.X,
                                layer.Location.Y,
                                layer.Bmp.Width,
                                layer.Bmp.Height));
                    }
                }

                return finalImage;
            }
            catch (Exception ex)
            {
                if (finalImage != null)
                    finalImage.Dispose();

                throw ex;
            }
        }

        public static Bitmap CombineBitmap(ImageFormat format, int width, int height)
        {
            Bitmap finalImage = null;


            try
            {
                finalImage = new Bitmap(width, height);
                using (Graphics g = Graphics.FromImage(finalImage))
                {
                    if (format == ImageFormat.Png)
                        g.Clear(Color.Transparent);

                    if (format == ImageFormat.Jpeg ||
                        format == ImageFormat.Gif)
                        g.Clear(Color.White);

                    foreach (Layer layer in Project.Instance.Layers
                        .Where(x => x.Visible)
                        .OrderByDescending(x => x.OrderId))
                    {
                        //var image = layer.Bmp;

                        //if (format == ImageFormat.Gif)
                        //    image = MakeTransparentGif(image, Color.Transparent);

                        g.DrawImage(layer.Bmp,
                            new Rectangle(
                                layer.Location.X,
                                layer.Location.Y,
                                layer.Bmp.Width,
                                layer.Bmp.Height));
                    }
                }

                return finalImage;
            }
            catch (Exception ex)
            {
                if (finalImage != null)
                    finalImage.Dispose();

                throw ex;
            }
        }
    }
}