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
        public string Translate(string text, Language originalLanguage, Language translationLanguage)
        {
            if (text.Length == 0)
            {
                return "";
            }

            WebRequest request = WebRequest.Create("https://translation.googleapis.com/language/translate/v2/?"
                + "key=AIzaSyAeW7Gac7Nu7CoIwi7oTi6GEfsibWLgrkw"//?? to find later
                + "&q=" + text
                + "&source=" + originalLanguage.Name
                + "&target=" + translationLanguage.Name);

            WebResponse response = request.GetResponse();

            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                string line = stream.ReadToEnd();
                dynamic translation = JsonConvert.DeserializeObject<dynamic>(line);
                text = translation.data.translations[0].translatedText;
            }
            return text;
        }
    }
}
