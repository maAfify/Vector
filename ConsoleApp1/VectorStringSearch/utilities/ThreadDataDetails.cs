using System.Runtime.InteropServices;
using System.Threading;
using VectorStudyCase.src.Interfaces;

namespace VectorStudyCase.VectorStringSearch.utilities
{
    public class ThreadDataDetails
    {

        [DllImport("kernel32.dll")]
        public static extern IntPtr CreateThread(
        IntPtr lpThreadAttributes, uint dwStackSize,
        IntPtr lpStartAddress, IntPtr param, uint dwCreationFlags, out uint lpThreadId);

        [DllImport("kernel32.dll")]
        public static extern uint WaitForMultipleObjects(
            uint nCount, IntPtr[] lpHandles, bool bWaitAll, uint dwMilliseconds);

        [StructLayout(LayoutKind.Sequential)]
        public struct ThreadData
        {
            public ISearchStructure<string> SearchInput;
            public List<ISearchStructure<string>> InputList;
            public List<string> ResultList;

            public ThreadData(List<ISearchStructure<string>> inputList, ISearchStructure<string> searchInput, List<string> resultList)
            {
                InputList = inputList;
                SearchInput = searchInput;
                ResultList = resultList;
            }
        }

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
                #region oldimplementation
                //Thread.SetData(Thread.GetNamedDataSlot("ThreadData"), threadDataArray[i]);
                //threadHandles[i] = CreateThread(IntPtr.Zero, 0, Marshal.GetFunctionPointerForDelegate(new ThreadStart(StringSearch)), Marshal.UnsafeAddrOfPinnedArrayElement(threadDataArray, i), 0, out _);
                //threadHandles[i] = CreateThread(IntPtr.Zero, 0, Marshal.GetFunctionPointerForDelegate(new ThreadStart(StringSearch)), IntPtr.Zero, 0, out _);
                #endregion
                threadHandles[i] = CreateThread(IntPtr.Zero, 0, Marshal.GetFunctionPointerForDelegate(new ThreadStart(() => StringSearch(threadData))), IntPtr.Zero, 0, out _);
            }

            // Wait for all threads to complete
            WaitForMultipleObjects((uint)numThreads, threadHandles, true, uint.MaxValue);

            return resultList;
        }

       
    }
}
