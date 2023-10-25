
namespace ToDoAppNTIer.Common.Response
{
    public interface IResponse
    {
        string Message { get; set; }
        ResponseType ResponseType { get; set; }
    }
}
