using Compiler_dp2.Nodes;
using Compiler_dp2.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler_dp2.Compiler.Compiletypes
{
    class CompileAssignment : Compiler
    {
        public override Token compile(Token currentToken, Token lastToken, NodeLinkedList nodeLinkedList, Node before)
        {
            string variableName = currentToken.value; //left hand value
            currentToken = currentToken.nextToken.nextToken;

            Compiler rightCompiled = CompilerFactory.getInstance().getCompiler(currentToken); //right hand value
            currentToken = rightCompiled.compile(currentToken, lastToken, nodeLinkedList, before);

            nodeLinkedList.addLast(new NodeDirectFunction("ReturnToVariable", variableName));

            return currentToken.nextToken;
        }

        public override bool isMatch(Token currentToken)
        {
            var first = currentToken;
            var second = currentToken.nextToken;
            var third = second.nextToken;

            return second.tokenType == TokenType.Plus &&
                   (first.tokenType == TokenType.Number || first.tokenType == TokenType.Identifier) &&
                   (third.tokenType == TokenType.Number || third.tokenType == TokenType.Identifier) ||

                   second.tokenType == TokenType.Equals &&
                   (first.tokenType == TokenType.Number || first.tokenType == TokenType.Identifier) &&
                   (third.tokenType == TokenType.Number || third.tokenType == TokenType.Identifier);
        }
    }
}
