namespace SteelCompiler.Code.Syntax {
    public sealed class ParenExpressionSyntax : ExpressionSyntax {
        public ParenExpressionSyntax(SyntaxToken openParentToken, ExpressionSyntax expression, SyntaxToken closeParenToken) {
            OpenParenToken = openParentToken;
            Expression = expression;
            CloseParenToken = closeParenToken;
        }

        public override SyntaxKind Kind => SyntaxKind.ParenExpression;
        public SyntaxToken OpenParenToken { get; }
        public ExpressionSyntax Expression { get; }
        public SyntaxToken CloseParenToken { get; }

        public override IEnumerable<SyntaxNode> GetChildren() {
            yield return OpenParenToken;
            yield return Expression;
            yield return CloseParenToken;
        }
    }
}