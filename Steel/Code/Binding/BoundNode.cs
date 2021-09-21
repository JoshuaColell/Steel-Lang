namespace SteelCompiler.Code.Binding {
    internal abstract class BoundNode {
        public abstract BoundNodeKind Kind { get; }
    }
}