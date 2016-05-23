using Compiler_dp2.Compiler.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler_dp2.Virtualmachine;

namespace Compiler_dp2.Commands
{
    class ReturnToVariable : BaseCommand
    {
        public override void execute(VirtualMachine vm, IList<string> parameters)
        {
            vm.vars[parameters[1]] = vm.returnValue; // From Powerpoint
        }
    }
}
