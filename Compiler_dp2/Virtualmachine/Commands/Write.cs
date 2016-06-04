using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler_dp2.Virtualmachine;

namespace Compiler_dp2.Compiler.Commands
{
    class Write : BaseCommand
    {
        public override void execute(VirtualMachine vm, IList<string> parameters)
        {
            string joinedString = string.Join(",", parameters);
            Console.WriteLine(joinedString);
        }
    }
}
