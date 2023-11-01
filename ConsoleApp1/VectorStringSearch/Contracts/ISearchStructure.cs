using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorStudyCase.src.Interfaces
{
    public interface ISearchStructure<T> 
        where T : IComparable
    {
        public T? Data { get; set; }
    }
}
