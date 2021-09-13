using SteelCompiler.Code;
using SteelCompiler.Code.Syntax;

namespace SteelCompiler {
    internal static class Program {
        private static void Main(string[] args) {
            var showTree = false;

            while (true) {
                Console.Write("$> ");
                var line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    return;
                
                if (line == "#showtree") {
                    showTree = !showTree;
                    Console.WriteLine(showTree ? "Showing parse trees" : "Not showing parse trees anymore");
                    continue;
                }
                else if (line == "cls" || line == "clear") {
                    Console.Clear();
                    continue;
                }

                var syntaxTree = SyntaxTree.Parse(line);

                if (showTree) {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    PrettyPrint(syntaxTree.Root);
                    Console.ResetColor();
                }

                if (!syntaxTree.Diagnostics.Any()) {
                    var e = new Evaluator(syntaxTree.Root);
                    var result = e.Evaluate();
                    Console.WriteLine(result);
                }
                else {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    
                    foreach (var diagnostic in syntaxTree.Diagnostics) {
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
