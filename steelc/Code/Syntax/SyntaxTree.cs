namespace SteelCompiler.Code.Syntax {
    public sealed class SyntaxTree {
        public SyntaxTree(IReadOnlyList<string> diagnostics, ExpressionSyntax root, SyntaxToken EOFToken) {
            Diagnostics = diagnostics.ToArray();
            Root = root;
            EOFT = EOFToken;
        }

        public IReadOnlyList<string> Diagnostics { get; }
        public ExpressionSyntax Root { get; }
        public SyntaxToken EOFT { get; }

        public static SyntaxTree Parse(string text) {
            var parser = new Parser(text);
            return parser.Parse();
        }
    }
}