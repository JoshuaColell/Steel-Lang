using System.Collections.Generic;

namespace SteelCompiler.Code.Syntax {
    public abstract class SyntaxNode {
        public abstract SyntaxKind Kind { get; }

        public abstract IEnumerable<SyntaxNode> GetChildren();
    }
}