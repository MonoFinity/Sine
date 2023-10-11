using System.Runtime.CompilerServices;

namespace NPlug.Sine;

public static class SinePlugin
{
    public static AudioPluginFactory GetFactory()
    {
        var factory = new AudioPluginFactory(new("NPlug", "https://github.com/monofinity/Sine", "monofinitygames@gmail.com"));
        factory.RegisterPlugin<SineProcessor>(new AudioProcessorClassInfo(SineProcessor.ClassId, "Sine", AudioProcessorCategory.Effect));
        factory.RegisterPlugin<SineController>(new AudioControllerClassInfo(SineController.ClassId, "Sine Controller"));
        return factory;
    }

    [ModuleInitializer]
    internal static void ExportThisPlugin()
    {
        AudioPluginFactoryExporter.Instance = GetFactory();
    }
}
