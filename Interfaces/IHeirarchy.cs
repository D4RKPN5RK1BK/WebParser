using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebParser.Interfaces
{
    internal interface IHeirarchy<T>
    {
        public T Parent { get; set; }
        public IEnumerable<T> Childrens { get; set; }
    }
}
