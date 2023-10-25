

namespace ToDoAppNTIer.Common.Response
{
    public class Response<T> : Response, IResponse<T>
    {
        public Response(ResponseType responseType, T data) : base(responseType)
        {
            Data = data;
        }

        //For Not Found Version
        public Response(ResponseType responseType, string message) : base(responseType, message)
        {
        }

        public Response(ResponseType responseType, List<CustomValidationErrors> errors, T data) : base(responseType)
        {
            ValidationErrors = errors;
            Data = data;
        }

        public T Data { get; set; }
        public List<CustomValidationErrors> ValidationErrors { get; set; }
    }
}
