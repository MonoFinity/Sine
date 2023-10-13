namespace NPlug.Sine;

public class SineModel : AudioProcessorModel
{
    public SineModel() : base("NPlug.Sine")
    {
        AddByPassParameter();
        Delay = AddParameter(new AudioParameter("Volume", defaultNormalizedValue: 1.0));
    }

    public AudioParameter Delay { get; }
}
