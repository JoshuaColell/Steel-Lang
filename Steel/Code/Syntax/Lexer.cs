using SteelCompiler.Code;

namespace SteelCompiler.Code.Syntax {
    internal class Lexer {
        private readonly string _text;
        private int _position;
        private DiagnosticBag _diagnostics = new DiagnosticBag();

        public Lexer(string text) {
            _text = text;
        }

        public DiagnosticBag Diagnostics => _diagnostics;

        private char Current => Peek(0);
        private char Lookahead => Peek(1);

        private char Peek(int offset) {
            var index = _position + offset;
            if (index >= _text.Length)
                return '\0';
                
            return _text[index];
        }

        private void Next() {
            _position++;
        }

        public SyntaxToken Lex() {

            if (_position >= _text.Length) {
                return new SyntaxToken(SyntaxKind.EndOfFileToken, _position, "\0", null);
            }

            var start = _position;

            if (char.IsDigit(Current)) {
                while (char.IsDigit(Current))
                    Next();
                
                var length = _position - start;
                var text = _text.Substring(start, length);
                if (!int.TryParse(text, out var value)) {
                    _diagnostics.ReportInvalidNumber(new TextSpan(start, length), _text, typeof(int));
                }

                return new SyntaxToken(SyntaxKind.NumberToken, start, text, value);
            }

            if (char.IsWhiteSpace(Current)) {
                while (char.IsWhiteSpace(Current))
                    Next();
                
                var length = _position - start;
                var text = _text.Substring(start, length);
                return new SyntaxToken(SyntaxKind.WhitespaceToken, start, text, null);
            }

            if (char.IsLetter(Current)) {
                while (char.IsLetter(Current))
                    Next();

                var length = _position - start;
                var text = _text.Substring(start, length);
                var kind = SyntaxFacts.GetKeywordKind(text);
                return new SyntaxToken(kind, start, text, null);
            }

            switch(Current) {
                // Math Operators (MO)
                case '+':
                    return new SyntaxToken(SyntaxKind.PlusToken, _position++, "+", null);
                case '-':
                    return new SyntaxToken(SyntaxKind.MinusToken, _position++, "-", null);
                case '^':
                    return new SyntaxToken(SyntaxKind.PowerToken, _position++, "^", null);
                case '*':
                    return new SyntaxToken(SyntaxKind.StarToken, _position++, "*", null);
                case '%':
                    return new SyntaxToken(SyntaxKind.ModularToken, _position++, "%", null);
                case '/':
                    return new SyntaxToken(SyntaxKind.SlashToken, _position++, "/", null);
                case '??':
                    return new SyntaxToken(SyntaxKind.SlashToken, _position++, "??", null);
                case '(':
                    return new SyntaxToken(SyntaxKind.OpenParenToken, _position++, "(", null);
                case ')':
                    return new SyntaxToken(SyntaxKind.CloseParenToken, _position++, ")", null);
                
                // Boolean Operators (BO)
                case '!':
                    if (Lookahead == '=') {
                        _position += 2;
                        return new SyntaxToken(SyntaxKind.NotEqualsToToken, start, "!=", null);
                    }
                    else
                        return new SyntaxToken(SyntaxKind.NotToken, _position++, "!", null);

                // Other Essential Operators (OEO)
                case '&':
                    if (Lookahead == '&') {
                        _position += 2;
                        return new SyntaxToken(SyntaxKind.AndToken, start, "&&", null);
                    }
                    break;
                case '|':
                    if (Lookahead == '|') {
                        _position += 2;
                        return new SyntaxToken(SyntaxKind.OrToken, start, "||", null);
                    }
                    break;
                case '=':
                    if (Lookahead == '=') {
                        _position += 2;
                        return new SyntaxToken(SyntaxKind.EqualsToToken, start, "==", null);
                    }
                    break;
            }

            _diagnostics.ReportBadCharacter(_position, Current);
            return new SyntaxToken(SyntaxKind.BadToken, _position++, _text.Substring(_position - 1, 1), null);
        }
    }
}