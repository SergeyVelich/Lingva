namespace Lingva.Additional.Mapping.DataAdapter
{
    public interface IDataAdapter
    {
        DestinationType Map<DestinationType>(object source) where DestinationType : class;
        DestinationType Update<DestinationType>(DestinationType source, DestinationType destination) where DestinationType : class;
    }
}
