using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace SenderService.SettingsProvider.Core.Entities
{
    [ExcludeFromCodeCoverage]
    public class EmailTemplate
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string ParametersString { get; set; }
        public string[] Parameters
        {
            get
            {
                return ParametersString.Split(';');
            }
            set
            {
                ParametersString = String.Join(";", value.Select(p => p.ToString()).ToArray());
            }
        }       
    }
}
