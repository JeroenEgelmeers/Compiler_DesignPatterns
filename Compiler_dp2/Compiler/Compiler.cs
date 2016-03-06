using Compiler_dp2.Nodes;
using Compiler_dp2.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler_dp2.Compiler
{
    class Compiler
    {
        protected NodeLinkedList compiledNodes;

        public Compiler() { compiledNodes = new NodeLinkedList(); }

        public virtual Token compile(Token currentToken, Token endToken, NodeLinkedList nodeList, Node before)
        {
            while (currentToken != null) {
               // Console.WriteLine("{0}, {1}: {2}", currentToken.ruleNumber, currentToken.positionInRule, currentToken.value); // fix door Martijn
                currentToken = CompilerFactory.getInstance().getCompiler(currentToken).compile(currentToken, endToken, nodeList, before);
            }
            return null;
        }

        public virtual bool isMatch(Token currentToken)
        {
            return false;
        }
    }
}
