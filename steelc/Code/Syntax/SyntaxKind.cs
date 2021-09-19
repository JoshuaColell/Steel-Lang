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
        IdentifierToken,

        // Keywords
        FalseKeyword,
        TrueKeyword,

        // Expressions
        LiteralExpressionSyntax,
        BinaryExpressionSyntax,
        ParenExpression,
        UnaryExpressionSyntax,
    }
}