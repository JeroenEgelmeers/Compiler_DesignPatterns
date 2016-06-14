using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler_dp2.Virtualmachine;

namespace Compiler_dp2.Nodes.Visitors
{
    class NextNodeVisitor : NodeVisistor.NodeVisitor
    {
        public Node nextNode { get; private set; }

        public override void visit(NodeDoNothing node) { nextNode = node.getNext(); }

        public override void visit(NodeJump node) { nextNode = node.jumpNode;  }

        public override void visit(NodeConditionalJump node)
        {
            if (VirtualMachine.getInstance().returnValue == true.ToString()) { this.nextNode = node.nextOntrue; }
            else { this.nextNode = node.nextOnFalse; }
        }

        public override void visit(NodeFunction node) { nextNode = node.getNext(); } // Not sure if this is right..

        public override void visit(NodeDirectFunction node) { nextNode = node.getNext(); } // Not sure if this is right..
    }
}
