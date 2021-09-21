using System;
using System.Collections.Generic;
using System.Linq;

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
                var compilation = new Compilation(syntaxTree);
                var result = compilation.Evaluate();

                var diagnostics = result.Diagnostics;

                if (showTree) {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    PrettyPrint(syntaxTree.Root);
                    Console.ResetColor();
                }

                if (!diagnostics.Any()) {
                    Console.WriteLine(result.Value);
                }
                else {
                    foreach (var diagnostic in diagnostics) {
                        Console.WriteLine();

                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(diagnostic);
                        Console.ResetColor();

                        var prefix = line.Substring(0, diagnostic.Span.Start);
                        var error = line.Substring(diagnostic.Span.Start, diagnostic.Span.Length);
                        var suffix = line.Substring(diagnostic.Span.End);

                        Console.Write("    ");
                        Console.Write(prefix);

                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write(error);
                        Console.ResetColor();

                        Console.Write(suffix);

                        Console.WriteLine();
                    }

                    Console.WriteLine();
                }
            }
        }

        static void PrettyPrint(SyntaxNode node, string indent = "", bool isLast = true) {
            var marker = isLast ? "└── " : "├── ";

            Console.Write(indent);
            Console.Write(marker);
            Console.Write(node.Kind);

            if (node is SyntaxToken t && t.Value != null) {
                Console.Write(" ");
                Console.Write(t.Value);
            }

            Console.WriteLine();

            indent += isLast ? "   " : "│  ";

            var lastChild = node.GetChildren().LastOrDefault();

            foreach (var child in node.GetChildren())
                PrettyPrint(child, indent, child == lastChild);
        }
    }
}
