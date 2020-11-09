using DesignPatterns.Behavioral.Visitor;
using System;

namespace DesignPatterns
{
    class Program
    {
        static void Main(string[] args)
        {
            var obj = new ExpressionWithPublicProps();
            obj.Run();
        }
    }
}
