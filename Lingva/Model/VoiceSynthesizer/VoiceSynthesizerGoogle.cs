using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Text;

namespace Lingva.Model
{
    public class VoiceSynthesizerGoogle : IVoiceSynthesizer
    {
        private readonly string _serviceKey;

        public VoiceSynthesizerGoogle(string serviceKey)
        {
            _serviceKey = serviceKey;
        }

        public void Synthesize(string text)
        {
            if (text.Length == 0)
            {
                return;
            }

            WebRequest request = WebRequest.Create("https://texttospeech.googleapis.com/v1beta1/text:synthesize?"
                + "key=" + _serviceKey);
            request.ContentType = "application/json";
            request.Method = "POST";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = "{ 'input':{ 'text':'Android is a mobile operating system developed by Google, based on the Linux kernel and designed primarily for touchscreen mobile devices such as smartphones and tablets.' }, 'voice':{ 'languageCode':'en-gb', 'name':'en-GB-Standard-A', 'ssmlGender':'FEMALE' }, 'audioConfig':{ 'audioEncoding':'MP3' }}";
                streamWriter.Write(json);
            }

            WebResponse response = request.GetResponse();

            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                string line = stream.ReadToEnd();
                dynamic translation = JsonConvert.DeserializeObject<dynamic>(line);
                string audio = translation.audioContent;
                byte[] bytes = Encoding.UTF8.GetBytes(audio);

                string speechFile = Path.Combine(Directory.GetCurrentDirectory(), "sample.wav");

                File.WriteAllBytes(speechFile, bytes);
            }

            return;


            //GoogleCredential credentials =
            //GoogleCredential.FromFile(Path.Combine(Program.AppPath, "jhabjan-test-47a56894d458.json"));

            //TextToSpeechClient client = TextToSpeechClient.Create(credentials);

            //SynthesizeSpeechResponse response = client.SynthesizeSpeech(
            //    new SynthesisInput()
            //    {
            //        Text = "Google Cloud Text-to-Speech enables developers to synthesize natural-sounding speech with 32 voices"
            //    },
            //    new VoiceSelectionParams()
            //    {
            //        LanguageCode = "en-US",
            //        Name = "en-US-Wavenet-C"
            //    },
            //    new AudioConfig()
            //    {
            //        AudioEncoding = AudioEncoding.Mp3
            //    }
            //);

            //string speechFile = Path.Combine(Directory.GetCurrentDirectory(), "sample.mp3");

            //File.WriteAllBytes(speechFile, response.AudioContent);





            //TextToSpeechClient client = TextToSpeechClient.Create();
            //var response = client.SynthesizeSpeech(new SynthesizeSpeechRequest
            //{
            //    Input = new SynthesisInput
            //    {
            //        Text = text
            //    },
            //    // Note: voices can also be specified by name
            //    Voice = new VoiceSelectionParams
            //    {
            //        LanguageCode = "en-US",
            //        SsmlGender = SsmlVoiceGender.Female
            //    },
            //    AudioConfig = new AudioConfig
            //    {
            //        AudioEncoding = AudioEncoding.Mp3
            //    }
            //});

            //using (Stream output = File.Create("output.mp3"))
            //{
            //    response.AudioContent.WriteTo(output);
            //}

        }
    }
}
