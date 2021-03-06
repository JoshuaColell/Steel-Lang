using System;

using SteelCompiler.Code.Syntax;

namespace SteelCompiler.Code.Binding {
    internal sealed class BoundBinaryOperator {
        private BoundBinaryOperator(SyntaxKind syntaxKind, BoundBinaryOperatorKind kind, Type type)
            : this(syntaxKind, kind, type, type, type)
        {
        }

        private BoundBinaryOperator(SyntaxKind syntaxKind, BoundBinaryOperatorKind kind, Type operandType, Type type)
            : this(syntaxKind, kind, operandType, operandType, type)
        {
        }

        private BoundBinaryOperator(SyntaxKind syntaxKind, BoundBinaryOperatorKind kind, Type leftType, Type rightType, Type type) {
            SyntaxKind = syntaxKind;
            Kind = kind;
            LeftType = leftType;
            RightType = rightType;
            Type = type;
        }

        public SyntaxKind SyntaxKind { get; }
        public BoundBinaryOperatorKind Kind { get; }
        public Type LeftType { get; }
        public Type RightType { get; }
        public Type Type { get; }

        private static BoundBinaryOperator[] _operators = {
            new BoundBinaryOperator(SyntaxKind.PlusToken, BoundBinaryOperatorKind.Addition, typeof(int)),
            new BoundBinaryOperator(SyntaxKind.MinusToken, BoundBinaryOperatorKind.Subtraction, typeof(int)),
            new BoundBinaryOperator(SyntaxKind.PowerToken, BoundBinaryOperatorKind.Powering, typeof(int)),
            new BoundBinaryOperator(SyntaxKind.StarToken, BoundBinaryOperatorKind.Multiplication, typeof(int)),
            new BoundBinaryOperator(SyntaxKind.ModularToken, BoundBinaryOperatorKind.Modulation, typeof(int)),
            new BoundBinaryOperator(SyntaxKind.SlashToken, BoundBinaryOperatorKind.Division, typeof(int)),
            new BoundBinaryOperator(SyntaxKind.EqualsToToken, BoundBinaryOperatorKind.EqualsTo, typeof(int), typeof(bool)),
            new BoundBinaryOperator(SyntaxKind.NotEqualsToToken, BoundBinaryOperatorKind.NotEqualsTo, typeof(int), typeof(bool)),

            new BoundBinaryOperator(SyntaxKind.AndToken, BoundBinaryOperatorKind.LogicalAnd, typeof(bool)),
            new BoundBinaryOperator(SyntaxKind.OrToken, BoundBinaryOperatorKind.LogicalOr, typeof(bool)),
            new BoundBinaryOperator(SyntaxKind.EqualsToToken, BoundBinaryOperatorKind.EqualsTo, typeof(bool)),
            new BoundBinaryOperator(SyntaxKind.NotEqualsToToken, BoundBinaryOperatorKind.NotEqualsTo, typeof(bool)),
        };

        public static BoundBinaryOperator Bind(SyntaxKind syntaxKind, Type leftType, Type rightType) {
            foreach (var op in _operators) {
                if (op.SyntaxKind == syntaxKind && op.LeftType ==  leftType && op.RightType == rightType)
                    return op;
            }
            
            return null;
        }
    }
}