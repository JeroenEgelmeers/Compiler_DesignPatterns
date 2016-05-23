using Compiler_dp2.Nodes;
using Compiler_dp2.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler_dp2.Compiler
{
    class CompileIfElseStatement : Compiler
    {
        private NodeJump nodeJump;
        private NodeDoNothing nodeDoNothing;

        private CompileCondition compileCondition; // ook weer een compiler
        private Compiler compileStatement; // ook weer een compiler
        private NodeConditionalJump conditionalJump;
        private NodeDoNothing firstNodeDoNothing, trueNodeDoNothing, falseNodeDoNothing;
        private bool openedBracket; // check if compiling statement

        public CompileIfElseStatement() : base()
        {
            firstNodeDoNothing = new NodeDoNothing();
            trueNodeDoNothing = new NodeDoNothing();
            falseNodeDoNothing = new NodeDoNothing();
            conditionalJump = new NodeConditionalJump();
            nodeJump = new NodeJump();
            nodeDoNothing = new NodeDoNothing();

            conditionalJump.nextOntrue = trueNodeDoNothing;
            conditionalJump.nextOnFalse = falseNodeDoNothing;

            compiledNodes.addLast(firstNodeDoNothing);
            compiledNodes.addLast(conditionalJump);
            compiledNodes.addLast(trueNodeDoNothing);
            compiledNodes.addLast(falseNodeDoNothing);

            nodeJump.jumpNode = nodeDoNothing;

            compiledNodes.insertBefore(falseNodeDoNothing, nodeJump);
            compiledNodes.addLast(nodeDoNothing);

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
            expected.Add(new TokenExpected(level, TokenType.ElseStatement));
            expected.Add(new TokenExpected(level, TokenType.BracketsOpen));
            expected.Add(new TokenExpected(level + 1, TokenType.ANY));
            expected.Add(new TokenExpected(level, TokenType.BracketsClose));

            foreach (TokenExpected expt in expected)
            {
                // If no currentToken anymore, return null to get out of the compiler loop
                if (currentToken == null)
                {
                    return null;
                } // needs to check for null else exception!
                if (expt.level == level)
                {
                    if (expt.tokenType == TokenType.ElseStatement)
                    {
                        openedBracket = true;
                    }

                    if (currentToken.tokenType != expt.tokenType)
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
                    else if (compileStatement == null)
                    {
                        compileStatement = CompilerFactory.getInstance().getCompiler(currentToken);
                        currentToken = compileStatement.compile(currentToken, lastToken, nodeLinkedList, nodeJump);
                    }
                    else
                    {
                        Compiler elseCompiler = CompilerFactory.getInstance().getCompiler(currentToken);
                        currentToken = compileStatement.compile(currentToken, lastToken, nodeLinkedList, nodeDoNothing);
                    }
                }
            }

            return currentToken;
        }        

        public override bool isMatch(Token currentToken)
        {
            return currentToken.tokenType == TokenType.IfStatement && currentToken.partner.tokenType == TokenType.ElseStatement;
        }
    }
}
