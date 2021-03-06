using System;

using SteelCompiler.Code.Syntax;

namespace SteelCompiler.Code.Binding {
    internal sealed class Binder {
        private readonly DiagnosticBag _diagnostics = new DiagnosticBag();

        public DiagnosticBag Diagnostics => _diagnostics;

        public BoundExpression BindExpression(ExpressionSyntax syntax) {
            switch(syntax.Kind) {
                case SyntaxKind.LiteralExpressionSyntax:
                    return BindLiteralExpression((LiteralExpressionSyntax)syntax);
                case SyntaxKind.UnaryExpressionSyntax:
                    return BindUnaryExpression((UnaryExpressionSyntax)syntax);
                case SyntaxKind.BinaryExpressionSyntax:
                    return BindBinaryExpression((BinaryExpressionSyntax)syntax);
                case SyntaxKind.ParenExpression:
                    return BindExpression(((ParenExpressionSyntax)syntax).Expression);
                default:
                    throw new Exception($"Unexpected syntax {syntax.Kind}");
            }
        }
        private BoundExpression BindLiteralExpression(LiteralExpressionSyntax syntax) {
            var value = syntax.Value ?? 0;
            return new BoundLiteralExpression(value);
        }

        private BoundExpression BindUnaryExpression(UnaryExpressionSyntax syntax) {
            var boundOperand = BindExpression(syntax.Operand);
            var boundOperator = BoundUnaryOperator.Bind(syntax.OperatorToken.Kind, boundOperand.Type);

            if (boundOperator == null) {
                _diagnostics.ReportUndefinedUnaryOperator(syntax.OperatorToken.Span, syntax.OperatorToken.Text, boundOperand.Type);
                return boundOperand;
            }

            return new BoundUnaryExpression(boundOperator, boundOperand);
        }
        
        private BoundExpression BindBinaryExpression(BinaryExpressionSyntax syntax) {
            var boundLeft = BindExpression(syntax.Left);
            var boundRight = BindExpression(syntax.Right);
            var boundOperator = BoundBinaryOperator.Bind(syntax.OperatorToken.Kind, boundLeft.Type, boundRight.Type);

            if (boundOperator == null) {
                _diagnostics.ReportUndefinedBinaryOperator(syntax.OperatorToken.Span, syntax.OperatorToken.Text, boundLeft.Type, boundRight.Type);
                return boundLeft;
            }

            return new BoundBinaryExpression(boundLeft, boundOperator, boundRight);
        }

        private BoundUnaryOperatorKind? BindUnaryOperatorKind(SyntaxKind kind, Type operandType) {
            if (operandType == typeof(int)) {
                switch(kind) {
                    case SyntaxKind.PlusToken:
                        return BoundUnaryOperatorKind.Identity;
                    case SyntaxKind.MinusToken:
                        return BoundUnaryOperatorKind.Negation;
                }
            }

            if (operandType == typeof(bool)) {
                switch(kind) {
                    case SyntaxKind.NotToken:
                        return BoundUnaryOperatorKind.LogicalNot;
                }
            }

            return null;
        }

        private BoundBinaryOperatorKind? BindBinaryOperatorKind(SyntaxKind kind, Type leftType, Type rightType) {
            if (leftType == typeof(int) && rightType == typeof(int)) {
                switch(kind) {
                    case SyntaxKind.PlusToken:
                        return BoundBinaryOperatorKind.Addition;
                    case SyntaxKind.MinusToken:
                        return BoundBinaryOperatorKind.Subtraction;
                    case SyntaxKind.PowerToken:
                        return BoundBinaryOperatorKind.Powering;
                    case SyntaxKind.StarToken:
                        return BoundBinaryOperatorKind.Multiplication;
                    case SyntaxKind.ModularToken:
                        return BoundBinaryOperatorKind.Modulation;
                    case SyntaxKind.SlashToken:
                        return BoundBinaryOperatorKind.Division;
                }
            }

            if (leftType == typeof(bool) && rightType == typeof(bool)) {
                switch(kind) {
                    case SyntaxKind.AndToken:
                        return BoundBinaryOperatorKind.LogicalAnd;
                    case SyntaxKind.OrToken:
                        return BoundBinaryOperatorKind.LogicalOr;
                }
            }
            
            return null;
        }
    }
}