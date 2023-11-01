using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using VectorStudyCase.src.Interfaces;

namespace VectorStudyCase.src.Features
{
    public class StringSearchedFeature : IStartWithFeature<string>
    {
        public string? Data { get;set; }

        public StringSearchedFeature(string? data)
        {
            Data = data;   
        }

        public bool? StartsWithImpl(string instance)
        {
            return Data?.StartsWith(instance, StringComparison.InvariantCulture);
        }
    }

}
