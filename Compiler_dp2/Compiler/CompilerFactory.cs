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

        public string getClassName(Token t)
        {
            string getClass = null;

            switch (t.tokenType)
            {
                case TokenType.While:
                    getClass = "CompileWhile";
                break;
                case TokenType.IfStatement:
                    if (t.partner != null && t.partner.tokenType == TokenType.ElseStatement) { getClass = "CompileIfElseStatement"; } // dit ok?
                    else { getClass = "CompileIfStatement"; }
                break;
                case TokenType.Identifier:
                    // TODO?
                break;
                default:
                    // ????
                break;
            }

            return getClass;
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

        public Compiler getCompiler (Token t)
        {
            Compiler compilerStrategy = null;

            try
            {
                foreach(Compiler c in compilerClasses)
                {
                    if (c.GetType().Name == getClassName(t))
                    {
                        compilerStrategy = c;
                    }
                }

            }catch
            {
                throw new Exception_FactoryFailed("#CF0001 :: Can't get right compiler.");
            }

            return compilerStrategy;
        }

    }
}
