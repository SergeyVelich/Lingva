using System.Collections.Generic;

namespace Lingva.MVC.Models.Contracts
{
    public interface IHttpParametersSource
    {
        Dictionary<string, object> GetParametersDictionary();
    }
}
