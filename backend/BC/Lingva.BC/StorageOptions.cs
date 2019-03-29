using System.Diagnostics.CodeAnalysis;

namespace Lingva.BC
{
    [ExcludeFromCodeCoverage]
    public class StorageOptions
    {
        public string ServicesGoogleTranslaterKey { get; set; }
        public string ServicesYandexTranslaterKey { get; set; }
        public string ServicesYandexDictionaryKey { get; set; }

        public StorageOptions() { }
    }
}
