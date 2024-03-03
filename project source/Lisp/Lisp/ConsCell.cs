using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ConsCell
{
    public Object car;
    public Object cdr;

    public ConsCell(Object car, Object cdr) {
        this.car = car;
        this.cdr = cdr;
    }

    public ConsCell(Object car) {
        this.car = car;
        cdr = new Sexpr.Nil();
    }

    public void Print() {
        Console.Write("(");

        if (car != null) {
            if (car is Sexpr.Nil) { Console.Write("()"); }
            else
            {
                rPrint(car);
            }
            Console.Write(".");
            rPrint(cdr);
        }

        Console.Write(")");
    }

    private void rPrint(Object c) {
        //if (c == null) { return; }
        if (c is ConsCell) {
            Console.Write("(");
            if (((ConsCell)c).car is Sexpr.Nil) { Console.Write("()"); }
            else { Console.Write(((ConsCell)c).car); }
            Console.Write(".");
            rPrint(((ConsCell)c).cdr);
            Console.Write(")");
            return;
        }
        // else it is a dotted cons cell
        //Console.Write(((ConsCell)c).cdr);
        if (c is Sexpr.Nil) { Console.Write("()"); }
        else { Console.Write(c); }
    }
}
