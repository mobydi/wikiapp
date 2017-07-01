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
        public IEnumerable<string> Tokenize(string input)
        {
            var words = input.Split(new[]{' ',':','.'}, StringSplitOptions.RemoveEmptyEntries);
            return words.Skip(1).Take(words.Length - 2).Select(x => x.Trim());
        }
    }
}