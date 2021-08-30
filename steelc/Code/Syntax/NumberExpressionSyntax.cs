namespace SteelCompiler.Code.Syntax {
    public sealed class NumberExpressionSyntax : ExpressionSyntax {
        public NumberExpressionSyntax(SyntaxToken numberToken) {
            NumberToken = numberToken;
        }

        public override SyntaxKind Kind => SyntaxKind.NumberExpressionSyntax;
        public SyntaxToken NumberToken { get; }

        public override IEnumerable<SyntaxNode> GetChildren() {
            yield return NumberToken;
        }
    }
}