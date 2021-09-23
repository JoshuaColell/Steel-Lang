namespace SteelCompiler.Code.Syntax {
    public enum SyntaxKind {
        // Tokens
        BadToken,
        EndOfFileToken,
        WhitespaceToken,
        NumberToken,
        PlusToken,
        MinusToken,
        PowerToken,
        StarToken,
        ModularToken,
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