namespace Lingva.BusinessLayer
{
    public class StorageOptions
    {
        public string Secret { get; set; }
        public string ServicesGoogleTranslaterKey { get; set; }
        public string ServicesYandexTranslaterKey { get; set; }
        public string ServicesYandexDictionaryKey { get; set; }

        public StorageOptions() { }
    }
}
