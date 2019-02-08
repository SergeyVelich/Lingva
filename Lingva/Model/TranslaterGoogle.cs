using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Lingva.Model
{
    public class TranslaterGoogle : ITranslater
    {
        public string Translate(string text, int originalLanguage, int translationLanguage)
        {
            if (text.Length == 0)
            {
                return "";
            }

            WebRequest request = WebRequest.Create("//https://translation.googleapis.com/language/translate/v2/?"
                + "&q=" + text
                + "&source=" + Enum.GetName(typeof(Languages), originalLanguage)
                + "&target=" + Enum.GetName(typeof(Languages), translationLanguage)
                + "key=trnsl.1.1.20170125T084253Z.cc366274cc3474e9.68d49c802348b39b5d677c856e0805c433b7618c");//?? to find later

            WebResponse response = request.GetResponse();

            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                string line;
                if ((line = stream.ReadLine()) != null)
                {
                    Translation translation = JsonConvert.DeserializeObject<Translation>(line);
                    text = "";
                    foreach (string str in translation.text)
                    {
                        text += str;
                    }
                }
            }
            return text;
        }

        struct Translation
        {
            public string code { get; set; }
            public string lang { get; set; }
            public string[] text { get; set; }
        }
    }
}
