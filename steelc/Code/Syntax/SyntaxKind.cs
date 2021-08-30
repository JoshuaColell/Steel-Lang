namespace SteelCompiler.Code.Syntax {
    public enum SyntaxKind {
        // Tokens
        BadToken,
        EndOfFileToken,
        WhitespaceToken,
        NumberToken,
        PlusToken,
        MinusToken,
        StarToken,
        SlashToken,
        OpenParenToken,
        CloseParenToken,

        // Expressions
        LiteralExpressionSyntax,
        BinaryExpressionSyntax,
        ParenExpression,
    }
}