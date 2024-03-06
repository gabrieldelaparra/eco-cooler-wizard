using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Svg;

//Move this out from Core.
namespace EcoCoolerWizard.Core
{
    public class CoolerDrawer
    {
        public string GetMarkupString(SvgDocument svgDocument)
        {
            var svgString = "";

            using (var stream = new MemoryStream()) {
                svgDocument.Write(stream);
                var text = Encoding.UTF8.GetString(stream.ToArray());
                var textList = text.Split(Environment.NewLine.ToArray(), StringSplitOptions.RemoveEmptyEntries);
                svgString = string.Join(Environment.NewLine, textList.Skip(2));
            }

            return svgString;
        }

        public SvgDocument Draw(Cooler cooler)
        {
            var width = (int) cooler.Width;
            var height = (int) cooler.Height;
            var margin = 4;
            var m = 50;
            var c1 = Color.Green;
            var c2 = Color.DarkBlue;
            var c3 = Color.Red;

            var svgDoc = new SvgDocument {
                Width = width * m,
                Height = height * m,
                CustomAttributes = {{"style", "background-color:white"}},
                ViewBox = new SvgViewBox(
                    -margin * m,
                    -margin * m,
                    width * m + margin * m * 2,
                    height * m + margin * m * 2)
            };

            //Frame
            svgDoc.Children.Add(new SvgRectangle {
                Fill = new SvgColourServer(Color.Transparent),
                Stroke = new SvgColourServer(Color.Black),
                X = new SvgUnit(0),
                Y = new SvgUnit(0),
                Width = width * m,
                Height = height * m
            });

            // Measurement Lines
            var measuresGroup = new SvgGroup();
            svgDoc.Children.Add(measuresGroup);

            var d1 = -(margin * m / 8 * 3);
            var d2 = -(margin * m / 8 * 5);
            var d3 = -(margin * m / 8 * 7);

            // Column / Horizontal Lines
            var sumDistances1 = Enumerable.Range(0, cooler.Columns).Select(x => x * cooler.HorizontalGap + cooler.MarginLeft).ToList();
            sumDistances1.Insert(0, 0);
            sumDistances1.Add(sumDistances1.Last() + cooler.MarginRight);

            var distances1 = Enumerable.Range(0, cooler.Columns - 1).Select(x => cooler.HorizontalGap).ToList();
            distances1.Insert(0, 0);
            distances1.Insert(1, cooler.MarginLeft);
            distances1.Add(cooler.MarginRight);

            for (var i = 0; i < sumDistances1.Count - 1; i++) {
                var x1 = sumDistances1[i] * m;
                var x2 = sumDistances1[i + 1] * m;
                measuresGroup.AddLineWithText(x1, x2, d1, d1, c3, $"{distances1[i + 1]}");
                measuresGroup.AddLineWithText(x1, x2, d2, d2, c1, $"{sumDistances1[i + 1]}");
            }

            measuresGroup.AddLineWithText(0, width * m, d3, d3, c2, $"{cooler.Width}");

            // Row / Vertical Lines
            var sumDistances2 = Enumerable.Range(0, cooler.Rows).Select(x => x * cooler.VerticalGap + cooler.MarginTop).ToList();
            sumDistances2.Insert(0, 0);
            sumDistances2.Add(sumDistances2.Last() + cooler.MarginBottom);

            var distances2 = Enumerable.Range(0, cooler.Rows - 1).Select(x => cooler.VerticalGap).ToList();
            distances2.Insert(0, 0);
            distances2.Insert(1, cooler.MarginTop);
            distances2.Add(cooler.MarginBottom);

            for (var i = 0; i < sumDistances2.Count - 1; i++) {
                var y1 = sumDistances2[i] * m;
                var y2 = sumDistances2[i + 1] * m;
                measuresGroup.AddLineWithText(d1, d1, y1, y2, c3, $"{distances2[i + 1]}");
                measuresGroup.AddLineWithText(d2, d2, y1, y2, c1, $"{sumDistances2[i + 1]}");
            }

            measuresGroup.AddLineWithText(d3, d3, 0, height * m, c2, $"{cooler.Height}");

            //Circles
            var circlesGroup = new SvgGroup();
            svgDoc.Children.Add(circlesGroup);

            for (var i = 0; i < cooler.Columns; i++) {
                for (var j = 0; j < cooler.Rows; j++) {
                    circlesGroup.Children.Add(new SvgCircle {
                        Radius = new SvgUnit((float) cooler.CapRatio * m / 2),
                        CenterX = new SvgUnit((float) cooler.MarginLeft * m + i * m * (float) cooler.HorizontalGap),
                        CenterY = new SvgUnit((float) cooler.MarginTop * m + j * m * (float) cooler.VerticalGap),
                        Fill = new SvgColourServer(Color.Transparent),
                        Stroke = new SvgColourServer(Color.Black),
                        StrokeWidth = 1
                    });
                }
            }

            var cx1 = cooler.MarginLeft * m + cooler.CapRatio * m / 2;
            var cx2 = cooler.MarginLeft * m - cooler.CapRatio * m / 2;
            var cy = cooler.MarginTop * m - cooler.CapRatio * m / 2 - 20;
            measuresGroup.AddLineWithText(cx1, cx2, cy, cy, Color.Black, $"{cooler.CapRatio}");

            return svgDoc;

            //var stream = new MemoryStream();
            //svgDoc.Write(stream);
            //textBox1.Text = Encoding.UTF8.GetString(stream.GetBuffer());

            //pictureBox1.Image = svgDoc.Draw();
        }
    }
}
