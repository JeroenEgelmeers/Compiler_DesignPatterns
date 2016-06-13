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
            Tokenizer.Tokenizer tokenizer = new Tokenizer.Tokenizer("C:\\Users\\jeroe\\Documents\\Visual Studio 2015\\Projects\\Compiler_dp2_git\\Compiler_dp2\\Code\\testScript1.txt");            
            Console.WriteLine("Finished tokenizer...\n");

            Console.WriteLine("Starting compiler...");
            Compiler.Compiler compiler = new Compiler.Compiler();
            NodeLinkedList nodeLinkedList = compiler.compile(tokenizer.tokens);                    
            Console.WriteLine("Finished compiler...\n");
            
            Console.WriteLine("Starting virtual machine...");
            Virtualmachine.VirtualMachine.getInstance().Run(nodeLinkedList);          
            Console.WriteLine("Finished virtual machine...\n");

            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }
    }
}
