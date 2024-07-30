using System.ComponentModel.DataAnnotations;

namespace SpacexWebApp.Server.Data
{
    public class ApiRegisterResult
    {
        public bool Success { get; set; }

        public required string Message { get; set; }
    }
}
