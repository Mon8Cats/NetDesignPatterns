using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Behavioral.Visitor
{
    public class AcyclicVisitor
    {
        public void Run()
        {
            var e = new AdditionExpression5(
                left: new DoubleExpression5(1),
                right: new AdditionExpression5(
                    left: new DoubleExpression5(2),
                    right: new DoubleExpression5(3)
                    ));
            var ep = new ExpressionPrinter5();
            ep.Visit(e);
            Console.WriteLine(ep);
        }
    }

    // generic interface?
    public interface IVisitor<TVisitable>
    {
        void Visit(TVisitable obj);
    }

    public interface IVisitor { } //marker interface, degenrate interface -- indicating some type

    public abstract class Expression5
    {
        public virtual void Accept(IVisitor visitor)
        {
            if (visitor is IVisitor<Expression5> typed)
                typed.Visit(this);
        }
    }

    public class DoubleExpression5 : Expression5
    {
        public double Value;

        public DoubleExpression5(double value)
        {
            Value = value;
        }

        public override void Accept(IVisitor visitor)
        {
            if (visitor is IVisitor<DoubleExpression5> typed)
                typed.Visit(this);
        }
    }

    public class AdditionExpression5 : Expression5
    {
        public Expression5 Left, Right;

        public AdditionExpression5(Expression5 left, Expression5 right)
        {
            Left = left ?? throw new ArgumentNullException(nameof(left));
            Right = right ?? throw new ArgumentNullException(nameof(right));
        }

        public override void Accept(IVisitor visitor)
        {
            if (visitor is IVisitor<AdditionExpression5> typed)
                typed.Visit(this);
        }
    }

    public class ExpressionPrinter5 : IVisitor, IVisitor<Expression5>, IVisitor<DoubleExpression5>, IVisitor<AdditionExpression5>
    {
        private StringBuilder sb = new StringBuilder();
        public void Visit(Expression5 obj)
        {
           
        }

        public void Visit(DoubleExpression5 obj)
        {
            sb.Append(obj.Value);
        }

        public void Visit(AdditionExpression5 obj)
        {
            sb.Append("(");
            obj.Left.Accept(this);
            sb.Append("+");
            obj.Right.Accept(this);
            sb.Append(")");
        }

        public override string ToString() => sb.ToString();

    }
}
