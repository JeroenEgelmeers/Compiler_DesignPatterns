using Compiler_dp2.Nodes;
using Compiler_dp2.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler_dp2.Compiler
{
    class CompileStatement : Compiler
    {
        
        public override  Token compile(Token currentToken, Token lastToken, NodeLinkedList nodeLinkedList, Node before)
        {

            int level = currentToken.level;
            

            List<TokenExpected> excepted = new List<TokenExpected>();
            excepted.Add(new TokenExpected(level, TokenType.Identifier));
            excepted.Add(new TokenExpected(level, TokenType.ANY)); // What should this be?
            excepted.Add(new TokenExpected(level, TokenType.ANY));

            foreach (TokenExpected expt in excepted)
            {

            }

            return currentToken;
        }

        public override bool isMatch(Token currentToken)
        {
            // TODO
            return true;
        }
    }
}
