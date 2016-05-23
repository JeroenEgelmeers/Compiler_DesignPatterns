using Compiler_dp2.Compiler.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler_dp2.Virtualmachine;
using Compiler_dp2.Tokenizer;
using System.Text.RegularExpressions;

namespace Compiler_dp2.Commands
{
    class Plus : BaseCommand
    {
        public override void execute(VirtualMachine vm, IList<string> parameters)
        {
            var var1 = vm.vars[parameters[1]];
            var var2 = vm.vars[parameters[2]];

            int intVar1;
            int intVar2;
            if (Int32.TryParse(var1, out intVar1) && Int32.TryParse(var2, out intVar2)) 
            {
                vm.returnValue = (Int32.Parse(var1) + Int32.Parse(var2)).ToString();
            }else {
                vm.returnValue = (var1 + var2);
            }
        }
    }
}
