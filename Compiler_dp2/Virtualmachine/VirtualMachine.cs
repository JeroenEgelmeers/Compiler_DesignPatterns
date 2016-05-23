using Compiler_dp2.Nodes;
using Compiler_dp2.Nodes.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler_dp2.Virtualmachine
{
    class VirtualMachine
    {
        private static VirtualMachine instance;
        public Dictionary<string, string> vars { get; set; }
        public string returnValue { get; set; } // Uit powerpoint

        private VirtualMachine() { vars = new Dictionary<string, string>(); }

        public static VirtualMachine getInstance()
        {
            if (instance == null) { instance = new VirtualMachine(); }
            return instance;
        }
        
        // From powerpoint:
        public void Run(NodeLinkedList list)
        {
            Node currentNode = list.getFirst();
            NextNodeVisitor visitor = new NextNodeVisitor();

            while(currentNode != null)
            {
                // Do something with the current node
                NodeAbstractFunction aNode = currentNode as NodeAbstractFunction;
                if (aNode != null)
                {
                   // TODO: Powerpoint niet duidelijk HIER!

                }

                // Get next Node
                currentNode.accept(visitor);
                currentNode = visitor.nextNode;
            }

        }
 
    }
}
