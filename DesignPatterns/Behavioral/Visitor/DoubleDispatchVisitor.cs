using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Behavioral.Visitor
{
    public class DoubleDispatchVisitor
    {
        public void Run()
        {
            var simple = new AdditionExpression7(new Value7(2), new Value7(3));
            var ep = new ExpressionPrinter7();
            ep.Visit(simple);
            Console.WriteLine(ep.ToString());

            var expr = new MultiplicationExpression7(
                new AdditionExpression7(new Value7(2), new Value7(3)),
                new Value7(4)
            );
            var ep7 = new ExpressionPrinter7();
            ep7.Visit(expr);
            Console.WriteLine(ep7.ToString());

        }
    }

    public abstract class ExpressionVisitor7
    {
        public abstract void Visit(Value7 value);
        public abstract void Visit(AdditionExpression7 ae);
        public abstract void Visit(MultiplicationExpression7 me);
    }

    public abstract class Expression7
    {
        public abstract void Accept(ExpressionVisitor7 ev);
    }

    public class Value7 : Expression7
    {
        public readonly int TheValue;

        public Value7(int value)
        {
            TheValue = value;
        }

        public override void Accept(ExpressionVisitor7 ev)
        {
            ev.Visit(this);
        }
    }

    public class AdditionExpression7 : Expression7
    {
        public readonly Expression7 LHS, RHS;

        public AdditionExpression7(Expression7 lhs, Expression7 rhs)
        {
            LHS = lhs;
            RHS = rhs;
        }

        public override void Accept(ExpressionVisitor7 ev)
        {
            ev.Visit(this);
        }
    }

    public class MultiplicationExpression7 : Expression7
    {
        public readonly Expression7 LHS, RHS;

        public MultiplicationExpression7(Expression7 lhs, Expression7 rhs)
        {
            LHS = lhs;
            RHS = rhs;
        }

        public override void Accept(ExpressionVisitor7 ev)
        {
            ev.Visit(this);
        }
    }

    public class ExpressionPrinter7 : ExpressionVisitor7
    {
        private StringBuilder sb = new StringBuilder();

        public override void Visit(Value7 value)
        {
            sb.Append(value.TheValue);
        }

        public override void Visit(AdditionExpression7 ae)
        {
            sb.Append("(");
            ae.LHS.Accept(this);
            sb.Append("+");
            ae.RHS.Accept(this);
            sb.Append(")");
        }

        public override void Visit(MultiplicationExpression7 me)
        {
            me.LHS.Accept(this);
            sb.Append("*");
            me.RHS.Accept(this);
        }

        public override string ToString()
        {
            return sb.ToString();
        }
    }
}
