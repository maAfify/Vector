using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorStudyCase.src.Interfaces;
using VectorStudyCase.VectorStringSearch.Interfaces;

namespace VectorStudyCase.VectorStringSearch.Features
{
    public class IntegerSearchEngine : ISearchEngine<int>
    {
        public List<int> SearchForPattern(List<ISearchStructure<int>> InputList, ISearchStructure<int> SearchInput)
        {
            throw new NotImplementedException();
        }

        public List<int> SearchForPatternNonTrivial(List<ISearchStructure<int>> InputList, ISearchStructure<int> SearchInput)
        {
            throw new NotImplementedException();
        }
    }
}
