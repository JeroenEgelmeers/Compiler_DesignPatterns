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
        
        public override Token compile(Token currentToken, Token lastToken, NodeLinkedList nodeLinkedList, Node before)
        {
            int level = currentToken.level;            

            List<TokenExpected> excepted = new List<TokenExpected>();
            excepted.Add(new TokenExpected(level, TokenType.Identifier)); //left hand value
            excepted.Add(new TokenExpected(level, TokenType.ANY)); // Operators
            excepted.Add(new TokenExpected(level, TokenType.ANY)); // right hand value

            
            foreach (TokenExpected expt in excepted)
            {
                //TODO

            }

            return currentToken;
        }

        public override bool isMatch(Token currentToken)
        {
            //TODO tweede if condition aanpassen
            if (currentToken.tokenType == TokenType.Identifier && currentToken.tokenType != TokenType.Identifier)
            {
                return true;
            }
            return false;
        }
    }
}
