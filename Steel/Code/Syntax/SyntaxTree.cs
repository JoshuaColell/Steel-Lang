using System.Collections.Generic;
using System.Linq;

namespace SteelCompiler.Code.Syntax {
    public sealed class SyntaxTree {
        public SyntaxTree(IEnumerable<Diagnostic> diagnostics, ExpressionSyntax root, SyntaxToken EOFToken) {
            Diagnostics = diagnostics.ToArray();
            Root = root;
            EOFT = EOFToken;
        }

        public IReadOnlyList<Diagnostic> Diagnostics { get; }
        public ExpressionSyntax Root { get; }
        public SyntaxToken EOFT { get; }

        public static SyntaxTree Parse(string text) {
            var parser = new Parser(text);
            return parser.Parse();
        }
    }
}