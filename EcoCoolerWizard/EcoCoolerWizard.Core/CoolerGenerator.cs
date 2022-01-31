namespace eco_cooler_wizard.Core
{
    public class CoolerGeneratorInches : ICoolerGenerator
    {
        private double ratio = 1;
        private double horizontalGap = 5;
        private double verticalGap = 5;

        public Cooler GenerateCooler(double windowWidth, double windowHeight)
        {
            var hDivRem = Math.DivRem((int)windowWidth * 1000, (int)horizontalGap * 1000);
            var vDivRem = Math.DivRem((int)windowHeight * 1000, (int)verticalGap * 1000);

            var columns = hDivRem.Quotient;
            var rows = vDivRem.Quotient;

            var hMargin = Math.Round(windowWidth - (horizontalGap * (columns - 1)), 3);
            var vMargin = Math.Round(windowHeight - (verticalGap * (rows - 1)), 3);

            return new Cooler()
            {
                Width = windowWidth,
                Height = windowHeight,
                Columns = columns,
                Rows = rows,
                CapRatio = ratio,
                HorizontalGap = horizontalGap,
                VerticalGap = verticalGap,
                MarginLeft = (hMargin / 2),
                MarginRight = (hMargin / 2),
                MarginTop = (vMargin / 2),
                MarginBottom = (vMargin / 2),
            };
        }
    }
}