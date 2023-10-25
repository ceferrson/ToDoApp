

namespace ToDoAppNTIer.Common.Response
{
    public interface IResponse<T> : IResponse
    {
        T Data { get; set; }
        List<CustomValidationErrors> ValidationErrors { get; set; }
    }
}
