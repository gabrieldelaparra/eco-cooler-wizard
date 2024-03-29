using System;
using System.IO;
using System.Text;
using EcoCoolerWizard.Core;
using NUnit.Framework;

namespace EcoCoolerWizard.UnitTests;

public class CoolerDrawerUnitTests
{
    [Test]
    public void TestConvertToSvg()
    {
        var generator = new CoolerGeneratorInches();
        var cooler = generator.GenerateCooler(131, 36);
        var drawer = new CoolerDrawer();
        var actual = drawer.Draw(cooler);

        var text = drawer.GetMarkupString(actual);
        
        File.WriteAllText($"EcoCoolerInches {DateTime.Now:yyyyMMdd-HHmmss}.svg", text);
        File.WriteAllText("EcoCoolerInches.svg", text);
    }
}
