using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Strata
{
    public class SerializableBitmap : IXmlSerializable, IDisposable
    {
        private Bitmap _img;

        public Bitmap Img
        {
            get
            {
                return _img ?? (_img = new Bitmap(Settings.Instance.Canvas.Height,
                    Settings.Instance.Canvas.Width));
            }
            set { _img = value; }
        }

        public void Dispose()
        {
        }

        public XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public void ReadXml(XmlReader reader)
        {
            var buffer = new byte[5000];
            reader.ReadToFollowing("Bmp");
            using (var ms = new MemoryStream())
            {
                var bw = new BinaryWriter(ms);
                int readBytes = 0;
                while ((readBytes = reader.ReadElementContentAsBase64(buffer, 0, 50)) > 0)
                    bw.Write(buffer, 0, readBytes);
            }

            var ic = new ImageConverter();
            var img = (Image) ic.ConvertFrom(buffer);
            if (img == null)
                throw new NullReferenceException();

            Img = new Bitmap(img);
        }

        public void WriteXml(XmlWriter writer)
        {
            using (var ms = new MemoryStream())
            {
                Img.Save(ms, ImageFormat.Bmp);
                byte[] bitmapData = ms.ToArray();
                writer.WriteStartElement("Bmp");
                writer.WriteBase64(bitmapData, 0, bitmapData.Length);
                writer.WriteEndElement();
            }
        }

        public Image GetThumbnailImage(int width, int height, object o, IntPtr intPtr)
        {
            return Img.GetThumbnailImage(width, height, (Image.GetThumbnailImageAbort)o, intPtr);
        }
    }
}