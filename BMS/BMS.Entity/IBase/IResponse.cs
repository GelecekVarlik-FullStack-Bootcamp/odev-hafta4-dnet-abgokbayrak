namespace BMS.Entity.IBase
{
    public interface IResponse
    {
        public string Message { get; }
        public int StatusCode { get; }
        public object Data { get; }
    }

    public interface IResponse<T> 
    {
        public string Message { get; }
        public int StatusCode { get; }
        public T Data { get; }
    }
}
