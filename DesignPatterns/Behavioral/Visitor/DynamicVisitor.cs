using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Behavioral.Visitor
{
    public class DynamicVisitor
    {
        public void Run()
        {
            Expression4 e = new AdditionExpression4(
               new DoubleExpression4(4),
               new AdditionExpression4(
                       new DoubleExpression4(5),
                       new DoubleExpression4(6)
                   )
               );

            var sb = new StringBuilder();
            var ep = new ExpressionPrinter4a();
            //ep.Visit(e);
            ep.Print((dynamic)e, sb);
            Console.WriteLine(sb);

            //var cal = new ExpressionCalculator();
            //cal.Visit(e);
            //Console.WriteLine($"{ep} = {cal.Result}");
        }
    }


    public interface IExpressionVisitor4
    {
        // implement for every subtypes?
        void Visit(DoubleExpression4 de);
        void Visit(AdditionExpression4 ae);
    }

    public abstract class Expression4
    {
        public abstract void Accept(IExpressionVisitor4 visitor);
    }

    public class DoubleExpression4 : Expression4
    {
        public double Value; // neeed to be a public
        public DoubleExpression4(double value)
        {
            Value = value;
        }
        public override void Accept(IExpressionVisitor4 visitor)
        {
            visitor.Visit(this); // double dispatch
        }
    }

    public class AdditionExpression4 : Expression4
    {
        public Expression4 Left, Right; // need to be public

        public AdditionExpression4(Expression4 left, Expression4 right)
        {
            Left = left ?? throw new ArgumentNullException(paramName: nameof(left));
            Right = right ?? throw new ArgumentNullException(paramName: nameof(right));

        }
        public override void Accept(IExpressionVisitor4 visitor)
        {
            visitor.Visit(this);
        }
    }

    public class ExpressionPrinter4 : IExpressionVisitor4
    {
        StringBuilder sb = new StringBuilder();

        public void Visit(DoubleExpression4 de)
        {
            sb.Append(de.Value);
        }

        public void Visit(AdditionExpression4 ae)
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

    public class ExpressionCalculato4 : IExpressionVisitor4
    {
        public double Result;
        public void Visit(DoubleExpression4 de)
        {
            Result = de.Value;
        }

        public void Visit(AdditionExpression4 ae)
        {
            ae.Left.Accept(this);
            var a = Result;
            ae.Right.Accept(this);
            var b = Result;
            Result = a + b;
        }
    }


    public class ExpressionPrinter4a
    {
        public void Print(AdditionExpression4 ae, StringBuilder sb)
        {
            sb.Append("(");
            Print((dynamic)ae.Left, sb);
            sb.Append("+");
            Print((dynamic)ae.Right, sb);
            sb.Append(")");
            // dynamic cast -- find correct overload method
            // slow
            // runtime binary exception
        }

        public void Print(DoubleExpression4 de, StringBuilder sb)
        {
            sb.Append(de.Value);
        }
    }
}
