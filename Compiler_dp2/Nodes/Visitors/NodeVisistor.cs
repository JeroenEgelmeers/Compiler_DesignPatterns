using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler_dp2.Nodes.Visitors
{
    class NodeVisistor
    {
        public abstract class NodeVisitor
        {
            public abstract void visit(NodeConditionalJump node);
            public abstract void visit(NodeDirectFunction node);

            internal void visit(NodeStatement nodeStatement)
            {
                throw new NotImplementedException();
            }

            public abstract void visit(NodeJump node);
            public abstract void visit(NodeDoNothing node);
            public abstract void visit(NodeFunction node);
        }

    }
}
