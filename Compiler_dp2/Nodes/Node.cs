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

        public Node getNext() { return next; }
        public Node setNext(Node node) { next = node;
            return this; 
        }
        public bool hasNext() { return (next != null); }

        public abstract string nodeKind(); // return a string for program results

        public abstract void accept(Visitors.NodeVisistor.NodeVisitor visitor);
    }
}
