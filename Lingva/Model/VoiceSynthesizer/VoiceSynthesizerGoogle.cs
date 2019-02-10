using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Lingva.Model
{
    public class VoiceSynthesizerGoogle : IVoiceSynthesizer
    {
        public void Synthesize(string text)
        {
            if (text.Length == 0)
            {
                return;
            }

            WebRequest request = WebRequest.Create("https://texttospeech.googleapis.com/v1beta1/text:synthesize?"
                + "key=AIzaSyAeW7Gac7Nu7CoIwi7oTi6GEfsibWLgrkw");
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

                string speechFile = Path.Combine(Directory.GetCurrentDirectory(), "sample.mp3");

                File.WriteAllBytes(speechFile, bytes);

                //// Write the response to the output file.
                //using (var output = File.Create("output.mp3"))
                //{
                //    audio.WriteTo(output);
                //}
            }

            return;
        }
    }
}
