namespace EcoCoolerWizard.Core
{
    public class Cooler : IRectangle
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public double HorizontalGap { get; set; } = 5;
        public double VerticalGap { get; set; } = 5;
        public double CapRatio { get; set; } = 1;
        public double MarginLeft { get; set; }
        public double MarginRight { get; set; }
        public double MarginTop { get; set; }
        public double MarginBottom { get; set; }
        public MeasureUnit MeasureUnit { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }
}
