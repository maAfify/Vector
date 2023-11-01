using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorStudyCase.src.Interfaces
{
    public interface IStartWithFeature<T> where T : IComparable
    {
        public bool? StartsWithImpl(T instance);
        public T? Data { get; set; }
    }
}
