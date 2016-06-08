using Compiler_dp2.Nodes;
using Compiler_dp2.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler_dp2.Compiler
{
    class CompileIfStatement : Compiler
    {
        private CompileCondition        compileCondition; // ook weer een compiler
        private Compiler                compileStatement; // ook weer een compiler
        private NodeConditionalJump     conditionalJump;
        private NodeDoNothing           firstNodeDoNothing, trueNodeDoNothing, falseNodeDoNothing;
        private bool                    openedBracket; // check if compiling statement

        public CompileIfStatement() : base()
        {
            firstNodeDoNothing = new NodeDoNothing();
            conditionalJump = new NodeConditionalJump();
            trueNodeDoNothing = new NodeDoNothing();
            falseNodeDoNothing = new NodeDoNothing();

            compiledNodes.addLast(new NodeDoNothing());
            compiledNodes.addLast(new NodeConditionalJump());
            compiledNodes.addLast(new NodeDoNothing());
            compiledNodes.addLast(new NodeDoNothing());

            compiledNodes.addLast(firstNodeDoNothing);
            compiledNodes.addLast(conditionalJump);
            compiledNodes.addLast(trueNodeDoNothing);
            compiledNodes.addLast(falseNodeDoNothing);
        }

        public override Token compile(Token currentToken, Token lastToken, NodeLinkedList nodeLinkedList, Node before)
        {
            int level = currentToken.level;

            nodeLinkedList.insertBefore(before, firstNodeDoNothing);

            List<TokenExpected> expected = new List<TokenExpected>();
            expected.Add(new TokenExpected(level, TokenType.IfStatement));
            expected.Add(new TokenExpected(level, TokenType.EllipsisOpen));
                expected.Add(new TokenExpected(level + 1, TokenType.ANY));
            expected.Add(new TokenExpected(level, TokenType.EllipsisClose));
            expected.Add(new TokenExpected(level, TokenType.BracketsOpen));
                expected.Add(new TokenExpected(level + 1, TokenType.ANY));
            expected.Add(new TokenExpected(level, TokenType.BracketsClose));

            foreach (TokenExpected expt in expected)
            {
                // If no currentToken anymore, return null to get out of the compiler loop
                if (currentToken == null) { return null; }

                if (expt.level == level)
                {
                    if (currentToken.tokenType != expt.tokenType)
                    {
                        if (expt.tokenType != currentToken.tokenType)
                        {
                            if (expt.tokenType == TokenType.BracketsOpen ||
                            (expt.tokenType == TokenType.BracketsClose && openedBracket))
                            {
                                openedBracket = false;
                            }
                            else
                            {
                                throw new Exception_UnexpectedEnd("#CP0001 :: Unexpected end of statement.");
                            }
                        }
                    }
                    else
                    {
                        currentToken = currentToken.nextToken;
                    }
                }
                else if (expt.level >= level)
                {
                    if (compileCondition == null)
                    {
                        compileCondition = new CompileCondition();
                        currentToken = compileCondition.compile(currentToken, lastToken, nodeLinkedList, conditionalJump);
                    }
                    else
                    {
                        while (currentToken.tokenType != TokenType.BracketsClose && currentToken.tokenType != TokenType.EllipsisClose)
                        {
                            compileStatement = CompilerFactory.getInstance().getCompiler(currentToken);
                            currentToken = compileStatement.compile(currentToken, lastToken, nodeLinkedList, falseNodeDoNothing);
                        }
                    }
                }
            }

            return currentToken;
        }

        public override bool isMatch(Token currentToken)
        {
            return currentToken.tokenType == TokenType.IfStatement && currentToken.partner == null;
        }
    }
}