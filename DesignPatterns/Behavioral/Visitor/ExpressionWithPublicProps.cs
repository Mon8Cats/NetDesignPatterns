using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Behavioral.Visitor
{

    using DictType = Dictionary<Type, Action<Expression2, StringBuilder>>;
    public class ExpressionWithPublicProps
    {
        public void Run()
        {
            var e = new AdditionExpression2(
               new DoubleExpression2(1),
               new AdditionExpression2(
                       new DoubleExpression2(2),
                       new DoubleExpression2(3)
                   )
               );

            var sb = new StringBuilder();
            ExpressionPrinter.Print(e, sb);
            Console.WriteLine(sb);
        }
    }

    public abstract class Expression2
    {
        // No print method
        //public abstract void Print(StringBuilder sb);
    }

    public class DoubleExpression2 : Expression2
    {
        public double Value; // neeed to be a public
        public DoubleExpression2(double value)
        {
            Value = value;
        }
        //public override void Print(StringBuilder sb)
        //{
        //    sb.Append(_value);
        //}
    }

    public class AdditionExpression2 : Expression2
    {
        public Expression2 Left, Right; // need to be public

        public AdditionExpression2(Expression2 left, Expression2 right)
        {
            Left = left ?? throw new ArgumentNullException(paramName: nameof(left));
            Right = right ?? throw new ArgumentNullException(paramName: nameof(right));

        }
        //public override void Print(StringBuilder sb)
        //{
        //    sb.Append("(");
        //    _left.Print(sb);
        //    sb.Append("+");
        //    _right.Print(sb);
        //    sb.Append(")");
        //}
    }

    // Create a new class
    public static class ExpressionPrinter
    {
        public static void Print(Expression2 e, StringBuilder sb)
        {
            if(e is DoubleExpression2 de)
            {
                sb.Append(de.Value);
            } else if(e is AdditionExpression2 ae)
            {
                sb.Append("(");
                Print(ae.Left, sb);
                Print(ae.Right, sb);
                sb.Append(")");
            }
        }
    }

    public static class ExpressionPrinter2
    {
        // no switch but you should add sub types here!
        private static DictType actions = new DictType
        {
            [typeof(DoubleExpression2)] = (e, sb) =>
            {
                var de = (DoubleExpression2)e;
                sb.Append(de.Value);
            },
            [typeof(AdditionExpression2)] = (e, sb) =>
            {
                var ae = (AdditionExpression2)e;
                sb.Append("(");
                Print(ae.Left, sb);
                Print(ae.Right, sb);
                sb.Append(")");
            }
        };

        public static void Print(Expression2 e, StringBuilder sb)
        {
            actions[e.GetType()](e, sb);
        }
    }
}
