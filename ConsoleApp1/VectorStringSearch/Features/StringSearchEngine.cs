using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorStudyCase.src.Features;
using System.Runtime.InteropServices;
using VectorStudyCase.src.Interfaces;
using VectorStudyCase.VectorStringSearch.Interfaces;
using VectorStudyCase.VectorStringSearch.utilities;

namespace VectorStudyCase.src
{
    public class StringSearchEngine : ISearchEngine<string>
    {
        public List<string> SearchForPatternTrivial(List<ISearchStructure<string>> InputList, ISearchStructure<string> SearchInput)
        {
            List<string> Result = new(); 

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
            return new ThreadDataDetails().MulticoreSearchPattern(StringSearch, InputList, SearchInput);
        }

        public static void StringSearch(ThreadDataDetails.ThreadData data)
        {
            ThreadDataDetails.ThreadData threadData = data;

            foreach (var input in threadData.InputList)
            {
                if (input.Data!.StartsWith(threadData.SearchInput.Data!))
                {
                    lock (threadData.ResultList)
                    {
                        threadData.ResultList.Add(input.Data);
                    }
                }
            }
        }

    }
}
