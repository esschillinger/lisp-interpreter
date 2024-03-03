using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class Sexpr
{
    public class ListExpr : Sexpr {
        public ListExpr(Sexpr left, List<Sexpr> sexprs) {
            this.left = left;
            this.sexprs = sexprs;
        }

        public Sexpr left; // atom (sym, num) or listexpr; basically an sexpr without the nil option
        public List<Sexpr> sexprs; // optional sexprs
    }

    public class Atom : Sexpr {
        public Atom(Token value) {
            this.value = value;
        }

        public Token value;
    }

    public class Nil : Sexpr {
        public Nil() { }

        public Object value = null;
    }
}
