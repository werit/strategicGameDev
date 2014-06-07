using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace EnvironmentCreator.Gammars
{
    class GStrat_VIS_int : GStratBaseVisitor<int>
    {
        public override int VisitAddSub(GStratParser.AddSubContext context)
        {
            int left = Visit(context.expression(0));
            int right = Visit(context.expression(1));

            if (context.opt.Type == GStratParser.ADD)
                return left + right;
            else
                return left - right;
        }
        public override int VisitMulDivMod(GStratParser.MulDivModContext context)
        {
            int left = Visit(context.expression(0));
            int right = Visit(context.expression(1));

            if (context.opt.Type == GStratParser.MUL)
                return left * right;
            else
                if (context.opt.Type == GStratParser.DIV)
                    return left / right;
                else
                    return left % right;
        }
        public override int VisitParenth(GStratParser.ParenthContext context)
        {
            return Visit(context.expression());
        }
        public override int VisitInt(GStratParser.IntContext context)
        {
            return int.Parse(context.INT().GetText());
        }
    }
}
