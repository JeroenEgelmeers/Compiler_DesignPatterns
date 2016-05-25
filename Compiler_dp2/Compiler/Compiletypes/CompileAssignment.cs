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
            if (currentToken.tokenType == TokenType.Identifier && currentToken.nextToken.tokenType == TokenType.Equals)
            {
                return true;
            }
            return false;
        }
    }
}
