namespace SteelCompiler.Code.Syntax {
    internal static class SyntaxFacts {
        public static int GetUnaryOperatorPrecedence(this SyntaxKind kind) {
                switch(kind) {
                    case SyntaxKind.PlusToken:
                    case SyntaxKind.MinusToken:
                    case SyntaxKind.NotToken:
                        return 6;
                        
                    default:
                        return 0;
            }
        }

        public static int GetBinaryOperatorPrecedence(this SyntaxKind kind) {
                switch(kind) {
                    case SyntaxKind.PowerToken:
                        return 6;

                    case SyntaxKind.StarToken:
                    case SyntaxKind.ModularToken:
                    case SyntaxKind.SlashToken:
                        return 5;
                    
                    case SyntaxKind.PlusToken:
                    case SyntaxKind.MinusToken:
                        return 4;
                    
                    case SyntaxKind.EqualsToToken:
                    case SyntaxKind.NotEqualsToToken:
                        return 3;
                    
                    case SyntaxKind.AndToken:
                        return 2;
                    
                    case SyntaxKind.OrToken:
                        return 1;
                        
                    default:
                        return 0;
            }
        }

        public static SyntaxKind GetKeywordKind(string text) {
            switch (text) {
                case "true":
                    return SyntaxKind.TrueKeyword;
                case "false":
                    return SyntaxKind.FalseKeyword;
                default:
                    return SyntaxKind.IdentifierToken;
            }
        }
    }
}