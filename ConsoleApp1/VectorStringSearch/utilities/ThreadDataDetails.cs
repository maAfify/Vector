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

        public static List<string> MulticoreSearchPattern(Action ThreadProc, List<ISearchStructure<string>> InputList, ISearchStructure<string> SearchInput)
        {
            int numThreads = Environment.ProcessorCount;
            List<string> resultList = new List<string>();

            // Calculate items per thread
            int itemsPerThread = InputList.Count / numThreads;
            int remainingItems = InputList.Count % numThreads;

            // Create thread handles
            IntPtr[] threadHandles = new IntPtr[numThreads];
            ThreadData[] threadDataArray = new ThreadData[numThreads];

            // Start multiple threads for parallel searching
            for (int i = 0; i < numThreads; i++)
            {
                int startIndex = i * itemsPerThread + Math.Min(i, remainingItems);
                int endIndex = startIndex + itemsPerThread + (i < remainingItems ? 1 : 0);

                threadDataArray[i] = new ThreadData(InputList.GetRange(startIndex, endIndex - startIndex), SearchInput, resultList);
                threadHandles[i] = CreateThread(IntPtr.Zero, 0, Marshal.GetFunctionPointerForDelegate(new ThreadStart(ThreadProc)), Marshal.UnsafeAddrOfPinnedArrayElement(threadDataArray, i), 0, out _);
            }

            // Wait for all threads to complete
            WaitForMultipleObjects((uint)numThreads, threadHandles, true, uint.MaxValue);

            return resultList;
        }
    }
}
