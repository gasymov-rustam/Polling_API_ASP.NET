namespace Shared.ServerResponse
{
    public class ServerResponse
    {
        public string Value { get; set; } = string.Empty;
        public bool Result { get; set; }
        public object? Data { get; set; }
        public ServerResponse(string value ) { Value = value; }
        public ServerResponse(string value, bool result) { Value = value; Result = result; }
        public ServerResponse(string value, bool result, object data) { Value = value; Result = result; Data = data; }
    }

}
