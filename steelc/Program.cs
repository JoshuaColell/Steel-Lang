using SteelCompiler.Code;
using SteelCompiler.Code.Syntax;
using SteelCompiler.Code.Binding;

namespace SteelCompiler {
    internal static class Program {
        private static void Main(string[] args) {
            var showTree = false;

            while (true) {
                Console.Write("@> ");
                var line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                
                if (line == "###showlangtree") {
                    Console.WriteLine("{STEEL DEBUG FUNCTION}");
                    continue;
                }
                else if (line == "###showlangtree on" || line == "###showlangtree 1" || line == "###showlangtree true") {
                    if (showTree == true) {
                        Console.WriteLine("Show language tree is already on! {STEEL DEBUG}");
                        continue;
                    } else {
                        showTree = !showTree;
                        Console.WriteLine("Show language tree is now on! {STEEL DEBUG}");
                        continue;
                    }
                }
                else if (line == "###showlangtree off" || line == "###showlangtree 0" || line == "###showlangtree false") {
                    if (showTree == false) {
                        Console.WriteLine("Show language tree is already off! {STEEL DEBUG}");
                        continue;
                    } else {
                        showTree = !showTree;
                        Console.WriteLine("Show language tree is now off! {STEEL DEBUG}");
                        continue;
                    }
                }
                else if (line == "cls" || line == "clear") {
                    Console.Clear();
                    continue;
                }

                var syntaxTree = SyntaxTree.Parse(line);
                var binder = new Binder();
                var boundExpression = binder.BindExpression(syntaxTree.Root);

                var diagnostics = syntaxTree.Diagnostics.Concat(binder.Diagnostics).ToArray();

                if (showTree) {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    PrettyPrint(syntaxTree.Root);
                    Console.ResetColor();
                }

                if (!diagnostics.Any()) {
                    var e = new Evaluator(boundExpression);
                    var result = e.Evaluate();
                    Console.WriteLine(result);
                }
                else {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    
                    foreach (var diagnostic in diagnostics) {
                        Console.WriteLine(diagnostic);
                    }
                    
                    Console.ResetColor();
                }
            }
        }

        static void PrettyPrint(SyntaxNode node, string indent = "") {
            Console.Write(indent);
            Console.Write(node.Kind);

            if (node is SyntaxToken t && t.Value != null) {
                Console.Write(" ");
                Console.Write(t.Value);
            }

            Console.WriteLine();

            indent += "    ";

            foreach (var child in node.GetChildren())
                PrettyPrint(child, indent);
        }
    }
}
