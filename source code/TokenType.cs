using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum TokenType
{
    // Single-character tokens.
    LEFT_PAREN, RIGHT_PAREN,

    // One or two character tokens.
    NIL,

    // Literals.
    SYMBOL, NUM, // identifier is a symbol

    EOF
}
