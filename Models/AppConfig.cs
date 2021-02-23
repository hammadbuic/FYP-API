using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Academic_project_manager_WebAPI.Models
{
    public class AppConfig
    {
        public string JWTSecret { get; set; }
        public string Client_URL { get; set; }
    }
}
