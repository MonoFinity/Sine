using System;


namespace NPlug.Sine
{
    public class SineWaveGenerator
    {
        private double frequency;
        private double sampleRate;
        private double phase;

        private Dictionary<AudioMidiControllerNumber, AudioParameter> midiToAudioParameterMap = new Dictionary<AudioMidiControllerNumber, AudioParameter>();


        public SineWaveGenerator(double sampleRate)
        {
            this.sampleRate = sampleRate;
            this.phase = 0;
        }

        public void SetFrequencyFromMidiNote(int midiNote)
        {
            this.frequency = 440.0 * Math.Pow(2, (midiNote - 69) / 12.0);
        }

        public float GenerateSample()
        {
            float sample = (float)Math.Sin(2 * Math.PI * frequency * phase / sampleRate);
            phase++;
            if (phase >= sampleRate)
            {
                phase -= sampleRate;
            }
            return sample;
        }
    }

    public class SineProcessor : AudioProcessor<SineModel>, IAudioControllerMidiMapping
    {
        public static readonly Guid ClassId = new();//what should I enter here?  how do I get a ClassId?

        private SineWaveGenerator sineWaveGenerator;
        private object midiToAudioParameterMap;

        public override Guid ControllerClassId => SineController.ClassId;

        int IAudioController.ParameterCount => throw new NotImplementedException();

        public SineProcessor() : base(AudioSampleSizeSupport.Float32)
        {
            sineWaveGenerator = new SineWaveGenerator(ProcessSetupData.SampleRate);
            midiToAudioParameterMap[AudioMidiControllerNumber.SomeNote] = new AudioParameter();
        }

        protected override void ProcessMain(in AudioProcessData data)
        {
            // Check for MIDI key press and set frequency

            // Get MIDI data


            data.Input.ParameterChanges.GetParameterData(0, out var midiControllerNumber, out var valueNormalized);

            if (TryGetMidiControllerAssignment(busIndex, channel, midiControllerNumber, out var audioParameterId))
            {
                // Do something with audioParameterId, like setting the frequency
                sineWaveGenerator.SetFrequencyFromMidiNote(audioParameterId.Value);
            }



            // Generate sine wave sample and add to output
            float sineSample = sineWaveGenerator.GenerateSample();

            // Add sineSample to the audio output buffer
            for (int i = 0; i < data.SampleCount; i++)
            {
                for (int j = 0; j < data.Output.ChannelCount; j++)
                {
                    ref float outputSample = ref data.Output.GetSample(j, i);
                    outputSample += sineSample;
                }
            }
        }



        public bool TryGetMidiControllerAssignment(int busIndex, int channel, AudioMidiControllerNumber midiControllerNumber, out AudioParameterId id)
        {
            if (midiToAudioParameterMap.TryGetValue(midiControllerNumber, out var parameter))
            {
                id = parameter.Id;
                return true;
            }
            id = default;
            return false;
        }

        void IAudioController.SetComponentState(Stream streamInput)
        {
            throw new NotImplementedException();
        }

        void IAudioController.SetState(Stream streamInput)
        {
            throw new NotImplementedException();
        }

        void IAudioController.GetState(Stream streamOutput)
        {
            throw new NotImplementedException();
        }

        AudioParameterInfo IAudioController.GetParameterInfo(int paramIndex)
        {
            throw new NotImplementedException();
        }

        string IAudioController.GetParameterStringByValue(AudioParameterId id, double valueNormalized)
        {
            throw new NotImplementedException();
        }

        double IAudioController.GetParameterValueByString(AudioParameterId id, string valueAsString)
        {
            throw new NotImplementedException();
        }

        double IAudioController.NormalizedParameterToPlain(AudioParameterId id, double valueNormalized)
        {
            throw new NotImplementedException();
        }

        double IAudioController.PlainParameterToNormalized(AudioParameterId id, double plainValue)
        {
            throw new NotImplementedException();
        }

        double IAudioController.GetParameterNormalized(AudioParameterId id)
        {
            throw new NotImplementedException();
        }

        void IAudioController.SetParameterNormalized(AudioParameterId id, double valueNormalized)
        {
            throw new NotImplementedException();
        }

        void IAudioController.SetControllerHandler(IAudioControllerHandler? controllerHandler)
        {
            throw new NotImplementedException();
        }

        IAudioPluginView? IAudioController.CreateView()
        {
            throw new NotImplementedException();
        }
    }
}