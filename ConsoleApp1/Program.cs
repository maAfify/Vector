// See https://aka.ms/new-console-template for more information


using VectorStudyCase.src;

using VectorStudyCase.VectorStringSearch.Features;
using VectorStudyCase.VectorStringSearch.Interfaces;

ISearchEngine<string> searchEngine = new StringSearchEngine();

ISearchEngine<int> IntegerSearchEngine = new IntegerSearchEngine();




