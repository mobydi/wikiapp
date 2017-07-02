using System;
using System.Linq;
using WikiApp.Models;
using Xunit;

namespace WikiAppTest
{
    public class TokenizerTests
    {
        [Fact]
        public void Should_Tokinizer_Work()
        {
            Tokenizer tokenizer = new Tokenizer();
            var tokens = tokenizer.Tokenize("File:Can't Wait: it, should, work!.jpg");
            var result = string.Join(" ", tokens);
            
            Assert.Equal("Can't Wait it, should, work!", result);
        }
        
        [Fact]
        public void Should_Tokinizer_Work_2()
        {
            Tokenizer tokenizer = new Tokenizer();
            var tokens = tokenizer.Tokenize("File:Can't Wait: it, should, work.jpg");
            var result = string.Join(" ", tokens);
            
            Assert.Equal("Can't Wait it, should, work", result);
        }
        
        [Fact]
        public void Should_Tokinizer_Work_With_Empty_String()
        {
            Tokenizer tokenizer = new Tokenizer();
            var tokens = tokenizer.Tokenize(string.Empty).ToList();
            var result = string.Join(" ", tokens);
            
            Assert.Empty(tokens);
            Assert.Equal(String.Empty, result);
        }
    }
}
