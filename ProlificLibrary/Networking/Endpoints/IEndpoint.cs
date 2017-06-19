namespace ProlificLibrary.Networking.Endpoints
{
    public interface IEndpoint
    {
        string Url { get; }
        
        HttpMethodEnum Method { get; }
        
        object Payload { get; }
    }
}