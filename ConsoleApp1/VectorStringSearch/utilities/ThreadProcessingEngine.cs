using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using VectorStudyCase.src.Interfaces;

namespace VectorStudyCase.VectorStringSearch.utilities
{
    /// <summary>
    /// Utility class for managing thread data and performing multicore search operations.
    /// </summary>
    public class ThreadProcessingEngine
    {
        /// <summary>
        /// Creates a new thread and returns its handle.
        /// </summary>
        /// <param name="lpThreadAttributes">Thread attributes.</param>
        /// <param name="dwStackSize">Stack size for the new thread.</param>
        /// <param name="lpStartAddress">Pointer to the thread's starting function.</param>
        /// <param name="param">Parameter to be passed to the thread's starting function.</param>
        /// <param name="dwCreationFlags">Thread creation flags.</param>
        /// <param name="lpThreadId">ID of the newly created thread.</param>
        /// <returns>Handle to the created thread.</returns>
        [DllImport("kernel32.dll")]
        public static extern IntPtr CreateThread(
            IntPtr lpThreadAttributes, uint dwStackSize,
            IntPtr lpStartAddress, IntPtr param, uint dwCreationFlags, out uint lpThreadId);

        /// <summary>
        /// Waits until one or all of the specified objects are in the signaled state.
        /// </summary>
        /// <param name="nCount">Number of object handles in the array.</param>
        /// <param name="lpHandles">Array of object handles.</param>
        /// <param name="bWaitAll">True to wait for all objects, false to wait for any object.</param>
        /// <param name="dwMilliseconds">Time-out interval, in milliseconds.</param>
        /// <returns>An integer value indicating the result of the wait operation.</returns>
        [DllImport("kernel32.dll")]
        public static extern uint WaitForMultipleObjects(
            uint nCount, IntPtr[] lpHandles, bool bWaitAll, uint dwMilliseconds);

        /// <summary>
        /// Represents the data structure for a thread, including input list, search input, and result list.
        /// </summary>
        public struct ThreadData
        {
            /// <summary>
            /// The search input used for comparison.
            /// </summary>
            public ISearchStructure<string> SearchInput;

            /// <summary>
            /// The list of search structures to be processed by the thread.
            /// </summary>
            public List<ISearchStructure<string>> InputList;

            /// <summary>
            /// The list containing the search results.
            /// </summary>
            public List<string> ResultList;

            /// <summary>
            /// Initializes a new instance of the <see cref="ThreadData"/> structure.
            /// </summary>
            /// <param name="inputList">List of search structures to be processed.</param>
            /// <param name="searchInput">The search input used for comparison.</param>
            /// <param name="resultList">List to store the search results.</param>
            public ThreadData(List<ISearchStructure<string>> inputList, ISearchStructure<string> searchInput, List<string> resultList)
            {
                InputList = inputList;
                SearchInput = searchInput;
                ResultList = resultList;
            }
        }

        /// <summary>
        /// Performs multicore search pattern using the specified search function and input data.
        /// </summary>
        /// <param name="StringSearch">The search function to be executed in parallel.</param>
        /// <param name="InputList">List of search structures to be processed.</param>
        /// <param name="SearchInput">The search input used for comparison.</param>
        /// <returns>A list of search results.</returns>
        public List<string> MulticoreSearchPattern(Action<ThreadData> StringSearch, List<ISearchStructure<string>> InputList, ISearchStructure<string> SearchInput)
        {
            int numThreads = Environment.ProcessorCount;
            List<string> resultList = new List<string>();

            // Calculate items per thread
            int itemsPerThread = InputList.Count / numThreads;
            int remainingItems = InputList.Count % numThreads;

            // Create thread handles
            IntPtr[] threadHandles = new IntPtr[numThreads];

            // Start multiple threads for parallel searching
            for (int i = 0; i < numThreads; i++)
            {
                int startIndex = i * itemsPerThread + Math.Min(i, remainingItems);
                int endIndex = startIndex + itemsPerThread + (i < remainingItems ? 1 : 0);

                ThreadData threadData = new ThreadData(InputList.GetRange(startIndex, endIndex - startIndex), SearchInput, resultList);

                threadHandles[i] = CreateThread(IntPtr.Zero, 0, Marshal.GetFunctionPointerForDelegate(new ThreadStart(() => StringSearch(threadData))), IntPtr.Zero, 0, out _);
            }

            // Wait for all threads to complete
            WaitForMultipleObjects((uint)numThreads, threadHandles, true, uint.MaxValue);

            return resultList;
        }
    }
}