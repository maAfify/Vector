using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using VectorStudyCase.src;
using VectorStudyCase.src.Features;
using VectorStudyCase.src.Interfaces;
using VectorStudyCase.VectorStringSearch.Interfaces;

namespace VectorStudyCase.Tests
{
    [TestFixture]
    public class StringSearchEngineTests
    {
        private ISearchEngine<string> _searchEngine;
        private List<ISearchStructure<string>> _inputList;
        private List<ISearchStructure<string>> _randomInputList;
        private Stopwatch _stopwatch;

        [SetUp]
        public void Setup()
        {
            _searchEngine = new StringSearchEngine();
            InitializeInputLists();
            GenerateAndShuffleRandomInputList();
            _stopwatch = new Stopwatch();
        }

        private void InitializeInputLists()
        {
            _inputList = new List<ISearchStructure<string>>
            {
                new StringSearchedStructure("AAAA"),
                new StringSearchedStructure("ACZM"),
                new StringSearchedStructure("ANDY"),
                new StringSearchedStructure("AAAA"),
                new StringSearchedStructure("ACZM"),
                new StringSearchedStructure("ANDY")
            };
        }

        private void GenerateAndShuffleRandomInputList()
        {
            _randomInputList = new List<ISearchStructure<string>>();
            Random rng = new Random();

            for (char firstLetter = 'A'; firstLetter <= 'Z'; firstLetter++)
            {
                for (char secondLetter = 'A'; secondLetter <= 'Z'; secondLetter++)
                {
                    for (char thirdLetter = 'A'; thirdLetter <= 'Z'; thirdLetter++)
                    {
                        for (char fourthLetter = 'A'; fourthLetter <= 'Z'; fourthLetter++)
                        {
                            string combination = $"{firstLetter}{secondLetter}{thirdLetter}{fourthLetter}";
                            _randomInputList.Add(new StringSearchedStructure(combination));
                        }
                    }
                }
            }

            int n = _randomInputList.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                var value = _randomInputList[k];
                _randomInputList[k] = _randomInputList[n];
                _randomInputList[n] = value;
            }
        }

        [Test]
        public void SearchForPatternNonTrivial_WhenPatternExists_ReturnsExpectedResults()
        {
            var searchPattern = "AN";

            _stopwatch.Start();
            var testResults = _searchEngine.SearchForPatternNonTrivial(_inputList, new StringSearchedStructure(searchPattern));
            _stopwatch.Stop();

            testResults.ForEach((result) => Console.Write(result + " "));
            CollectionAssert.AreNotEqual(new[] { "ANDY" }, testResults);
            Console.WriteLine($"\nElapsed time in microseconds: {_stopwatch.ElapsedTicks / 10.0}");
        }

        [Test]
        public void SearchForPatternTrivial_WhenPatternExists_ReturnsExpectedResults()
        {
            var searchPattern = "AN";

            _stopwatch.Start();
            var testResults = _searchEngine.SearchForPatternTrivial(_inputList, new StringSearchedStructure(searchPattern));
            _stopwatch.Stop();

            testResults.ForEach((result) => Console.Write(result + " "));
            CollectionAssert.AreEqual(new[] { "ANDY", "ANDY" }, testResults);
            Console.WriteLine($"\nElapsed time in microseconds: {_stopwatch.ElapsedTicks / 10.0}");
        }

        [Test]
        public void SearchForPatternNonTrivial_WhenPatternDoesNotExist_ReturnsEmptyResults()
        {
            var searchPattern = "MH";
            _stopwatch.Start();
            var testResults = _searchEngine.SearchForPatternNonTrivial(_inputList, new StringSearchedStructure(searchPattern));
            _stopwatch.Stop();

            testResults.ForEach((result) => Console.Write(result + " "));
            CollectionAssert.IsEmpty(testResults);
            Console.WriteLine($"\nElapsed time in microseconds: {_stopwatch.ElapsedTicks / 10.0}");
        }

        [Test]
        public void SearchForPatternNonTrivial_WithEmptyInputList_ReturnsEmptyResults()
        {
            _inputList.Clear();
            _stopwatch.Start();
            var testResults = _searchEngine.SearchForPatternNonTrivial(_inputList, new StringSearchedStructure("MH"));
            _stopwatch.Stop();

            testResults.ForEach((result) => Console.Write(result + " "));
            CollectionAssert.IsEmpty(testResults);
            Console.WriteLine($"\nElapsed time in microseconds: {_stopwatch.ElapsedTicks / 10.0}");
        }

        [Test]
        public void SearchForPatternNonTrivial_WithRandomPattern_ReturnsExpectedResult()
        {
            var random = new Random();
            int randomIndex = random.Next(0, _randomInputList.Count);
            var randomElement = _randomInputList[randomIndex];

            _stopwatch.Start();
            var testResults = _searchEngine.SearchForPatternNonTrivial(_randomInputList, randomElement);
            _stopwatch.Stop();

            testResults.ForEach((result) => Console.Write(result + " "));
            CollectionAssert.AreEqual(new[] { randomElement.Data! }, testResults);
            Console.WriteLine($"\nElapsed time in microseconds: {_stopwatch.ElapsedTicks / 10.0}");
        }

        [Test]
        public void SearchForPatternTrivial_WithRandomPattern_ReturnsExpectedResult()
        {
            var random = new Random();
            int randomIndex = random.Next(0, _randomInputList.Count);
            var randomElement = _randomInputList[randomIndex];

            _stopwatch.Start();
            var testResults = _searchEngine.SearchForPatternTrivial(_randomInputList, randomElement);
            _stopwatch.Stop();

            testResults.ForEach((result) => Console.Write(result + " "));
            CollectionAssert.AreEqual(new[] { randomElement.Data! }, testResults);
            Console.WriteLine($"\nElapsed time in microseconds: {_stopwatch.ElapsedTicks / 10.0}");
        }

        [Test]
        public void SearchForPatternTrivial2_WithRandomPattern_ReturnsExpectedResult()
        {
            var random = new Random();
            int randomIndex = random.Next(0, _randomInputList.Count);
            var randomElement = _randomInputList[randomIndex];

            _stopwatch.Start();
            var testResults = _searchEngine.SearchForPatternTrivial(_randomInputList, randomElement);
            _stopwatch.Stop();

            testResults.ForEach((result) => Console.Write(result + " "));
            CollectionAssert.AreEqual(new[] { randomElement.Data! }, testResults);
            Console.WriteLine($"\nElapsed time in microseconds: {_stopwatch.ElapsedTicks / 10.0}");
        }
    }
}