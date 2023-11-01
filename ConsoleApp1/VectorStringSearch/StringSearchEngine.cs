using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorStudyCase.src.Features;
using VectorStudyCase.src.Interfaces;
using VectorStudyCase.VectorStringSearch.Interfaces;

namespace VectorStudyCase.src
{
    public class StringSearchEngine : ISearchEngine<string> 
    {
        public List<string> SearchForPattern(List<ISearchStructure<string>> InputList, ISearchStructure<string> SearchInput)
        {
            List<string> Result = new List<string>(); 

            foreach (var input in InputList)
            {
                if (input.Data!.StartsWith(SearchInput.Data!)!)
                {
                    Result.Add(input.Data!);
                }
            }
            return Result;
        }

        public List<string> SearchForPatternNonTrivial(List<ISearchStructure<string>> InputList, ISearchStructure<string> SearchInput)
        {
            throw new NotImplementedException();
        }
    }
}
