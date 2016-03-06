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
            // No clue..

            return null;
        }

        public override bool isMatch(Token currentToken)
        {
            // TODO
            return true;
        }
    }
}
