using System;
using System.Drawing;
using Svg;

namespace EcoCoolerWizard.Core
{
    public static class SvgDocumentExtensions
    {
        public static SvgElement AddLineWithText(this SvgElement svgElement, double x1, double x2, double y1, double y2, Color color, string text)
        {
            var isHorizontal = Math.Abs(y1 - y2) < 0.05;
            var group = new SvgGroup();

            //Add Line
            var svgLine = new SvgLine {
                StartX = (float) x1,
                EndX = (float) x2,
                StartY = (float) y1,
                EndY = (float) y2,
                Stroke = new SvgColourServer(color),
                StrokeWidth = 1
            };

            group.Children.Add(svgLine);

            var svgText = new SvgText(text) {
                X = new SvgUnitCollection {new SvgUnit((svgLine.StartX + svgLine.EndX) / 2f)},
                Y = new SvgUnitCollection {new SvgUnit((svgLine.StartY + svgLine.EndY) / 2f)},
                FontSize = new SvgUnit(SvgUnitType.Em, 2),
                Fill = new SvgColourServer(color),
                TextAnchor = SvgTextAnchor.Middle ,
                FontFamily = "Verdana",
                CustomAttributes = {{"alignment-baseline", "middle"}},
            };

            group.Children.Add(svgText);

            //Add Line endings
            var size = 20;

            var endLine1 = new SvgLine {
                StartX = (float) (isHorizontal ? x1 : x1 - size / 2),
                EndX = (float) (isHorizontal ? x1 : x1 + size / 2),
                StartY = (float) (isHorizontal ? y1 - size / 2 : y1),
                EndY = (float) (isHorizontal ? y1 + size / 2 : y1),
                Stroke = new SvgColourServer(color),
                StrokeWidth = 1
            };

            group.Children.Add(endLine1);

            var endLine2 = new SvgLine {
                StartX = (float) (isHorizontal ? x2 : x2 - size / 2),
                EndX = (float) (isHorizontal ? x2 : x2 + size / 2),
                StartY = (float) (isHorizontal ? y2 - size / 2 : y2),
                EndY = (float) (isHorizontal ? y2 + size / 2 : y2),
                Stroke = new SvgColourServer(color),
                StrokeWidth = 1
            };

            group.Children.Add(endLine2);

            svgElement.Children.Add(group);
            return svgElement;
        }
    }
}
