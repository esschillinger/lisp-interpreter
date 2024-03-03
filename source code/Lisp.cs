using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class Lisp
{
    static Boolean hadError = false;
    static Boolean hadRuntimeError = false;

    public static void Main(String[] args)
    {
        if (args.Length > 1)
        {
            Console.WriteLine("Usage: lisp [script]");
            Environment.Exit(64);
        }
        else if (args.Length == 1)
        {
            runFile(args[0]);
        }
        else
        {
            runPrompt();
        }
    }

    private static void runFile(String path)
    {
        byte[] bytes = File.ReadAllBytes(path);
        run(Encoding.Default.GetString(bytes));
        if (hadError) Environment.Exit(65);
        if (hadRuntimeError) Environment.Exit(70);
    }

    private static void runPrompt()
    {
        TextReader input = Console.In;
        TextReader reader = input;
        for (; ; )
        {
            Console.Write("> ");
            String line = reader.ReadLine();
            if (line == null) break;
            run(line);
            hadError = false;
        }
    }

    private static void run(String source)
    {
        Scanner scanner = new Scanner(source);
        List<Token> tokens = scanner.scanTokens();

        /*foreach (Token t in tokens)
        {
            Console.WriteLine(t.toString());
        }*/

        Parser parser = new Parser(tokens);
        List<Sexpr> sexprs = parser.parse();

        /*foreach (Sexpr s in sexprs)
        {
            Console.Write("sexpr type : ");
            Console.WriteLine(s);
            SexprPrinter printer = new SexprPrinter(s);
            Console.WriteLine(printer.ToString() + "\n");
        }*/

        if (hadError) return;

        Interpreter interpreter = new Interpreter(sexprs);
        interpreter.interpret();
    }

    public static void error(int line, String message)
    {
        report(line, "", message);
    }
    private static void report(int line, String where,
    String message)
    {
        Console.Error.WriteLine("[line " + line + "] Error" + where + ": " + message);
        hadError = true;
    }

    public static void error(Token token, String message)
    {
        if (token.type == TokenType.EOF)
        {
            report(token.line, " at end", message);
        }
        else
        {
            report(token.line, " at '" + token.lexeme + "'", message);
        }
    }

    public static void runtimeError(RuntimeError error)
    {
        Console.Error.WriteLine(error.Message +
        "\n[line " + error.Token.line + "]");
        hadRuntimeError = true;
    }
}