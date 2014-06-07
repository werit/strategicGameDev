using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvironmentCreator.Gammars
{
    class CalculatorVisitor : CalculatorBaseVisitor<int>
    {
        private int count;
        public CalculatorVisitor()
        {
            count = 0;
        }

        public override int VisitInt(CalculatorParser.IntContext context)
        {
            ++count;
            return int.Parse(context.INT().GetText());
        }
        public override int VisitAddSub(CalculatorParser.AddSubContext context)
        {
            System.Console.Out.WriteLine("Som v VisitAddSub vo Visitore"); 
            int left = Visit(context.expr(0));
            int right = Visit(context.expr(1));
            if (context.op.Type == CalculatorParser.ADD)
            {
                return left + right;
            }
            else
            {
                return left - right;
            }
        }
        public override int VisitMulDiv(CalculatorParser.MulDivContext context)
        {
            int left = Visit(context.expr(0));
            int right = Visit(context.expr(1));
            if (context.op.Type == CalculatorParser.MUL)
            {
                return left * right;
            }
            else
            {
                return left / right;
            }
        }

        public override int VisitParens(CalculatorParser.ParensContext context)
        {
            return Visit(context.expr());
        }
        public int getCount()
        {
            return this.count;
        }
    }
}
