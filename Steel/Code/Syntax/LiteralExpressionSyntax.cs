using System.Collections.Generic;

namespace SteelCompiler.Code.Syntax {
    public sealed class LiteralExpressionSyntax : ExpressionSyntax {
        public LiteralExpressionSyntax(SyntaxToken literalToken)
            : this(literalToken, literalToken.Value)
        {
        }
        
        public LiteralExpressionSyntax(SyntaxToken literalToken, object value) {
            LiteralToken = literalToken;
            Value = value;
        }

        public override SyntaxKind Kind => SyntaxKind.LiteralExpressionSyntax;
        public SyntaxToken LiteralToken { get; }
        public object Value { get; }

        public override IEnumerable<SyntaxNode> GetChildren() {
            yield return LiteralToken;
        }
    }
}