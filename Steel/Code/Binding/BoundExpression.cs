using System;

namespace SteelCompiler.Code.Binding {
    internal abstract class BoundExpression : BoundNode {
        public abstract Type Type { get; }
    }
}