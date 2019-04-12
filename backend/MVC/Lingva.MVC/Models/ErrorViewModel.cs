using System.Diagnostics.CodeAnalysis;

namespace Lingva.MVC.Models
{
    [ExcludeFromCodeCoverage]
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public string Message { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public ErrorViewModel()
        {
            
        }

        public ErrorViewModel(string message)
        {
            Message = message;
        }
    }
}