namespace DemoApi.Dtos
{
    public interface IBaseCommandResult
    {
        object ResponseDataObj { get; set; }

        string Message { get; set; }

        bool Success { get; set; }
    }
    public class BaseCommandResult :IBaseCommandResult
    {
        public BaseCommandResult(bool success, string message, object responseDataObj)
        {
            Success = success;
            Message = message;
            ResponseDataObj = responseDataObj;
        }

        public BaseCommandResult() { }

        public object ResponseDataObj { get; set; }

        public string Message { get; set; }

        public bool Success { get; set; }
    }
}