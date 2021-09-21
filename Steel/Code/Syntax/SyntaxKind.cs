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
        NotToken,
        AndToken,
        OrToken,
        EqualsToToken,
        NotEqualsToToken,
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