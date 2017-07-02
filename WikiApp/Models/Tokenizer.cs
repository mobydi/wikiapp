using System;
using System.Collections.Generic;
using System.Linq;

namespace WikiApp.Models
{
    public interface ITokenizer
    {
        IEnumerable<string> Tokenize(string input);
    }

    public class Tokenizer : ITokenizer
    {
        private static readonly char[] _separators = new[]{' ',':','.'};
        public IEnumerable<string> Tokenize(string input)
        {
            var words = input.Split(_separators, StringSplitOptions.RemoveEmptyEntries);
            return words.Skip(1).Take(words.Length - 2).Select(x => x.Trim());
        }
    }
}