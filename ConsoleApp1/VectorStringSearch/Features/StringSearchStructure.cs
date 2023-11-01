using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using VectorStudyCase.src.Interfaces;

namespace VectorStudyCase.src.Features
{
    public class StringSearchStructure : ISearchStructure<string>
    {
        public string? Data { get;set; }

        public StringSearchStructure(string? data)
        {
            Data = data;   
        }
    }

}
