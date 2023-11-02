using System;
using System.Collections.Generic;
using VectorStudyCase.src.Interfaces;
using VectorStudyCase.VectorStringSearch.Interfaces;
using VectorStudyCase.VectorStringSearch.utilities;

namespace VectorStudyCase.src
{
    /// <summary>
    /// Implements the <see cref="ISearchEngine{T}"/> interface for searching strings.
    /// </summary>
    public class StringSearchEngine : ISearchEngine<string>
    {
        /// <summary>
        /// Searches for a pattern in the input list using a trivial approach.
        /// </summary>
        /// <param name="InputList">List of search structures.</param>
        /// <param name="SearchInput">Search pattern.</param>
        /// <returns>List of matching results.</returns>
        public List<string> SearchForPatternTrivial(List<ISearchStructure<string>> InputList, ISearchStructure<string> SearchInput)
        {
            List<string> Result = new();

            foreach (var input in InputList)
            {
                if (input.Data?.StartsWith(SearchInput.Data!, StringComparison.OrdinalIgnoreCase) == true)
                {
                    Result.Add(input.Data);
                }
            }

            return Result;
        }

        /// <summary>
        /// Searches for a pattern in the input list using a non-trivial, multicore approach.
        /// </summary>
        /// <param name="InputList">List of search structures.</param>
        /// <param name="SearchInput">Search pattern.</param>
        /// <returns>List of matching results.</returns>
        public List<string> SearchForPatternNonTrivial(List<ISearchStructure<string>> InputList, ISearchStructure<string> SearchInput)
        {
            return new ThreadProcessingEngine().MulticoreSearchPattern(StringSearch, InputList, SearchInput);
        }

        /// <summary>
        /// Performs string search on the provided data using the specified thread data.
        /// </summary>
        /// <param name="data">Thread data containing input list, search pattern, and result list.</param>
        public static void StringSearch(ThreadProcessingEngine.ThreadData data)
        {
            ThreadProcessingEngine.ThreadData threadData = data;

            foreach (var input in threadData.InputList)
            {
                if (input.Data?.StartsWith(threadData.SearchInput.Data!, StringComparison.OrdinalIgnoreCase) == true)
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