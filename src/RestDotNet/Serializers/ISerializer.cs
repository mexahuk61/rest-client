namespace RestDotNet.Serializers
{
    public interface ISerializer
    {
        string MediaType { get; }

        string Serialize(object value);
    }
}