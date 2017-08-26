namespace RestDotNet
{
    internal struct RestResponseMessage
    {
        public RestResponseMessage(bool success, string content)
        {
            IsSuccessStatusCode = success;
            Content = content;
        }

        public bool IsSuccessStatusCode { get; }

        public string Content { get; }
    }
}