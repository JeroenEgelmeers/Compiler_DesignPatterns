using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler_dp2.Tokenizer
{
    class TokenExpected
    {
        public int level;
        public TokenType tokenType;

        public TokenExpected(int setLevel, TokenType setTokenType)
        {
            level = setLevel;
            tokenType = setTokenType;
        }
    }
}
