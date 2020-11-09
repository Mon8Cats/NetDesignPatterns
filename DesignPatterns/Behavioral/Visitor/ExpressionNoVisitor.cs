using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Behavioral.Visitor
{
    public class ExpressionNoVisitor
    {
        public void Run()
        {
            var e = new AdditionExpression(
                new DoubleExpression(1),
                new AdditionExpression(
                        new DoubleExpression(2),
                        new DoubleExpression(3)
                    )
                );

            var sb = new StringBuilder();
            e.Print(sb);
            Console.WriteLine(sb);
        }
    }

    public abstract class Expression
    {
        public abstract void Print(StringBuilder sb);
    }

    public class DoubleExpression : Expression
    {
        private double _value;
        public DoubleExpression(double value)
        {
            _value = value;
        }
        public override void Print(StringBuilder sb)
        {
            sb.Append(_value);
        }
    }

    public class AdditionExpression : Expression
    {
        private Expression _left, _right;

        public AdditionExpression(Expression left, Expression right)
        {
            _left = left ?? throw new ArgumentNullException(paramName: nameof(left));
            _right = right ?? throw new ArgumentNullException(paramName: nameof(right));

        }
        public override void Print(StringBuilder sb)
        {
            sb.Append("(");
            _left.Print(sb);
            sb.Append("+");
            _right.Print(sb);
            sb.Append(")");
        }
    }
}
