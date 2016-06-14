using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler_dp2.Tokenizer
{
    public enum TokenType
    {
        BracketsClose,  // }
        BracketsOpen,   // {
        ElseStatement,  // ELSE
        EllipsisClose,  // )
        EllipsisOpen,   // (
        Equals,         // =
        EqualsEquals,   // ==
        NotEquals,      // !=
        Function,       // function (write)
        GreaterEquals,  // >=
        LessEquals,     // <=
        Identifier,     // x
        IfStatement,    // IF
        Plus,           // +
        Minus,          // -
        Number,         // 0
        Semicolon,      // ;
        While,           // WHILE
        String,         // String
        ANY,            // Can be anything...
    }

}
