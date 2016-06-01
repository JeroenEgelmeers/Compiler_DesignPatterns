using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler_dp2.Nodes
{
    abstract class Node
    {
        private Node next;
        private Node previous;

        public Node getNext() { return next; }
        public Node setNext(Node node)
        {
            var prevNext = next;
            next = node;
            node.next = prevNext;
            if (node.next != null)
            {
                node.next.previous = node;
            }
            node.previous = this;
            return this; 
        }

        public Node insertPrevious(Node node)
        {
            var prevPrevious = previous;
            previous = node;
            node.next = this;
            node.previous = prevPrevious;
            if (prevPrevious != null)
            {
                prevPrevious.next = node;
            }
            return this;
        }

        public bool hasNext() { return (next != null); }

        public abstract string nodeType(); // return a string for program results

        public abstract void accept(Visitors.NodeVisistor.NodeVisitor visitor);

        public void writeLineNode()
        {
            Console.WriteLine("Node: " + nodeType());
        }

        public override string ToString()
        {
            return nodeType();
        }
    }
}
