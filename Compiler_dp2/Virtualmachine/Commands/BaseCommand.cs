using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler_dp2.Virtualmachine;
using System.Collections;

namespace Compiler_dp2.Compiler.Commands
{
    abstract class BaseCommand
    {
        public abstract void execute(VirtualMachine vm, IList<string> parameters);
    }
}
