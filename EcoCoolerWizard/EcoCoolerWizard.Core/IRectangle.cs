namespace EcoCoolerWizard.Core
{
    public interface IRectangle
    {
        MeasureUnit MeasureUnit { get; set; }
        double Width { get; set; }
        double Height { get; set; }
    }
}
