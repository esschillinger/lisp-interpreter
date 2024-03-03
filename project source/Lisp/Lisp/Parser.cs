using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

public class Parser
{
    private class ParseError : Exception { }

    private List<Token> tokens;
    private int current = 0;

    /* LISP GRAMMAR:
     * 
     *  TERMINALS: { ( ) SYMBOL NUMBER }
        s => sexp
        sexp => listexpr | nil | atom
        nil => ( )
        atom => SYMBOL | NUMBER
        listexpr => ( SYMBOL exprs ) | ( NUMBER exprs ) | ( listexpr exprs )
        exprs => epsilon | exprs sexp
        function => SYMBOL
        epsilon =>
     *
     */

    public Parser(List<Token> tokens)
    {
        this.tokens = tokens;
    }

    public List<Sexpr> parse()
    {
        List<Sexpr> sexprs = new List<Sexpr>();
        while (!isAtEnd())
        {
            sexprs.Add(start());
        }

        return sexprs;
    }

    // s => sexp
    private Sexpr start() {
        return sexpr();
    }

    // sexp => listexpr | nil | atom
    private Sexpr sexpr() {
        if (match(TokenType.SYMBOL, TokenType.NUM))
        {
            return atom();
        }
        
        if (match(TokenType.NIL))
        {
            return nil();
        }

        // must be a listexpr at this point

        return listexpr();
    }

    // atom => SYMBOL | NUMBER
    private Sexpr atom() {
        Token t = advance();
        return new Sexpr.Atom(t);
    }

    // nil => ( )
    private Sexpr nil() {
        Token t = advance();
        return new Sexpr.Nil();
    }

    // listexpr => ( SYMBOL exprs ) | ( NUMBER exprs ) | ( listexpr exprs )
    private Sexpr listexpr() {
        consume(TokenType.LEFT_PAREN, "Expect '(' to begin listexpr.");

        Sexpr left;

        if (match(TokenType.SYMBOL, TokenType.NUM))
        {
            left = atom();
        }
        else if (match(TokenType.NIL))
        {
            throw error(peek(), "Cannot insert nil () into beginning of listexpr.");
        }
        else
        {
            left = listexpr();
        }

        List<Sexpr> sexprs = exprs();
        consume(TokenType.RIGHT_PAREN, "Expect ')' to end listexpr.");

        return new Sexpr.ListExpr(left, sexprs);
    }

    // exprs => epsilon | exprs sexp
    private List<Sexpr> exprs() {
        List<Sexpr> sexprs = new List<Sexpr>();

        while (!match(TokenType.RIGHT_PAREN))
        { // looking for 0 or more sexpr
            sexprs.Add(sexpr());
        }

        return sexprs;
    }

    private Boolean match(params TokenType[] types) {
        foreach (TokenType type in types)
        {
            if (check(type))
            {
                return true;
            }
        }

        return false;
    }

    private Token consume(TokenType type, String message)
    {
        if (check(type)) return advance();

        throw error(peek(), message);
    }

    private Boolean check(TokenType type)
    {
        if (isAtEnd()) return false;
        return peek().type == type;
    }

    private Token advance()
    {
        if (!isAtEnd()) current++;
        return previous();
    }

    private Boolean isAtEnd()
    {
        return peek().type == TokenType.EOF;
    }

    private Token peek()
    {
        return tokens[current];
    }

    private Token previous()
    {
        return tokens[current - 1];
    }

    private ParseError error(Token token, String message)
    {
        Lisp.error(token, message);
        return new ParseError();
    }
}
