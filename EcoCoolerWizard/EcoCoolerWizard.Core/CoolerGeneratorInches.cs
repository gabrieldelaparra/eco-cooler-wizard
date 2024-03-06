using System;

namespace EcoCoolerWizard.Core
{
    public class CoolerGeneratorInches : ICoolerGenerator
    {
        private const double Ratio = 1;
        private const double HorizontalGap = 5;
        private const double VerticalGap = 5;
        private const MeasureUnit MeasureUnit = Core.MeasureUnit.Imperial;

        public Cooler GenerateCooler(double windowWidth, double windowHeight)
        {
            var columns = (int) windowWidth / (int) HorizontalGap;
            var rows = (int) windowHeight / (int) VerticalGap;

            var hMargin = Math.Round(windowWidth - HorizontalGap * (columns - 1), 3);
            var vMargin = Math.Round(windowHeight - VerticalGap * (rows - 1), 3);

            return new Cooler {
                MeasureUnit = MeasureUnit,
                Width = windowWidth,
                Height = windowHeight,
                Columns = columns,
                Rows = rows,
                CapRatio = Ratio,
                HorizontalGap = HorizontalGap,
                VerticalGap = VerticalGap,
                MarginLeft = hMargin / 2,
                MarginRight = hMargin / 2,
                MarginTop = vMargin / 2,
                MarginBottom = vMargin / 2
            };
        }
    }
}
