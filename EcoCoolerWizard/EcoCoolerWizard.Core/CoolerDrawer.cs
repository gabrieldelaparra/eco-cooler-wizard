using System.Drawing;
using Svg;

//Move this out from Core.
namespace EcoCoolerWizard.Core
{
    public static class SvgDocumentExtensions
    {
        public static SvgElement AddLine(this SvgElement svgElement, double x1, double x2, double y1, double y2,
                                         Color color)
        {
            var line = new SvgLine {
                StartX = (float) x1,
                EndX = (float) x2,
                StartY = (float) y1,
                EndY = (float) y2,
                Stroke = new SvgColourServer(color),
                StrokeWidth = 1
            };

            svgElement.Children.Add(line);
            return svgElement;
        }

        public static SvgElement AddLineWithText(this SvgElement svgElement, double x1, double x2, double y1, double y2, Color color, string text)
        {
            var svgLine = new SvgLine {
                StartX = (float) x1,
                EndX = (float) x2,
                StartY = (float) y1,
                EndY = (float) y2,
                Stroke = new SvgColourServer(color),
                StrokeWidth = 1
            };

            svgElement.Children.Add(svgLine);

            var svgText = new SvgText(text) {
                X = new SvgUnitCollection {new SvgUnit((svgLine.StartX + svgLine.EndX) / 2f)},
                Y = new SvgUnitCollection {new SvgUnit((svgLine.StartY + svgLine.EndY) / 2f)},
                FontSize = new SvgUnit(SvgUnitType.Em, 2),
                Fill = new SvgColourServer(color),
                TextAnchor = SvgTextAnchor.Middle,
                CustomAttributes = { { "alignment-baseline", "middle" } }
            };

            svgElement.Children.Add(svgText);

            return svgElement;
        }
    }

    public class CoolerDrawer
    {
        public SvgDocument Draw(Cooler cooler)
        {
            var width = (int) cooler.Width;
            var height = (int) cooler.Height;
            var margin = 4;
            var m = 50;
            var c1 = Color.Green;
            var c2 = Color.DarkBlue;

            var svgDoc = new SvgDocument {
                Width = width * m,
                Height = height * m,
                CustomAttributes = { { "style", "background-color:white" } },
                ViewBox = new SvgViewBox(
                    -margin * m,
                    -margin * m,
                    width * m + margin * m * 2,
                    height * m + margin * m * 2)
            };

            var circlesGroup = new SvgGroup();
            svgDoc.Children.Add(circlesGroup);

            var measuresGroup = new SvgGroup();
            svgDoc.Children.Add(measuresGroup);

            // First + Last Column Line
            measuresGroup.AddLine(0, 0, -(margin * m / 4), -(margin * m / 2), c1);
            measuresGroup.AddLine(width * m, width * m, -(margin * m / 4), -(margin * m / 2), c1);
            measuresGroup.AddLine(0, width * m, -(margin * m / 8 * 3), -(margin * m / 8 * 3), c1);

            // First + Last Row Line
            measuresGroup.AddLine(-(margin * m / 4), -(margin * m / 2), 0, 0, c1);
            measuresGroup.AddLine(-(margin * m / 4), -(margin * m / 2), height * m, height * m, c1);
            measuresGroup.AddLine(-(margin * m / 8 * 3), -(margin * m / 8 * 3), 0, height * m, c1);

            // Column Lines
            for (var i = 0; i < cooler.Columns; i++) {
                var x1 = cooler.MarginLeft * m + i * m * cooler.HorizontalGap;
                var x2 = cooler.MarginLeft * m + i * m * cooler.HorizontalGap;
                measuresGroup.AddLine(x1, x2, -(margin * m / 4), -(margin * m / 2), c1);
            }

            // Row Lines
            for (var j = 0; j < cooler.Rows; j++) {
                var y1 = cooler.MarginTop * m + j * m * cooler.VerticalGap;
                var y2 = cooler.MarginTop * m + j * m * cooler.VerticalGap;
                measuresGroup.AddLine(-(margin * m / 4), -(margin * m / 2), y1, y2, c1);
            }

            for (var i = 0; i < cooler.Columns; i++) {
                for (var j = 0; j < cooler.Rows; j++) {
                    circlesGroup.Children.Add(new SvgCircle {
                        Radius = new SvgUnit((float) cooler.CapRatio * m),
                        CenterX = new SvgUnit((float) cooler.MarginLeft * m + i * m * (float) cooler.HorizontalGap),
                        CenterY = new SvgUnit((float) cooler.MarginTop * m + j * m * (float) cooler.VerticalGap),
                        Fill = new SvgColourServer(Color.Transparent),
                        Stroke = new SvgColourServer(Color.Black),
                        StrokeWidth = 1
                    });
                }
            }

            svgDoc.Children.Add(new SvgRectangle {
                Fill = new SvgColourServer(Color.Transparent),
                Stroke = new SvgColourServer(Color.Black),
                X = new SvgUnit(0),
                Y = new SvgUnit(0),
                Width = width * m,
                Height = height * m
            });

            measuresGroup.AddLineWithText(0, width * m, -(margin * m / 8 * 6), -(margin * m / 8 * 6), c2, $"{cooler.Width}");
            measuresGroup.AddLineWithText(-(margin * m / 8 * 6), -(margin * m / 8 * 6), 0, height * m, c2, $"{cooler.Height}");

            return svgDoc;

            //var stream = new MemoryStream();
            //svgDoc.Write(stream);
            //textBox1.Text = Encoding.UTF8.GetString(stream.GetBuffer());

            //pictureBox1.Image = svgDoc.Draw();
        }
    }
}
