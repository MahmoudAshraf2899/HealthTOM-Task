using System.Text.Json.Serialization;

namespace Boilerplate.Infrastructure.Extentions
{
    public class ErrorDetails
    {
        [JsonPropertyName("status_code")]
        public int StatusCode { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("error")]
        public string Error { get; set; }

        [JsonPropertyName("exception")]
        public string Exception { get; set; }

        /// <summary>
        /// Gets the validation errors associated with this instance of <see cref="T:Microsoft.AspNetCore.Mvc.ValidationProblemDetails" />.
        /// </summary>
        [JsonPropertyName("errors")]
        public Dictionary<string, string> Errors { get; set; }
    }
}
