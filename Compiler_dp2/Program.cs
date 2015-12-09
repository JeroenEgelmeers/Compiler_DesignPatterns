using Compiler_dp2.Nodes;
using Compiler_dp2.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler_dp2
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Starting tokenizer...");
            Tokenizer.Tokenizer tokenizer = new Tokenizer.Tokenizer("E:/Documenten/Avans/semester - open/Semester 7/Blok 1/Design Patterns/week 2/testScript.txt");
            Token firstToken = tokenizer.getFirstToken();
            Console.WriteLine("Finished tokenizer...\n");

            Console.WriteLine("Starting compiler...");
            Compiler.Compiler compiler = new Compiler.Compiler();
            NodeLinkedList nodeLinkedList = new NodeLinkedList();
            compiler.compile(firstToken, null, nodeLinkedList, null);

            // Get first node after compiled
            Node node = nodeLinkedList.getFirst();

            while (node != null)
            {
                Console.WriteLine(node.nodeKind());
                node = node.getNext();
            }
            
            Console.WriteLine("Finished compiler...\n");

            Console.WriteLine("Starting virtual macine...");
            // # Virtual machine
            Console.WriteLine("Finished virtual macine...\n");
            // Keep it alive..
            Console.ReadLine();
        }


    }
}
