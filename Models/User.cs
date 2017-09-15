using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class User
    {
        public string Email { get; set; }
        public UserType UserType { get; set; }
        public string SickCode { get; set; }
        public string AssociatedSickUserId { get; set; }
        public string AssociatedSickUserEmail { get; set; }
    }
}
