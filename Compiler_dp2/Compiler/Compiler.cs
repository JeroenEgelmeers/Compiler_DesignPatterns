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

        public Compiler()
        {
            compiledNodes = new NodeLinkedList();
        }

        //Initial compile method
        public NodeLinkedList compile(List<Token> tokens)
        {
            Token nextToken = tokens.First();
            while (nextToken != null)
            {
                Compiler compilerType = CompilerFactory.getInstance().getCompiler(nextToken);
                nextToken = compilerType.compile(nextToken, null, compiledNodes, null);
                Console.WriteLine(compiledNodes);
            }            
            return compiledNodes;            
        }

        private void print()
        {
            Node currentNode = compiledNodes.getFirst();
            while(currentNode != null)
            {
                currentNode.writeLineNode();
                currentNode = currentNode.getNext();
            }
        }

        //sub compile method OLD
        public virtual Token compile(Token currentToken, Token endToken, NodeLinkedList nodeList, Node before)
        {
            while (currentToken != null)
            {
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
