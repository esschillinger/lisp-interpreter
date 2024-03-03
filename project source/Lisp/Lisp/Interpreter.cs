using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Sexpr;

public class Interpreter
{
    List<Sexpr> sexprs;
    Dictionary<string, Object> vars = new Dictionary<string, Object>();

    public Interpreter(List<Sexpr> sexprs)
    {
        this.sexprs = sexprs;
    }

    public void interpret() {
        foreach (Sexpr s in sexprs) {
            Object e = evaluate(s);

            if (e is ConsCell)              // only ever returns an integer or a cons cell
            {
                ((ConsCell)e).Print();
                Console.WriteLine();
            }
            else if (e is Sexpr.Nil) {
                Console.WriteLine("()");
            }
            else
            {
                Console.WriteLine(e);
            }
        }
    }

    private Object evaluate(Sexpr sexpr) {
        if (sexpr is Sexpr.ListExpr) { return evaluate((Sexpr.ListExpr)sexpr); }
        else if (sexpr is Sexpr.Atom) { return evaluate((Sexpr.Atom)sexpr); }
        else { return evaluate((Sexpr.Nil)sexpr); }
    }

    private Object evaluate(Sexpr.ListExpr list) {
        Object left = list.left; // atom or listexpr

        if (left is Sexpr.Atom)
        {
            left = evaluate((Sexpr.Atom)left);
        }
        else { // left -> listexpr
            left = evaluate((Sexpr.ListExpr)left);
        }

        // evaluate list of sexprs

        if (left is string)
        {
            switch ((string)left)
            {
                case "+":
                    return (int)evaluate(list.sexprs[0]) + (int)evaluate(list.sexprs[1]);
                case "-":
                    return (int)evaluate(list.sexprs[0]) - (int)evaluate(list.sexprs[1]);
                case "*":
                    return (int)evaluate(list.sexprs[0]) * (int)evaluate(list.sexprs[1]);
                case "/":
                    return (int)evaluate(list.sexprs[0]) / (int)evaluate(list.sexprs[1]);
                case "=":
                case "eq?":
                    return eq(list.sexprs);
                case ">":
                    return (int)evaluate(list.sexprs[0]) > (int)evaluate(list.sexprs[1]);
                case "<":
                    return (int)evaluate(list.sexprs[0]) < (int)evaluate(list.sexprs[1]);
                case "define":
                    return define(list.sexprs);
                case "set":
                    return set(list.sexprs);
                case "cons":
                    return cons(list.sexprs);
                case "cond":
                    return cond(list.sexprs);
                case "car":
                    return car(list.sexprs);
                case "cdr":
                    return cdr(list.sexprs);
                case "and?":
                    return truth(list.sexprs[0]) && truth(list.sexprs[1]);
                case "or?":
                    return truth(list.sexprs[0]) || truth(list.sexprs[1]);
                case "number?":
                    return evaluate(list.sexprs[0]) is int;
                    /*if (list.sexprs[0] is Sexpr.Atom)
                    {
                        return ((Sexpr.Atom)list.sexprs[0]).value.type == TokenType.NUM;
                    }
                    return false;*/
                case "symbol?":
                    return symbol(list.sexprs[0]);
                case "list?":
                    return this.list(list.sexprs[0]);
                case "nil?":
                    return !truth(list.sexprs[0]);
                default:
                    // current symbol not a predefined function
                    // evaluate user function call :. get from vars dict
                    // return end result

                    //          0       1        2
                    // (define name (params) body_expr)
                    List<Sexpr> func_info = (List<Sexpr>)vars[(string)left];
                    Sexpr.ListExpr parameters = (Sexpr.ListExpr)func_info[0];   // list of symbols
                    Sexpr body = func_info[1];  // sexpr to evaluate

                    vars[((Sexpr.Atom)parameters.left).value.lexeme] = evaluate(list.sexprs[0]); // directly cast listexpr to Atom, since we know the parameter list is a list of symbols
                    for (int i = 0; i < parameters.sexprs.Count; i++) {
                        vars[((Sexpr.Atom)parameters.sexprs[i]).value.lexeme] = evaluate(list.sexprs[i + 1]); // map each param symbol to the sexpr list (list.sexprs)
                    }

                    Object ret = evaluate(body);

                    vars.Remove(((Sexpr.Atom)parameters.left).value.lexeme); // remove local variables from scope after call
                    foreach (Sexpr sexpr in parameters.sexprs) {
                        vars.Remove(((Sexpr.Atom)sexpr).value.lexeme);
                    }

                    return ret;
            }
        }
        else { // left is a num, so it's just meant to be a list. return it as cons cell structure

            ConsCell start = new ConsCell(left);
            ConsCell prev = start;

            foreach (Sexpr s in list.sexprs) {
                ConsCell c = new ConsCell(evaluate(s));
                prev.cdr = c;
                prev = c;
            }

            return start;
        }
    }

    private Object evaluate(Sexpr.Atom atom) {
        Token value = atom.value;
        if (value.type == TokenType.NUM) {
            return value.literal;
        }

        // atom is a symbol

        return resolve(value.lexeme); // resolve variable name
    }

    private Object evaluate(Sexpr.Nil nil) {
        return nil;
    }

    private Object resolve(string lexeme) {
        if (vars.ContainsKey(lexeme) && !(vars[lexeme] is List<Sexpr>)) { return vars[lexeme]; }
        
        return lexeme;
    }

    private Boolean eq(List<Sexpr> list) {
        Object l = evaluate(list[0]);
        Object r = evaluate(list[1]);

        if (l is int && r is int) {
            return (int)l == (int)r;
        } else if (l is Sexpr.Nil && r is Sexpr.Nil) {
            return true;
        } else if (l is ConsCell && r is ConsCell) {
            return eq_conscell((ConsCell)l, (ConsCell)r);
        }

        return false;
    }

    private Boolean eq_conscell(ConsCell left, ConsCell right) {
        Boolean cont = false;

        if (left.car is int && right.car is int)
        {
            cont = ((int)left.car == (int)right.car);
        }
        else if (left.car is Sexpr.Nil && right.car is Sexpr.Nil)
        {
            cont = true;
        }
        else if (left.car is ConsCell && right.car is ConsCell)
        {
            cont = eq_conscell((ConsCell)left.car, (ConsCell)right.car);
        }
        else { return false; }

        if (cont) {
            // cars match if we are here

            if (left.cdr is int && right.cdr is int)
            {
                return ((int)left.cdr == (int)right.cdr);
            }
            else if (left.cdr is Sexpr.Nil && right.cdr is Sexpr.Nil)
            {
                return true;
            }
            else if (left.cdr is ConsCell && right.cdr is ConsCell)
            {
                return eq_conscell((ConsCell)left.cdr, (ConsCell)right.cdr);
            }
        }

        return false;
    }

    private Object define(List<Sexpr> list) {
        //          0       1        2
        // (define name (params) body_expr)

        string func_name = ((Sexpr.Atom)list[0]).value.lexeme;
        List<Sexpr> func_info = new List<Sexpr>();
        Sexpr.ListExpr parameters = (Sexpr.ListExpr)list[1];
        Sexpr body = list[2];
        func_info.Add(parameters);
        func_info.Add(body);

        vars[func_name] = func_info;

        return "function \"" + func_name + "\" defined.";
    }

    private Object set(List<Sexpr> list) {
        string var_name = ((Sexpr.Atom)list[0]).value.lexeme;
        Object value = evaluate(list[1]); // null, int, list, or cons cell

        vars[var_name] = value;

        return "variable \"" + var_name + "\" set." ; // not sure about this, feels right though
    }

    private Object cons(List<Sexpr> list) {
        Object left = evaluate(list[0]);
        Object right = evaluate(list[1]);

        ConsCell c = new ConsCell(left, right);

        return c;
    }

    private Object cond(List<Sexpr> list) {
        for (int i = 0; i < list.Count; i++) {
            if (truth(list[i])) {
                return evaluate(list[i + 1]);
            }
            i++; // keep sexprs together as pairs
        }
        return new Sexpr.Nil();
    }

    private Object car(List<Sexpr> list) {
        ConsCell c = (ConsCell)evaluate(list[0]);
        return c.car;
    }

    private Object cdr(List<Sexpr> list) {
        ConsCell c = (ConsCell)evaluate(list[0]);
        return c.cdr;
    }

    private Boolean symbol(Sexpr sexpr) {
        if (sexpr is Sexpr.Atom)
        {
            return ((Sexpr.Atom)sexpr).value.type == TokenType.SYMBOL;
        }
        return false;
    }

    private Boolean list(Sexpr sexpr) {
        Object eval = evaluate(sexpr);
        return !(eval is Sexpr.Nil || eval is int);
    }

    private Boolean truth(Sexpr sexpr) {
        if (sexpr is Sexpr.Nil || evaluate(sexpr) is Sexpr.Nil || evaluate(sexpr) is false)
        {
            return false;
        }
        else {
            return true;
        }
    }
}
