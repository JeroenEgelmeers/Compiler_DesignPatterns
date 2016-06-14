using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler_dp2.Nodes.Visitors;

namespace Compiler_dp2.Nodes
{
    class NodeConditionalJump : Node
    {

        public Node nextOntrue { get; set; }
        public Node nextOnFalse { get; set; }

        public override void accept(NodeVisistor.NodeVisitor visitor) { visitor.visit(this); }

        public override string nodeType()
        {
            return "Conditional Jump Node";
        }
    }
}
