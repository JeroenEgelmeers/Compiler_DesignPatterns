using Compiler_dp2.Nodes;
using Compiler_dp2.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler_dp2.Compiler.Compiletypes
{
    class CompileConstant : Compiler
    {
        public override Token compile(Token currentToken, Token lastToken, NodeLinkedList nodeLinkedList, Node before)
        {
            if (before == null)
            {
                nodeLinkedList.addLast(new NodeDirectFunction("ConstantToReturn", currentToken.value));
                nodeLinkedList.addLast(new NodeDirectFunction("ReturnToVariable", Guid.NewGuid().ToString("N")));
            }
            else
            {
                before = before.insertPrevious(new NodeDirectFunction("ConstantToReturn", currentToken.value));
                before.insertPrevious(new NodeDirectFunction("ReturnToVariable", Guid.NewGuid().ToString("N")));
            }

            return currentToken.nextToken;
        }

        public override bool isMatch(Token currentToken)
        {
            //TODO uitbreiden
            return currentToken.tokenType == TokenType.Number && currentToken.nextToken.tokenType == TokenType.Semicolon;
        }
    }
}
