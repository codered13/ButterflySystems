namespace ButterflySystems.Api.Core.Models
{
    public class ActionResponse
    {
        public ActionResponse() { }

        public ActionResponse(ActionResponseType status) : this(null, null, null, status)
        {
        }

        public ActionResponse(object error, ActionResponseType status) : this(null, error, null, status)
        {
        }
        public ActionResponse(string message, ActionResponseType status) : this(message, null, null, status)
        {
        }

        public ActionResponse(string message, object error, ActionResponseType status) : this(message, error, null, status)
        {
        }

        public ActionResponse(string message, object errors, object data, ActionResponseType status)
        {
            Message = message;
            Errors = errors;
            Data = data;
            Status = status;
        }

        public string Message { get; set; }
        public object Errors { get; set; }
        public object Data { get; set; }
        public ActionResponseType Status { get; set; } = ActionResponseType.InternalServerError;
    }

    public enum ActionResponseType
    {
        Ok,
        Created,
        Updated,
        Conflict,
        NoContent,
        NotFound,
        UnAuthorized,
        InternalServerError
    }
}
