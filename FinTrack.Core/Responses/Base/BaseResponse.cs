using System.Text.Json.Serialization;

namespace FinTrack.Core.Responses.Base
{
    public class BaseResponse<TData>
    {
        [JsonConstructor]
        public BaseResponse() => _code = Configuration.DefaultStatusCode;

        public BaseResponse(TData? data, int code = Configuration.DefaultStatusCode, string? message = null)
        {
            Data = data;
            _code = code;
            Message = message;
        }

        private int _code = Configuration.DefaultStatusCode;

        [JsonIgnore]
        public bool IsSuccess => _code is >= 200 and <= 299;
        public string? Message { get; set; }
        public TData? Data { get; set; }
    }
}
