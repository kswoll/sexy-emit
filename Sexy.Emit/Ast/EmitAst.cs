namespace Sexy.Emit.Ast
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

        public static EmitBinaryExpression Assign(this EmitExpression left, EmitBinaryOperator op, EmitExpression right)
        {
            return new EmitBinaryExpression(left, op, right);
        }

        public static EmitBinaryExpression Add(this EmitExpression left, EmitBinaryOperator op, EmitExpression right)
        {
            return new EmitBinaryExpression(left, op, right);
        }

        public static EmitBinaryExpression AddAssign(this EmitExpression left, EmitBinaryOperator op, EmitExpression right)
        {
            return new EmitBinaryExpression(left, op, right);
        }

        public static EmitBinaryExpression Subtract(this EmitExpression left, EmitBinaryOperator op, EmitExpression right)
        {
            return new EmitBinaryExpression(left, op, right);
        }

        public static EmitBinaryExpression SubtractAssign(this EmitExpression left, EmitBinaryOperator op, EmitExpression right)
        {
            return new EmitBinaryExpression(left, op, right);
        }

        public static EmitBinaryExpression Multiply(this EmitExpression left, EmitBinaryOperator op, EmitExpression right)
        {
            return new EmitBinaryExpression(left, op, right);
        }

        public static EmitBinaryExpression MultiplyAssign(this EmitExpression left, EmitBinaryOperator op, EmitExpression right)
        {
            return new EmitBinaryExpression(left, op, right);
        }

        public static EmitBinaryExpression Divide(this EmitExpression left, EmitBinaryOperator op, EmitExpression right)
        {
            return new EmitBinaryExpression(left, op, right);
        }

        public static EmitBinaryExpression DivideAssign(this EmitExpression left, EmitBinaryOperator op, EmitExpression right)
        {
            return new EmitBinaryExpression(left, op, right);
        }

        public static EmitBinaryExpression Modulus(this EmitExpression left, EmitBinaryOperator op, EmitExpression right)
        {
            return new EmitBinaryExpression(left, op, right);
        }

        public static EmitBinaryExpression ModulusAssign(this EmitExpression left, EmitBinaryOperator op, EmitExpression right)
        {
            return new EmitBinaryExpression(left, op, right);
        }

        public static EmitBinaryExpression Equals(this EmitExpression left, EmitBinaryOperator op, EmitExpression right)
        {
            return new EmitBinaryExpression(left, op, right);
        }

        public static EmitBinaryExpression NotEquals(this EmitExpression left, EmitBinaryOperator op, EmitExpression right)
        {
            return new EmitBinaryExpression(left, op, right);
        }

        public static EmitBinaryExpression LessThan(this EmitExpression left, EmitBinaryOperator op, EmitExpression right)
        {
            return new EmitBinaryExpression(left, op, right);
        }

        public static EmitBinaryExpression GreaterThan(this EmitExpression left, EmitBinaryOperator op, EmitExpression right)
        {
            return new EmitBinaryExpression(left, op, right);
        }

        public static EmitBinaryExpression LessThanOrEqual(this EmitExpression left, EmitBinaryOperator op, EmitExpression right)
        {
            return new EmitBinaryExpression(left, op, right);
        }

        public static EmitBinaryExpression GreaterThanOrEqual(this EmitExpression left, EmitBinaryOperator op, EmitExpression right)
        {
            return new EmitBinaryExpression(left, op, right);
        }

        public static EmitBinaryExpression BooleanAnd(this EmitExpression left, EmitBinaryOperator op, EmitExpression right)
        {
            return new EmitBinaryExpression(left, op, right);
        }

        public static EmitBinaryExpression BooleanOr(this EmitExpression left, EmitBinaryOperator op, EmitExpression right)
        {
            return new EmitBinaryExpression(left, op, right);
        }

        public static EmitBinaryExpression BitwiseAnd(this EmitExpression left, EmitBinaryOperator op, EmitExpression right)
        {
            return new EmitBinaryExpression(left, op, right);
        }

        public static EmitBinaryExpression BitwiseAndAssign(this EmitExpression left, EmitBinaryOperator op, EmitExpression right)
        {
            return new EmitBinaryExpression(left, op, right);
        }

        public static EmitBinaryExpression BitwiseOr(this EmitExpression left, EmitBinaryOperator op, EmitExpression right)
        {
            return new EmitBinaryExpression(left, op, right);
        }

        public static EmitBinaryExpression BitwiseOrAssign(this EmitExpression left, EmitBinaryOperator op, EmitExpression right)
        {
            return new EmitBinaryExpression(left, op, right);
        }

        public static EmitBinaryExpression ShiftLeft(this EmitExpression left, EmitBinaryOperator op, EmitExpression right)
        {
            return new EmitBinaryExpression(left, op, right);
        }

        public static EmitBinaryExpression ShiftLeftAssign(this EmitExpression left, EmitBinaryOperator op, EmitExpression right)
        {
            return new EmitBinaryExpression(left, op, right);
        }

        public static EmitBinaryExpression ShiftRight(this EmitExpression left, EmitBinaryOperator op, EmitExpression right)
        {
            return new EmitBinaryExpression(left, op, right);
        }

        public static EmitBinaryExpression ShiftRightAssign(this EmitExpression left, EmitBinaryOperator op, EmitExpression right)
        {
            return new EmitBinaryExpression(left, op, right);
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
