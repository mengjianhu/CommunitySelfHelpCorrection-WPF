using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceReturnEntity
{
    public class Result<T>
    {
        public int code { get; set; }
        public string msg { get; set; }
        public List<T> rows { get; set; }
        public bool ok { get; set; }
        public bool fail { get; set; }
    }
}
