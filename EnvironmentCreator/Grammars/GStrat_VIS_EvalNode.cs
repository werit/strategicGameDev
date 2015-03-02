using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvironmentCreator.Gammars
{
    /// <summary>
    /// Visitor class of grammar.
    /// Class process all grammar expresions that are in form of <see cref="EvaluationNode"/>.
    /// These grammar expresion consist 
    /// <list type="bullet">
    /// <item>equations</item>
    /// <item>assigns</item>
    /// <item>comparisions</item>
    /// </list>
    /// </summary>
    public class GStrat_VIS_EvalNode : GStratBaseVisitor<EvaluationNode>
    {
        /// <summary>
        /// Method processing visiting of end effect of action.
        /// Start and end effects are the same, therefore this method pass processing to method which handles effect.\n
        /// <see cref="GStrat_VIS_EvalNode.VisitAssignExpr"/> or <see cref="GStrat_VIS_EvalNode.VisistCallFn"/>.
        /// </summary>
        /// <param name="context">Parameter of parser context of currently processed tree part.</param>
        /// <returns>Node which represents one end effect.</returns>
        public override EvaluationNode VisitEffecte(GStratParser.EffecteContext context)
        {
            return this.Visit(context.effect());
        }
        /// <summary>
        /// Method processing visiting of assign expresion of action.
        /// Left side consist of node whom will be assigned value and right contain assigned value.
        /// </summary>
        /// <param name="context">Parameter of parser context of currently processed tree part.</param>
        /// <returns>Node which represents one assign expresion.</returns>
        public override EvaluationNode VisitAssignExpr(GStratParser.AssignExprContext context)
        {
            EvaluationNode lNode = GameStatData.m_assignNode_VIS.Visit(context.id());
            EvaluationNode rNode = GameStatData.m_assignNode_VIS.Visit(context.expression());
            NewAssign thisNode;
            switch (context.opt.Type)
            {
                case GStratParser.ASSIGN:
                    thisNode = new Assign();
                    break;
                case GStratParser.ASSIGN_MINUS:
                    thisNode = new SubstractAssign();
                    break;
                case GStratParser.ASSIGN_DIV:
                    thisNode = new DivisionAssign();
                    break;
                case GStratParser.ASSIGN_MUL:
                    thisNode = new MultiplAssign();
                    break;
                case GStratParser.ASSIGN_MOD:
                    thisNode = new ModAssign();
                    break;
                case GStratParser.ASSIGN_ADD:
                    thisNode = new AddAssign();
                    break;
                default:
                    throw new UnexpectedParserToken("assign expresion");
            }
            thisNode.SetNodes(lNode, rNode);
            return thisNode;
        }
        /// <summary>
        /// Method processing visiting of precondition function of action.
        /// Method returns node which represents function, which after evaluation returns boolean.
        /// </summary>
        /// <param name="context">Parameter of parser context of currently processed tree part.</param>
        /// <returns>Node which represents one precondition function call.</returns>
        public override EvaluationNode VisitCallFnPrecond(GStratParser.CallFnPrecondContext context)
        {
            return base.VisitCallFnPrecond(context);
        }
        /// <summary>
        /// Method process visiting addition or subtraction in expression of action.
        /// </summary>
        /// <param name="context">Parameter of parser context of currently processed tree part.</param>
        /// <returns>Node which represents addition or subtraction of two <see cref="EvaluationNode"/>.</returns>
        public override EvaluationNode VisitAddSub(GStratParser.AddSubContext context)
        {
            EvaluationNode lNode = Visit(context.expression(0));
            EvaluationNode rNode = Visit(context.expression(1));
            BinaryMathOp thisNode;
            if (context.opt.Type == GStratParser.ADD)
                thisNode = new PlusNode();
            else
                thisNode = new MinusNode();
            thisNode.SetNodes(lNode, rNode);
            return thisNode;
        }
        /// <summary>
        /// Method process visiting multiplication, division or modulation in expression of action.
        /// </summary>
        /// <param name="context">Parameter of parser context of currently processed tree part.</param>
        /// <returns>Node which represents multiplication, division or modulation of two <see cref="EvaluationNode"/>.</returns>
        public override EvaluationNode VisitMulDivMod(GStratParser.MulDivModContext context)
        {
            EvaluationNode lNode = Visit(context.expression(0));
            EvaluationNode rNode = Visit(context.expression(1));
            BinaryMathOp thisNode;
            switch (context.opt.Type)
            {
                case GStratParser.MUL:
                    thisNode = new MulNode();
                    break;
                case GStratParser.MOD:
                    thisNode = new ModNode();
                    break;
                case GStratParser.DIV:
                    thisNode = new DivNode();
                    break;
                default:
                    throw new UnexpectedParserToken("MulDivMod expresion");
            }
            thisNode.SetNodes(lNode, rNode);
            return thisNode;
        }
        /// <summary>
        /// Method creating Identification evaluation node.\n
        /// Method return two types of identifier. One is just containing variable. Second identifier takes name of instance of class and parameter of this class.\n
        /// For example function header defined for function 'fn', class 'Soldier' with variable 'bulletCount' : fn(Soldier shooter)
        ///
        /// identifier referencing to bulletCount of shooter will look like shooter.bulletCount (during runetime for shooter will be assigned real instance).
        /// </summary>
        /// <param name="context">Parameter of parser context of currently processed tree part.</param>
        /// <returns>Method returns <see cref="IDNode"/> with stored identifier.</returns>
        public override EvaluationNode VisitId(GStratParser.IdContext context)
        {
            //if (context.NAME().Count > 1)
            //    return new IDNode(context.NAME(0).GetText(), context.NAME(1).GetText());
            //else
            //    return new IDNode(context.NAME(0).GetText());
            return new IDNode(context.NAME(0).GetText(), context.NAME(1).GetText());
        }
        /// <summary>
        /// Method process visiting 'ident' rule of grammar.\n
        /// This rule is processed as identifier, therefore method <see cref="GStrat_VIS_EvalNode.VisitId"/> is called.
        /// </summary>
        /// <param name="context">Parameter of parser context of currently processed tree part.</param>
        /// <returns>Method returns <see cref="IDNode"/> with stored identifier.</returns>
        public override EvaluationNode VisitIdent(GStratParser.IdentContext context)
        {
            return Visit(context.id());
        }
        /// <summary>
        /// Method process visiting 'PrecondExpr' rule of grammar. It is <see cref="Action"/> rule.
        /// </summary>
        /// <param name="context">Parameter of parser context of currently processed tree part.</param>
        /// <returns>Method returns <see cref="BinaryCompareOp"/> containing compare expresion and comparator.</returns>
        public override EvaluationNode VisitPrecondExpr(GStratParser.PrecondExprContext context)
        {
            EvaluationNode lNode = Visit(context.id());
            EvaluationNode rNode = Visit(context.expression());
            BinaryCompareOp thisNode;
            switch (context.op.Type)
            {
                case GStratParser.EQUAL:
                    thisNode = new EqualNode();
                    break;
                case GStratParser.NOT_EQUAL:
                    thisNode = new NotEqualNode();
                    break;
                case GStratParser.LESS_THEN:
                    thisNode = new LessThenNode();
                    break;
                case GStratParser.LESS__OR_EQ:
                    thisNode = new LessOrEQNode();
                    break;
                case GStratParser.MORE_THEN:
                    thisNode = new MoreThenNode();
                    break;
                case GStratParser.MORE_OR_EQ:
                    thisNode = new MoreOrEQNode();
                    break;
                default:
                    throw new UnexpectedParserToken("precondition expresion");
            }
            thisNode.SetNodes(lNode, rNode);
            return thisNode;
        }
        /// <summary>
        /// Method processing grammar rule matching integer.
        /// </summary>
        /// <param name="context">Parameter of parser context of currently processed tree part.</param>
        /// <returns>Method returns <see cref="IntNode"/> with stored integer value.</returns>
        public override EvaluationNode VisitInt(GStratParser.IntContext context)
        {
            return new IntNode(int.Parse(context.INT().GetText()));
        }
        /// <summary>
        /// Method processing grammar rule matching parenthesis.
        /// </summary>
        /// <param name="context">Parameter of parser context of currently processed tree part.</param>
        /// <returns>Method returns internal expresion in form of tree consisting of <see cref="EvaluationNode"/>.</returns>
        public override EvaluationNode VisitParenth(GStratParser.ParenthContext context)
        {
            return Visit(context.expression());
        }
    }
}
