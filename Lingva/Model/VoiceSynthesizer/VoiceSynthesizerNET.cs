using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Speech.Synthesis;
using System.IO;

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
            synthesizer.Volume = 100;  // (0 - 100)
            synthesizer.Rate = 0;     // (-10 - 10)
            FileStream fs = new FileStream("simple.wav", FileMode.Create, FileAccess.Write);
            synthesizer.SetOutputToWaveStream(fs);
            //synthesizer.SetOutputToWaveFile(@"C:\MyWavFile.wav");
            //synthesizer.SetOutputToWaveFile(Server.MapPath("~/path/to/file/") + fileName + ".wav");
            synthesizer.SpeakAsync(text);
        }
    }
}
