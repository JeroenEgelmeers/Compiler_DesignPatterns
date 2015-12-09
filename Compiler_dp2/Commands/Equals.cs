using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler_dp2.Virtualmachine;

namespace Compiler_dp2.Compiler.Commands
{
    class Equals : BaseCommand
    {
        public override void execute(VirtualMachine vm, IList<string> parameters)
        {
            string var1 = vm.vars[parameters[1]];
            string var2 = vm.vars[parameters[2]];

            if (var1.Equals(var2)) { vm.returnValue = true.ToString(); }
            else { vm.returnValue = false.ToString();}
        }
    }
}
