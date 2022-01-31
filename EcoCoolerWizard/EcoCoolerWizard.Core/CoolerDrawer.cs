using Svg;

using System.Drawing;

namespace EcoCoolerWizard.Core
{
    public class CoolerDrawer
    {
        public SvgDocument Draw(Cooler cooler)
        {
            var width = (int)cooler.Width;
            var height = (int)cooler.Height;
            var margin = 2;

            var svgDoc = new SvgDocument
            {
                Width = width,
                Height = height,
                ViewBox = new SvgViewBox(-margin, -margin, width + margin, height + margin),
            };

            var group = new SvgGroup() { };
            svgDoc.Children.Add(group);

            for (int i = 0; i < cooler.Rows; i++)
            {
                for (int j = 0; j < cooler.Columns; j++)
                {
                    group.Children.Add(new SvgCircle
                    {
                        Radius = 1,
                        CenterX = new SvgUnit((float)cooler.MarginLeft + (i * 5)),
                        CenterY = new SvgUnit((float)cooler.MarginTop + (j * 5)),
                        //Fill = new SvgColourServer(Color.Red),
                        Stroke = new SvgColourServer(Color.Black),
                        StrokeWidth = 1,
                    });
                }

            }

            return svgDoc;

            //var stream = new MemoryStream();
            //svgDoc.Write(stream);
            //textBox1.Text = Encoding.UTF8.GetString(stream.GetBuffer());

            //pictureBox1.Image = svgDoc.Draw();
        }
    }
}