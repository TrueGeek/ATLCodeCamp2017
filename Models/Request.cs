using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Request
    {
        public Guid Id { get; set; }

        public string RequestedByUserId { get; set; }
        public DateTime RequestedOn { get; set; }

        public string FullfilledByUserId { get; set; }
        public DateTime? FullfilledOn { get; set; }
    }
}
