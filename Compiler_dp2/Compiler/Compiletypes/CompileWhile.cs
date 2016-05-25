using Compiler_dp2.Nodes;
using Compiler_dp2.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler_dp2.Compiler
{
    class CompileWhile : Compiler
    {
        private CompileCondition        compileCondition; // ook weer een compiler
        private Compiler                compileStatement; // ook weer een compiler
        private NodeDoNothing           firstDoNothingNode;
        private NodeConditionalJump     conditionalJump;
        private NodeJump                jumpBackNode;
        private bool                    openedBracket; // check if compiling statement

        public CompileWhile() : base()
        {
            firstDoNothingNode          = new NodeDoNothing();
            conditionalJump             = new NodeConditionalJump();
            jumpBackNode                = new NodeJump();

            compiledNodes.addLast(firstDoNothingNode);
            compiledNodes.addLast(conditionalJump);
            compiledNodes.addLast(new NodeDoNothing());
            compiledNodes.addLast(jumpBackNode);
            compiledNodes.addLast(new NodeDoNothing());

            jumpBackNode.jumpNode           = compiledNodes.getFirst();
            conditionalJump.nextOntrue      = compiledNodes.get(2);
            conditionalJump.nextOnFalse     = compiledNodes.getLast();

            // Settings
            openedBracket                   = false;
        }

        public override Token compile(Token currentToken, Token lastToken, NodeLinkedList nodeLinkedList, Node before)
        {
            int level = currentToken.level;

            nodeLinkedList.insertBefore(before, firstDoNothingNode);

            List<TokenExpected> expected = new List<TokenExpected>();
            expected.Add(new TokenExpected(level, TokenType.While));
            expected.Add(new TokenExpected(level, TokenType.EllipsisOpen));
                expected.Add(new TokenExpected(level + 1, TokenType.ANY));
            expected.Add(new TokenExpected(level, TokenType.EllipsisClose));
            expected.Add(new TokenExpected(level, TokenType.BracketsOpen));
                expected.Add(new TokenExpected(level + 1, TokenType.ANY));
            expected.Add(new TokenExpected(level, TokenType.BracketsClose));

            foreach(TokenExpected expt in expected)
            {
                // If no currentToken anymore, return null to get out of the compiler loop
                if (currentToken == null) { return null; }
                
                if (expt.level == level)
                {
                    if (currentToken.tokenType != expt.tokenType)
                    {
                        if (expt.tokenType != currentToken.tokenType) //TODO vraag waar is dit voor ? hoort de token niet altijd expected te zijn ?
                        {
                            if (expt.tokenType == TokenType.BracketsOpen || (expt.tokenType == TokenType.BracketsClose && openedBracket))
                            {
                                openedBracket = true;
                            }else { throw new Exception_UnexpectedEnd("#CP0001 :: Unexpected end of statement."); }
                        }
                    }
                    else
                    {
                        currentToken = currentToken.nextToken;
                    }
                }else if (expt.level >= level)
                {
                    if (compileCondition == null)
                    {
                        compileCondition    = new CompileCondition();
                        currentToken        = compileCondition.compile(currentToken, lastToken, nodeLinkedList, conditionalJump);
                    }else
                    {
                        compileStatement    = CompilerFactory.getInstance().getCompiler(currentToken);
                        currentToken        = compileStatement.compile(currentToken, lastToken, nodeLinkedList, jumpBackNode);
                    }
                }
            }
            return currentToken;
        }

        public override bool isMatch(Token currentToken)
        {
            return currentToken.tokenType == TokenType.While;
        }
    }
}
