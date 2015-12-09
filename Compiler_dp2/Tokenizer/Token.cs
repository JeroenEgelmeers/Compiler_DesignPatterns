using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler_dp2.Tokenizer
{
    public class Token
    {
        public int ruleNumber { get; set; } // Regel nummer
        public int positionInRule { get; set; } // Positie in regel (characther 3)
        public string value { get; set; } // Waarde
        public TokenType tokenType { get; set; } // Tokentype uit TokenType.cs
        public int level { get; set; } // ( level++, ) level--
        public Token partner { get; set; } // ( has partner )
        public Token nextToken { get; set; }

        public void writeLineToken()
        {
            Console.WriteLine("Token: " + "[Regel: " + ruleNumber + "] [Waarde: " + value + "] [Type: "+ tokenType +" ] [Level: " + level + "] [Partner: " + partner + "]");
        }
    }
}