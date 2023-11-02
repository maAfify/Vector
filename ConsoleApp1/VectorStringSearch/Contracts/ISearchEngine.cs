using VectorStudyCase.src.Interfaces;

namespace VectorStudyCase.VectorStringSearch.Interfaces
{
    public interface ISearchEngine<T>
        where T : IComparable
    {
        public List<T> SearchForPatternTrivial(List<ISearchStructure<T>> InputList, ISearchStructure<T> SearchInput);
        public List<T> SearchForPatternNonTrivial(List<ISearchStructure<T>> InputList, ISearchStructure<T> SearchInput);

    }
}