

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler_dp2.Nodes.Visitors;

namespace Compiler_dp2.Nodes
{
    class NodeFunction : NodeAbstractFunction
    {
        public NodeFunction(string type, string left, string right)
        {
            parameters = new string[3] { type, left, right };
        }

        public void setStringArray(int arraySize)
        {
            if (arraySize > 0) { parameters = new string[arraySize]; }
            else { parameters = null; }
        }

        public void         setAt(int index, string text)   { parameters[index] = text;  }
        public string       getAt(int index)                { return parameters[index];  }
        public string[]     getToCompile()                  { return parameters;         }

        public override void accept(NodeVisistor.NodeVisitor visitor) { visitor.visit(this); }
        public override string nodeType()
        {
            string returnContent = "Function: ";

            returnContent += String.Join(", ", parameters);

            return returnContent;
        }

    }
}
