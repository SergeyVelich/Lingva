using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingva.Model
{
    interface ITranslater
    {
        string Translate(string text, int originalLanguage, int translationLanguage);
    }
}
