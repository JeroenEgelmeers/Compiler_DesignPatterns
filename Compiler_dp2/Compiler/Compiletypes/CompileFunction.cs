using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler_dp2.Nodes;
using Compiler_dp2.Tokenizer;

namespace Compiler_dp2.Compiler
{
    class CompileFunction : Compiler
    {

        public override Token compile(Token currentToken, Token endToken, NodeLinkedList nodeList, Node before)
        {
            Node current = before;
            Token first = currentToken;
            Token second = currentToken.nextToken.nextToken;
            string value = second.value;
            string varName;

            current = current.insertPrevious(new NodeDirectFunction("ConstantToReturn", value));
            varName = Guid.NewGuid().ToString("N");
            current = current.insertPrevious(new NodeDirectFunction("ReturnToVariable", varName));

            current = current.insertPrevious(new NodeFunction("write", varName));

            return second.nextToken.nextToken.nextToken;
        }

        public override bool isMatch(Token currentToken)
        {
            if (currentToken.tokenType == TokenType.Function)
            {
                return true;
            }
            return false;
        }
    }
}
