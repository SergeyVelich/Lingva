using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Speech.Synthesis;

namespace Lingva.Model
{
    public class VoiceSynthesizerNET : IVoiceSynthesizer
    {
        public void Synthesize(string text)
        {
            if (text.Length == 0)
            {
                return;
            }

            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            System.Diagnostics.Debug.WriteLine(synthesizer.GetInstalledVoices().Count());
            synthesizer.SetOutputToDefaultAudioDevice();
            var t = synthesizer.GetInstalledVoices();
            synthesizer.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Adult); // to change VoiceGender and VoiceAge check out those links below
            synthesizer.Volume = 100;  // (0 - 100)
            synthesizer.Rate = 0;     // (-10 - 10)

            //speechSynthesizer.SetOutputToWaveFile(Server.MapPath("~/path/to/file/") + fileName + ".wav");
            //speechSynthesizer.Speak(text);

            synthesizer.SpeakAsync(text); // here args = pran
        }
    }
}
