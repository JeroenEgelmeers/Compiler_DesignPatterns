using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler_dp2.Nodes.Visitors;

namespace Compiler_dp2.Nodes
{
    class NodeAbstractFunction : Node
    {
        public override void accept(NodeVisistor.NodeVisitor visitor) { }

        public override string nodeKind() { return "Abstract Function"; }
    }
}
