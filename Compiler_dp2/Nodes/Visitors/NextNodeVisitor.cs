using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler_dp2.Nodes.Visitors
{
    class NextNodeVisitor : NodeVisistor.NodeVisitor
    {
        public Node nextNode { get; private set; }

        public override void visit(NodeDoNothing node) { nextNode = node.getNext(); }

        public override void visit(NodeJump node) { nextNode = node.jumpNode;  }

        public override void visit(NodeConditionalJump node) { throw new NotImplementedException(); } // Zorg ervoor dat hij bij de ReturnValue van de VirtualMachine kan.

        public override void visit(NodeFunction node) { throw new NotImplementedException(); }

        public override void visit(NodeDirectFunction node) { throw new NotImplementedException(); }

    }
}
