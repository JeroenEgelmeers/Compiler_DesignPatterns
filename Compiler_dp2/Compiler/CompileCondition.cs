using Compiler_dp2.Nodes;
using Compiler_dp2.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
///  UPDATED!!!!
/// </summary>


namespace Compiler_dp2.Compiler
{
    class CompileCondition : Compiler
    {
        Dictionary<TokenType, string> supportedTokens; // add suported tokens like == to get right condition from conditions folder

        public CompileCondition()
        {
            // Add conditions that you want to support
            supportedTokens = new Dictionary<TokenType, string>();
            supportedTokens.Add(TokenType.EqualsEquals, "Equals");
            //? supportedTokens.Add(TokenType.Function, "Write");

            compiledNodes.addLast(new NodeDoNothing());
        }

        public override Token compile(Token currentToken, Token lastToken, NodeLinkedList nodeLinkedList, Node before)
        {
            string supportedTokenString;
            int level = currentToken.level;
            

            return currentToken;
        }
    }
}
