using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class TokenConfigurationDTO
    {
        public string? ValidIssuer { get; set; }
        public string? ValidAudience { get; set; }
        public string? SecretKey { get; set; }
    }
}
