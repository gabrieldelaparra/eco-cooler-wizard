using Svg;
using System.Drawing;
using Svg.Transforms;

//Move this out from Core.
namespace EcoCoolerWizard.Core
{
    public class CoolerDrawer
    {
        public SvgDocument Draw(Cooler cooler)
        {
            var width = (int) cooler.Width;
            var height = (int) cooler.Height;
            var margin = 4;
            var m = 50;

            var svgDoc = new SvgDocument
            {
                Width = width * m,
                Height = height * m,
                ViewBox = new SvgViewBox(
                    -margin * m,
                    -margin * m,
                    width * m + margin * m,
                    height * m + margin * m),
            };

            var circlesGroup = new SvgGroup() { };
            svgDoc.Children.Add(circlesGroup);

            var measuresGroup = new SvgGroup() { };
            svgDoc.Children.Add(measuresGroup);
            
            var firstColumnLine = new SvgLine()
            {
                StartX = 0,
                EndX = 0,
                StartY = (float) -(margin * m / 4),
                EndY = (float) -(margin * m / 2),
                Stroke = new SvgColourServer(Color.GreenYellow),
                StrokeWidth = 1,
            };
            measuresGroup.Children.Add(firstColumnLine);

            var lastColumnLine = new SvgLine()
            {
                StartX = width * m,
                EndX = width * m,
                StartY = (float) -(margin * m / 4),
                EndY = (float) -(margin * m / 2),
                Stroke = new SvgColourServer(Color.GreenYellow),
                StrokeWidth = 1,
            };
            measuresGroup.Children.Add(lastColumnLine);
            
            var firstRowLine = new SvgLine()
            {
                StartX = (float) -(margin * m / 4),
                EndX = (float) -(margin * m / 2),
                StartY = 0,
                EndY = 0,
                Stroke = new SvgColourServer(Color.GreenYellow),
                StrokeWidth = 1,
            };
            measuresGroup.Children.Add(firstRowLine);

            var lastRowLine = new SvgLine()
            {
                StartX = (float) -(margin * m / 4),
                EndX = (float) -(margin * m / 2),
                StartY = height * m,
                EndY = height * m,
                Stroke = new SvgColourServer(Color.GreenYellow),
                StrokeWidth = 1,
            };
            measuresGroup.Children.Add(lastRowLine);

            for (int i = 0; i < cooler.Columns; i++)
            {
                var columnLine = new SvgLine()
                {
                    StartX = (float) cooler.MarginLeft * m + (i * m * (float) cooler.HorizontalGap),
                    EndX = (float) cooler.MarginLeft * m + (i * m * (float) cooler.HorizontalGap),
                    StartY = (float) -(margin * m / 4),
                    EndY = (float) -(margin * m / 2),
                    Stroke = new SvgColourServer(Color.GreenYellow),
                    StrokeWidth = 1,
                };
                measuresGroup.Children.Add(columnLine);
            }

            for (int j = 0; j < cooler.Rows; j++)
            {
                var rowLine = new SvgLine()
                {
                    StartX = (float) -(margin * m / 4),
                    EndX = (float) -(margin * m / 2),
                    StartY = (float) cooler.MarginTop * m + (j * m * (float) cooler.VerticalGap),
                    EndY = (float) cooler.MarginTop * m + (j * m * (float) cooler.VerticalGap),
                    Stroke = new SvgColourServer(Color.GreenYellow),
                    StrokeWidth = 1,
                };
                measuresGroup.Children.Add(rowLine);
            }

            for (int i = 0; i < cooler.Columns; i++)
            {
                for (int j = 0; j < cooler.Rows; j++)
                {
                    circlesGroup.Children.Add(new SvgCircle
                    {
                        Radius = new SvgUnit((float) cooler.CapRatio * m),
                        CenterX = new SvgUnit((float) cooler.MarginLeft * m + (i * m * (float) cooler.HorizontalGap)),
                        CenterY = new SvgUnit((float) cooler.MarginTop * m + (j * m * (float) cooler.VerticalGap)),
                        Fill = new SvgColourServer(Color.Transparent),
                        Stroke = new SvgColourServer(Color.Black),
                        StrokeWidth = 1,
                    });
                }
            }

            svgDoc.Children.Add(new SvgRectangle()
            {
                Fill = new SvgColourServer(Color.Transparent),
                Stroke = new SvgColourServer(Color.Black),
                X = new SvgUnit(0),
                Y = new SvgUnit(0),
                Width = width * m,
                Height = height * m
            });

            var totalHorizontalLine = new SvgLine()
            {
                StartX = 0,
                EndX = width * m,
                StartY = (float) -(margin * m / 4 * 3),
                EndY = (float) -(margin * m / 4 * 3),
                Stroke = new SvgColourServer(Color.DarkBlue),
                StrokeWidth = 1,
            };
            measuresGroup.Children.Add(totalHorizontalLine);
            
            var totalHorizontalMeasure = new SvgText($"{cooler.Width}")
            {
                X = new SvgUnitCollection() {new SvgUnit((totalHorizontalLine.StartX + totalHorizontalLine.EndX) / 2f)},
                Y = new SvgUnitCollection() {new SvgUnit((totalHorizontalLine.StartY + totalHorizontalLine.EndY) / 2f)},
                FontSize = new SvgUnit(SvgUnitType.Em, 2),
                TextAnchor = SvgTextAnchor.Middle,
            };
            measuresGroup.Children.Add(totalHorizontalMeasure);
            
            var totalVerticalLine = new SvgLine()
            {
                StartY = 0,
                EndY = height * m,
                StartX = (float) -(margin * m / 4 * 3),
                EndX = (float) -(margin * m / 4 * 3),
                Stroke = new SvgColourServer(Color.DarkBlue),
                StrokeWidth = 1,
            };
            measuresGroup.Children.Add(totalVerticalLine);
            
            var totalVerticalMeasure = new SvgText($"{cooler.Height}")
            {
                X = new SvgUnitCollection() {new SvgUnit((totalVerticalLine.StartX + totalVerticalLine.EndX) / 2f)},
                Y = new SvgUnitCollection() {new SvgUnit((totalVerticalLine.StartY + totalVerticalLine.EndY) / 2f)},
                TextAnchor = SvgTextAnchor.Middle,
                FontSize = new SvgUnit(SvgUnitType.Em, 2),
            };
            measuresGroup.Children.Add(totalVerticalMeasure);

            return svgDoc;

            //var stream = new MemoryStream();
            //svgDoc.Write(stream);
            //textBox1.Text = Encoding.UTF8.GetString(stream.GetBuffer());

            //pictureBox1.Image = svgDoc.Draw();
        }
    }
}