using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SexprPrinter
{
    private Sexpr sexpr;
    public SexprPrinter(Sexpr sexpr) {
        this.sexpr = sexpr;
    }

    public string Print(Sexpr sexpr) {
        if (sexpr is Sexpr.Nil) {
            //Console.Write("()");
            return "()";
        }

        if (sexpr is Sexpr.Atom) {
            Sexpr.Atom atom = (Sexpr.Atom)sexpr;
            Token t = (Token) atom.value;
            //Console.Write(t.lexeme, " ");
            return t.lexeme + " ";
        }

        // sexpr is listexpr

        Sexpr.ListExpr listexpr = (Sexpr.ListExpr) sexpr;

        string output = "(";
        //Console.Write("(");
        output += Print(listexpr.left);
        foreach (Sexpr s in listexpr.sexprs) {
            output += " ";
            //Console.Write(" ");
            output += Print(s);
        }
        return output + ")";
        //Console.Write(")");
    }

    public override string ToString() {
        return Print(sexpr);
    }
}
