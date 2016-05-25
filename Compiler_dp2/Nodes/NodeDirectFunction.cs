using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler_dp2.Nodes.Visitors;
using Compiler_dp2.Tokenizer;

namespace Compiler_dp2.Nodes
{
    class NodeDirectFunction : NodeAbstractFunction
    {      

        public NodeDirectFunction(string type, string value)
        {
            parameters = new string[2] { type, value };
        }

        public void setStringArray(int arraySize)
        {
            if (arraySize > 0) { parameters = new string[arraySize]; }
            else { parameters = null; }
        }

        public void setAt(int index, string text) { parameters[index] = text; }
        public string getAt(int index) { return parameters[index]; }

        public string[] getToCompile() { return parameters; }

        public override void accept(NodeVisistor.NodeVisitor visitor) { visitor.visit(this); }

        public override string nodeType()
        {
            string returnContent = "Direct Function Node: ";

            foreach(string c in parameters) { returnContent += c; }

            return returnContent;
        }



    }
}
