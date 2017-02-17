using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using BitMiracle.LibTiff.Classic;

namespace TIFFReader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private class PointXYZ
        {
            public double X = 0;
            public double Y = 0;
            public double Z = 0;

            public PointXYZ()
            {
            }

            public PointXYZ(double pX, double pY, double pZ)
            {
                X = pX;
                Y = pY;
                Z = pZ;
            }
        }

        private class MapRow
        {
            public List<PointXYZ> columns = new List<PointXYZ>();
            public void Add(PointXYZ point)
            {
                columns.Add(point);
            }
        }

        private class ElevationMap
        {
            public List<MapRow> rows = new List<MapRow>();
            public void Add(MapRow row)
            {
                rows.Add(row);
            }
        }


        //List of Point Max min
        private const int MinMaxResultsSalmples = 10;
        private List<PointXYZ> listMaxResult = new List<PointXYZ>();
        private List<PointXYZ> listMinResult = new List<PointXYZ>();

        private void InitializeList()
        {
            for (int i = 0; i < MinMaxResultsSalmples;i++)
            {
                listMaxResult.Add(new PointXYZ());
                listMinResult.Add(new PointXYZ());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newPoint"></param>
        private void CheckMaxResult(PointXYZ newPoint)
        {

            PointXYZ minResult = null;
            
            foreach (PointXYZ point in listMaxResult){
                //Get the minimin value
                if (minResult == null) minResult = point;
                else if (point.Z < minResult.Z) minResult = point;
            }

            if (newPoint.Z > minResult.Z) {
                minResult.X = newPoint.X;
                minResult.Y = newPoint.Y;
                minResult.Z = newPoint.Z;
            }
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="newPoint"></param>
        private void CheckMinResult(PointXYZ newPoint)
        {
            PointXYZ maxResult = null;

            foreach (PointXYZ point in listMinResult)
            {
                //Get the maximun value
                if (maxResult == null) maxResult = point;
                else if (point.Z > maxResult.Z) maxResult = point;
            }

            if (newPoint.Z < maxResult.Z)
            {
                maxResult.X = newPoint.X;
                maxResult.Y = newPoint.Y;
                maxResult.Z = newPoint.Z;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newPoint"></param>
        
        private void CheckMinMaxResult(PointXYZ newPoint)
        {
           CheckMaxResult(newPoint);
           CheckMinResult(newPoint);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //initialize list
            InitializeList();
           
            double maxHeight = 0;
            double minHeight = 0;

            using (Tiff tiff = Tiff.Open(@"C:\Users\javier.santos\Downloads\eudem_dem_5deg_n25w020.tif", "r"))
            {
                int height = tiff.GetField(TiffTag.IMAGELENGTH)[0].ToInt();

                FieldValue[] modelPixelScaleTag = tiff.GetField((TiffTag)33550);
                FieldValue[] modelTiepointTag = tiff.GetField((TiffTag)33922);
                FieldValue[] GDALMetadataTag = tiff.GetField((TiffTag)42112);

                byte[] GDALMetadata = GDALMetadataTag[1].GetBytes();
                XmlDocument doc = new XmlDocument();
                string xml = Encoding.UTF8.GetString(GDALMetadata);
                xml = xml.Replace("\0", string.Empty);
                doc.LoadXml(xml);
                XmlNode initialXMLNode = doc.GetElementsByTagName("GDALMetadata")[0];
                foreach (XmlNode node in initialXMLNode.ChildNodes){
                    string name = node.Attributes.GetNamedItem("name").Value;
                    switch (name)
                    {
                        case "STATISTICS_MAXIMUM":
                            maxHeight = Double.Parse(node.InnerText);
                            break;
                        case "STATISTICS_MINIMUM":
                            minHeight =  Double.Parse(node.InnerText);
                            break;
                    }
                }

                byte[] modelPixelScale = modelPixelScaleTag[1].GetBytes();
                double pixelSizeX = BitConverter.ToDouble(modelPixelScale, 0);
                double pixelSizeY = BitConverter.ToDouble(modelPixelScale, 8) * -1;

                byte[] modelTransformation = modelTiepointTag[1].GetBytes();
                double originLon = BitConverter.ToDouble(modelTransformation, 24);
                double originLat = BitConverter.ToDouble(modelTransformation, 32);

                double startLat = originLat + (pixelSizeY / 2.0);
                double startLon = originLon + (pixelSizeX / 2.0);

                var scanline = new byte[tiff.ScanlineSize()];
                Single[] scanline32Bit = new Single[tiff.ScanlineSize() / 2];

                double currentLat = startLat;
                double currentLon = startLon;

                //Write into the file
                //using (StreamWriter writer = new StreamWriter("output.txt"))
                //{
                    ElevationMap elevationMap = new ElevationMap();

                    //for (int i = 0; i < height; i++)
                    for (int i = 0; i < height; i++)
                    {
                        //MapRow row = new MapRow();

                        tiff.ReadScanline(scanline, i);
                        Buffer.BlockCopy(scanline, 0, scanline32Bit, 0, scanline.Length);

                        var latitude = currentLat + (pixelSizeY * i);
                        for (int j = 0; j < scanline32Bit.Length; j++)
                        {
                            var longitude = currentLon + (pixelSizeX * j);
                            Single value = scanline32Bit[j];
                            if (value == 2143289344){
                                value = -9999;
                            }

                            //Check results
                            CheckMinMaxResult(new PointXYZ(latitude, longitude, value));
                        
                            //row.Add(new PointXYZ(latitude, longitude, value));
                            //writer.Write(value + " ");

                        }

                        //elevationMap.Add(row);
                        //writer.WriteLine();
                    }

                //}
  
            }


            //Write into the file
            using (StreamWriter writer = new StreamWriter("outputResults.txt"))
            {
                writer.WriteLine("MAX REAL HEIGHT = " + maxHeight);
                writer.WriteLine("MIN REAL HEIGHT = " + minHeight);
                writer.WriteLine();
                //Max Values
                for (int i = 0; i < MinMaxResultsSalmples; i++)
                {
                    writer.WriteLine("MAXIMUM " + i);
                    writer.WriteLine(listMaxResult[i].X + ", " + listMaxResult[i].Y + ", " + listMaxResult[i].Z);
                }
                //Min values
                for (int i = 0; i < MinMaxResultsSalmples; i++)
                {
                    writer.WriteLine("MINIMUN " + i);
                    writer.WriteLine(listMinResult[i].X + ", " + listMinResult[i].Y + ", " + listMinResult[i].Z);
                }

               
             }

            System.Windows.Forms.MessageBox.Show("Done");

        }
    }
}
