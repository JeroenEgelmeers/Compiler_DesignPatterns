using Compiler_dp2.Nodes;
using Compiler_dp2.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler_dp2.Compiler
{
    class CompileConstant : Compiler
    {
        // TODO: Should be compileConstant....

        public override Token compile(Token currentToken, Token lastToken, NodeLinkedList nodeLinkedList, Node before)
        {
            int level = currentToken.level;

            string value = "";
            string varName = "";
            Node current = before;
            List<NodeDirectFunction> function = new List<NodeDirectFunction>();

            List<TokenExpected> excepted = new List<TokenExpected>();
            excepted.Add(new TokenExpected(level, TokenType.Identifier)); //left hand value
            excepted.Add(new TokenExpected(level, TokenType.Equals)); // Operators
            excepted.Add(new TokenExpected(level, TokenType.ANY)); // right hand value
            excepted.Add(new TokenExpected(level, TokenType.Semicolon)); // Close statement

            foreach (TokenExpected expt in excepted)
            {
                if (expt.level == level)
                {
                    if (currentToken.tokenType != expt.tokenType && expt.tokenType != TokenType.ANY)
                    {
                        throw new Exception_UnexpectedEnd("#CP0001 :: Unexpected end of statement.");
                    }
                    else
                    {
                        if (currentToken.tokenType == TokenType.Identifier)
                        {
                            varName = currentToken.value.ToString();
                        }
                        if (expt.tokenType == TokenType.ANY)
                        {
                            if (currentToken.nextToken.tokenType == TokenType.Plus)
                            {
                                String f1 = Guid.NewGuid().ToString("N");
                                String f2 = Guid.NewGuid().ToString("N");
                                String f3 = Guid.NewGuid().ToString("N");
                                nodeLinkedList.addLast(new NodeDirectFunction("ConstantToReturn", currentToken.value));
                                nodeLinkedList.addLast(new NodeDirectFunction("ReturnToVariable", f1));
                                nodeLinkedList.addLast(new NodeDirectFunction("ConstantToReturn", currentToken.nextToken.nextToken.value));
                                nodeLinkedList.addLast(new NodeDirectFunction("ReturnToVariable", f2));
                                nodeLinkedList.addLast(new NodeFunction("Plus", f1, f2));

                                currentToken = currentToken.nextToken.nextToken;

                            }
                            value = currentToken.value;
                        }
                        currentToken = currentToken.nextToken;
                    }
                }
            }
            if (before == null)
            {
                nodeLinkedList.addLast(new NodeDirectFunction("ConstantToReturn", value));
                nodeLinkedList.addLast(new NodeDirectFunction("ReturnToVariable", Guid.NewGuid().ToString("N"))); // TODO: Klopt dit? Of geef "var" mee?
            }
            else
            {
                before = before.insertPrevious(new NodeDirectFunction("ConstantToReturn", value));
                before.insertPrevious(new NodeDirectFunction("ReturnToVariable", Guid.NewGuid().ToString("N"))); // TODO: klopt dit? Of geef "var" mee?
            }

            return currentToken;
        }

        public override bool isMatch(Token currentToken)
        {
            // Todo var toekenen
            if (currentToken.tokenType == TokenType.Identifier && currentToken.nextToken.tokenType == TokenType.Equals)
            {
                return true;
            }
            return false;
        }
    }
}
