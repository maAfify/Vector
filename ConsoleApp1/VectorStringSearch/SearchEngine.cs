using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorStudyCase.src.Features;
using VectorStudyCase.src.Interfaces;

namespace VectorStudyCase.src
{
    public class SearchEngine<T>
        where T : IComparable
    {
        public List<T> SearchForPattern(List<IStartWithFeature<T>> InputList, IStartWithFeature<T> SearchInput)
        {
            List<T> Result = new List<T>(); 

            foreach (var input in InputList)
            {
                if ((bool)input.StartsWithImpl(SearchInput.Data!)!)
                {
                    Result.Add(input.Data!);
                }
            }
            return Result;
        }
    }
}
