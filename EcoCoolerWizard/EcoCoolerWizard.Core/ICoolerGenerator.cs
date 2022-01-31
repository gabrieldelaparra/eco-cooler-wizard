namespace eco_cooler_wizard.Core
{
    public interface ICoolerGenerator
    {
        Cooler GenerateCooler(double windowWidth, double windowHeight);
    }
}