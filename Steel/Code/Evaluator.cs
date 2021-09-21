using System;

using SteelCompiler.Code.Binding;

namespace SteelCompiler.Code {
    internal sealed class Evaluator {
        private readonly BoundExpression _root;

        public Evaluator(BoundExpression root) {
            _root = root;
        }

        public object Evaluate() {
            return EvaluateExpression(_root);
        }

        private object EvaluateExpression(BoundExpression node) {
            if (node is BoundLiteralExpression l)
                return l.Value;
            
            if (node is BoundUnaryExpression u) {
                var operand = EvaluateExpression(u.Operand);

                switch(u.Op.Kind) {
                    case BoundUnaryOperatorKind.Identity:
                        return (int) operand;
                    case BoundUnaryOperatorKind.Negation:
                        return -(int) operand;
                    case BoundUnaryOperatorKind.LogicalNot:
                        return !(bool) operand;
                    default:
                        throw new Exception($"Unexpected unary operator {u.Op}");
                }
            }

            if (node is BoundBinaryExpression b) {
                var left = EvaluateExpression(b.Left);
                var right = EvaluateExpression(b.Right);

                switch(b.Op.Kind) {
                    case BoundBinaryOperatorKind.Addition:
                        return (int) left + (int) right;
                    case BoundBinaryOperatorKind.Subtraction:
                        return (int) left - (int) right;
                    case BoundBinaryOperatorKind.Multiplication:
                        return (int) left * (int) right;
                    case BoundBinaryOperatorKind.Division:
                        if ((int) left == 0 || (int) right == 0) return 0;
                        if ((int) left == 0 && (int) right == 0) return 0;
                        
                        return (int) left / (int) right;
                    case BoundBinaryOperatorKind.LogicalAnd:
                        return (bool) left && (bool) right;
                    case BoundBinaryOperatorKind.LogicalOr:
                        return (bool) left || (bool) right;
                    case BoundBinaryOperatorKind.EqualsTo:
                        return Equals(left, right);
                    case BoundBinaryOperatorKind.NotEqualsTo:
                        return !Equals(left, right);
                    default:
                        throw new Exception($"Unexpected binary operator {b.Op}");
                }
            }

            throw new Exception($"Unexpected node {node.Kind}");
        }
    }
}