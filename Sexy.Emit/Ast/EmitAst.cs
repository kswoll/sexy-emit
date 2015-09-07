﻿namespace Sexy.Emit.Ast
{
    public static class EmitAst
    {
        public static EmitBlockStatement Block(params IEmitStatement[] statements)
        {
            return new EmitBlockStatement(statements);
        }

        public static EmitExpressionStatement Express(this EmitBlockStatement block, EmitExpression expression)
        {
            var statement = new EmitExpressionStatement(expression);
            block.Statements.Add(statement);
            return statement;
        }

        public static EmitReturnStatement ReturnNull(this EmitBlockStatement block)
        {
            var statement = new EmitReturnStatement(Null());
            block.Statements.Add(statement);
            return statement;
        }

        public static EmitLiteralExpression Null()
        {
            return new EmitLiteralExpression();
        }

        public static EmitReturnStatement Return(this EmitBlockStatement block, EmitExpression expression)
        {
            var result = expression.Return();
            block.Statements.Add(result);
            return result;
        }

        public static EmitReturnStatement Return(this EmitExpression expression)
        {
            return new EmitReturnStatement(expression);
        }

        public static EmitBinaryExpression Assign(this EmitExpression left, EmitExpression right)
        {
            return new EmitBinaryExpression(left, EmitBinaryOperator.Assign, right);
        }

        public static EmitBinaryExpression Add(this EmitExpression left, EmitExpression right)
        {
            return new EmitBinaryExpression(left, EmitBinaryOperator.Add, right);
        }

        public static EmitBinaryExpression AddAssign(this EmitExpression left, EmitExpression right)
        {
            return new EmitBinaryExpression(left, EmitBinaryOperator.AddAssign, right);
        }

        public static EmitBinaryExpression Subtract(this EmitExpression left, EmitExpression right)
        {
            return new EmitBinaryExpression(left, EmitBinaryOperator.Subtract, right);
        }

        public static EmitBinaryExpression SubtractAssign(this EmitExpression left, EmitExpression right)
        {
            return new EmitBinaryExpression(left, EmitBinaryOperator.SubtractAssign, right);
        }

        public static EmitBinaryExpression Multiply(this EmitExpression left, EmitExpression right)
        {
            return new EmitBinaryExpression(left, EmitBinaryOperator.Multiply, right);
        }

        public static EmitBinaryExpression MultiplyAssign(this EmitExpression left, EmitExpression right)
        {
            return new EmitBinaryExpression(left, EmitBinaryOperator.MultiplyAssign, right);
        }

        public static EmitBinaryExpression Divide(this EmitExpression left, EmitExpression right)
        {
            return new EmitBinaryExpression(left, EmitBinaryOperator.Divide, right);
        }

        public static EmitBinaryExpression DivideAssign(this EmitExpression left, EmitExpression right)
        {
            return new EmitBinaryExpression(left, EmitBinaryOperator.DivideAssign, right);
        }

        public static EmitBinaryExpression Modulus(this EmitExpression left, EmitExpression right)
        {
            return new EmitBinaryExpression(left, EmitBinaryOperator.Modulus, right);
        }

        public static EmitBinaryExpression ModulusAssign(this EmitExpression left, EmitExpression right)
        {
            return new EmitBinaryExpression(left, EmitBinaryOperator.ModulusAssign, right);
        }

        public static EmitBinaryExpression Equals(this EmitExpression left, EmitExpression right)
        {
            return new EmitBinaryExpression(left, EmitBinaryOperator.Equals, right);
        }

        public static EmitBinaryExpression NotEquals(this EmitExpression left, EmitExpression right)
        {
            return new EmitBinaryExpression(left, EmitBinaryOperator.NotEquals, right);
        }

        public static EmitBinaryExpression LessThan(this EmitExpression left, EmitExpression right)
        {
            return new EmitBinaryExpression(left, EmitBinaryOperator.LessThan, right);
        }

        public static EmitBinaryExpression GreaterThan(this EmitExpression left, EmitExpression right)
        {
            return new EmitBinaryExpression(left, EmitBinaryOperator.GreaterThan, right);
        }

        public static EmitBinaryExpression LessThanOrEqual(this EmitExpression left, EmitExpression right)
        {
            return new EmitBinaryExpression(left, EmitBinaryOperator.LessThanOrEqual, right);
        }

        public static EmitBinaryExpression GreaterThanOrEqual(this EmitExpression left, EmitExpression right)
        {
            return new EmitBinaryExpression(left, EmitBinaryOperator.GreaterThanOrEqual, right);
        }

        public static EmitBinaryExpression BooleanAnd(this EmitExpression left, EmitExpression right)
        {
            return new EmitBinaryExpression(left, EmitBinaryOperator.BooleanAnd, right);
        }

        public static EmitBinaryExpression BooleanOr(this EmitExpression left, EmitExpression right)
        {
            return new EmitBinaryExpression(left, EmitBinaryOperator.BooleanOr, right);
        }

        public static EmitBinaryExpression BitwiseAnd(this EmitExpression left, EmitExpression right)
        {
            return new EmitBinaryExpression(left, EmitBinaryOperator.BitwiseAnd, right);
        }

        public static EmitBinaryExpression BitwiseAndAssign(this EmitExpression left, EmitExpression right)
        {
            return new EmitBinaryExpression(left, EmitBinaryOperator.BitwiseAndAssign, right);
        }

        public static EmitBinaryExpression BitwiseOr(this EmitExpression left, EmitExpression right)
        {
            return new EmitBinaryExpression(left, EmitBinaryOperator.BitwiseOr, right);
        }

        public static EmitBinaryExpression BitwiseOrAssign(this EmitExpression left, EmitExpression right)
        {
            return new EmitBinaryExpression(left, EmitBinaryOperator.BitwiseOrAssign, right);
        }

        public static EmitBinaryExpression ShiftLeft(this EmitExpression left, EmitExpression right)
        {
            return new EmitBinaryExpression(left, EmitBinaryOperator.ShiftLeft, right);
        }

        public static EmitBinaryExpression ShiftLeftAssign(this EmitExpression left, EmitExpression right)
        {
            return new EmitBinaryExpression(left, EmitBinaryOperator.ShiftLeftAssign, right);
        }

        public static EmitBinaryExpression ShiftRight(this EmitExpression left, EmitExpression right)
        {
            return new EmitBinaryExpression(left, EmitBinaryOperator.ShiftRight, right);
        }

        public static EmitBinaryExpression ShiftRightAssign(this EmitExpression left, EmitExpression right)
        {
            return new EmitBinaryExpression(left, EmitBinaryOperator.ShiftRightAssign, right);
        }

        public static EmitExpressionStatement Express(this EmitExpression expression)
        {
            return new EmitExpressionStatement(expression);
        }

        public static EmitForStatement For(EmitVariableDeclarationStatement initializer = null, EmitExpression predicate = null,
            IEmitStatement incrementor = null, IEmitStatement body = null)
        {
            return new EmitForStatement(initializer, predicate, incrementor, body);
        }

        public static EmitVariableDeclarationStatement Declare(params EmitVariable[] variables)
        {
            return new EmitVariableDeclarationStatement(variables);
        }

        public static EmitVariableDeclarationStatement Declare(IEmitType type)
        {
            return new EmitVariableDeclarationStatement(new EmitVariable(type));
        }

        public static EmitLiteralExpression Literal(bool value)
        {
            return new EmitLiteralExpression(value);
        }

        public static EmitLiteralExpression Literal(int value)
        {
            return new EmitLiteralExpression(value);
        }

        public static EmitLiteralExpression Literal(long value)
        {
            return new EmitLiteralExpression(value);
        }

        public static EmitLiteralExpression Literal(float value)
        {
            return new EmitLiteralExpression(value);
        }

        public static EmitLiteralExpression Literal(double value)
        {
            return new EmitLiteralExpression(value);
        }

        public static EmitLiteralExpression Literal(string value)
        {
            return new EmitLiteralExpression(value);
        }
    }
}
