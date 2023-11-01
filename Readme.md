-A data structure is given in the form of a list (vector) with different words. A
The search algorithm should find a list of words from this word list whose first characters start with
match a search string.

-The algorithm is intended to exploit modern multi-core processors, so the search is based on as many as possible
Distribute cores.

- Each time you search, you can expect a new word list.


- Design a suitable data structure for the word list.
- Develop the search algorithm and implement it in C# on a Windows platform.
- It can be assumed that the word list follows the search algorithm during the search
  is available exclusively.
- Win32 API methods should be used for thread creation and thread synchronization
  become.
- The trivial approach with built-in C# functionality for parallelization is not a valid solution
  represents.
- Implement a test client that determines the runtime of a search and the search result
  validated.
- For demonstration purposes, a word list should be used consisting of all possible
  Combinations of 4 capital letters exist (AAAA to ZZZZ). The order of the words in the List can be considered random.