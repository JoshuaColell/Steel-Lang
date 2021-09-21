using System.Collections.Generic;
using System.Linq;

namespace SteelCompiler.Code {
    public sealed class EvaluationResult {
        public EvaluationResult(IEnumerable<Diagnostic> diagnostics, object value) {
            Diagnostics = diagnostics.ToArray();
            Value = value;
        }

        public IReadOnlyList<Diagnostic> Diagnostics { get; }
        public object Value { get; }
    }
}