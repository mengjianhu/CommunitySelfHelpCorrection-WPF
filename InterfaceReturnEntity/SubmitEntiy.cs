using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceReturnEntity
{
    public class SubmitEntiy
    {
        public string termerId { get; set; }
        public Nullable<int> pageNumber { get; set; }
        public Nullable<int> pageSize { get; set; }
    }
}
