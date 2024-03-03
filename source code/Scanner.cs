using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using static TokenType;

public class Scanner
{
    private String source;
    private List<Token> tokens = new List<Token>();
    private int start = 0;
    private int current = 0;
    private int line = 1;

    public Scanner(String source)
    {
        this.source = source;
    }

    public List<Token> scanTokens()
    {
        while (!isAtEnd())
        {
            // We are at the beginning of the next lexeme.
            start = current;
            scanToken();
        }
        tokens.Add(new Token(EOF, "", null, line));
        return tokens;
    }
    private Boolean isAtEnd()
    {
        return current >= source.Length;
    }

    private char advance()
    {
        return source[current++];
    }
    private void addToken(TokenType type)
    {
        addToken(type, null);
    }
    private void addToken(TokenType type, Object literal)
    {
        String text = source[start..current]; //second parameter needs to actually be the length of the substring
        tokens.Add(new Token(type, text, literal, line));
    }

    private void scanToken()
    {
        char c = advance();
        switch (c)
        {
            case '(':
                if (peek() == ')') {
                    advance();
                    addToken(NIL);
                } else
                {
                    addToken(LEFT_PAREN);
                }
                break;
            case ')': addToken(RIGHT_PAREN); break;
            case ';':
                while (peek() != '\n' && !isAtEnd()) advance(); // semicolon is a single-line comment
                break;
            case ' ':
            case '\r':
            case '\t':
                // Ignore whitespace.
                break;
            case '\n':
                line++;
                break;
            default:
                if (Char.IsDigit(c))
                {
                    number();
                }
                else if (Char.IsLetter(c) || c == '_' || operators(c))
                {
                    symbol();
                }
                break;
        }
    }

    private Boolean operators(char c) {
        switch (c) {
            case '-':
            case '+':
            case '*':
            case '/':
            case '>':
            case '<':
            case '=':
                return true;
            default:
                Lisp.error(line, "Unexpected character.");
                return false;
        }
    }

    private void symbol()
    {
        while (Char.IsLetter(peek()) || Char.IsNumber(peek()) || peek() == '?' || peek() == '_') advance();
        String text = source[start..current];
        addToken(SYMBOL);
    }

    private void number()
    {
        while (Char.IsDigit(peek())) advance();

        addToken(NUM, int.Parse(source[start..current]));
    }

    private char peek()
    {
        if (isAtEnd()) return '\0';
        return source[current];
    }
}
