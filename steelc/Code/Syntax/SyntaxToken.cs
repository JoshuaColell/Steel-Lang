namespace SteelCompiler.Code.Syntax {
    public class SyntaxToken : SyntaxNode {
        public SyntaxToken(SyntaxKind kind, int positon, string text, object value) {
            Kind = kind;
            Positon = positon;
            Text = text;
            Value = value;
        }

        public override SyntaxKind Kind { get; }
        public int Positon { get; }
        public string Text { get; }
        public object Value { get; }

        public override IEnumerable<SyntaxNode> GetChildren() {
            return Enumerable.Empty<SyntaxNode>();
        }
    }
}