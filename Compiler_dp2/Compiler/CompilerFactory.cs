using Compiler_dp2.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Compiler_dp2.Compiler
{
    class CompilerFactory
    {
        private List<Compiler> compilerClasses;
        private static CompilerFactory instance;

        // Singleton
        public static CompilerFactory getInstance()
        {
            if (instance == null) { instance = new CompilerFactory(); }
            return instance;
        }

        private CompilerFactory()
        {
            compilerClasses = new List<Compiler>();
            foreach (Type type in
                Assembly.GetAssembly(typeof(Compiler)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Compiler))))
            {
                compilerClasses.Add((Compiler)Activator.CreateInstance(type));
            }
        }

        public Compiler getCompiler(Token t)
        {
            Compiler compilerStrategy = null;

            try
            {
                foreach (Compiler c in compilerClasses)
                {
                    if (c.isMatch(t))
                    {
                        compilerStrategy = c;
                        break;
                    }
                }
                if (compilerStrategy == null)
                    throw new Exception();

            }
            catch
            {
               throw new Exception_FactoryFailed("#CF0001 :: Can't get right compiler." + t.tokenType);
            }

            return compilerStrategy;
        }

    }
}
