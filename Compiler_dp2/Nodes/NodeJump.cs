using Compiler_dp2.Nodes.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler_dp2.Nodes
{
    class NodeJump : Node
    {
        public Node jumpNode { get; set; }

        public override void accept(NodeVisistor.NodeVisitor visitor)
        {
            visitor.visit(this);
        }

        public override string nodeKind() { return "Jump Node"; }
    }
}
