using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Behavioral.Visitor
{
    using DictTyp3e = Dictionary<Type, Action<Expression3, StringBuilder>>;

    public class ClassicVisitor
    {
        public void Run()
        {
            var e = new AdditionExpression3(
               new DoubleExpression3(4),
               new AdditionExpression3(
                       new DoubleExpression3(5),
                       new DoubleExpression3(6)
                   )
               );

            //var sb = new StringBuilder();
            var ep = new ExpressionPrinter3();
            ep.Visit(e);
            Console.WriteLine(ep);

            var cal = new ExpressionCalculator();
            cal.Visit(e);
            Console.WriteLine($"{ep} = {cal.Result}");
        }
    }

    public interface IExpressionVisitor
    {
        // implement for every subtypes?
        void Visit(DoubleExpression3 de);
        void Visit(AdditionExpression3 ae);
    }

    public abstract class Expression3
    {
        public abstract void Accept(IExpressionVisitor visitor);
    }

    public class DoubleExpression3 : Expression3
    {
        public double Value; // neeed to be a public
        public DoubleExpression3(double value)
        {
            Value = value;
        }
        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this); // double dispatch
        }
    }

    public class AdditionExpression3 : Expression3
    {
        public Expression3 Left, Right; // need to be public

        public AdditionExpression3(Expression3 left, Expression3 right)
        {
            Left = left ?? throw new ArgumentNullException(paramName: nameof(left));
            Right = right ?? throw new ArgumentNullException(paramName: nameof(right));

        }
        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class ExpressionPrinter3 : IExpressionVisitor
    {
        StringBuilder sb = new StringBuilder();

        public void Visit(DoubleExpression3 de)
        {
            sb.Append(de.Value);
        }

        public void Visit(AdditionExpression3 ae)
        {
            sb.Append("(");
            ae.Left.Accept(this);
            sb.Append("+");
            ae.Right.Accept(this);
            sb.Append(")");
        }

        public override string ToString()
        {
            return sb.ToString();
        }
    }

    public class ExpressionCalculator : IExpressionVisitor
    {
        public double Result;
        public void Visit(DoubleExpression3 de)
        {
            Result = de.Value;
        }

        public void Visit(AdditionExpression3 ae)
        {
            ae.Left.Accept(this);
            var a = Result;
            ae.Right.Accept(this);
            var b = Result;
            Result = a + b;
        }
    }
}
