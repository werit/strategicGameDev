using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvironmentCreator.Gammars
{
    /*class GStrat_VIS_bool : GStratBaseVisitor<bool>
    {
        public override bool VisitTruth(GStratParser.TruthContext context)
        {
            return true;
        }
        public override bool VisitFalsee(GStratParser.FalseeContext context)
        {
            return false;
        }
        public override bool VisitBoolParenth(GStratParser.BoolParenthContext context)
        {
            return Visit(context.precondition());
        }
        public override bool VisitAndOr(GStratParser.AndOrContext context)
        {
            bool left = Visit(context.precondition(0));
            bool right = Visit(context.precondition(1));
            if (context.op.Type == GStratParser.AND)
                return left && right;
            else
                return left || right;
        }
    }*/
}
