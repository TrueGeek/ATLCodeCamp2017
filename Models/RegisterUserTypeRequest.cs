using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class RegisterUserTypeRequest
    {
        public UserType UserType { get; set; }
        public string SickCode { get; set; }
    }
}
