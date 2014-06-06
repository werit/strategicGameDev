using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;

namespace EnvironmentCreator
{
    public abstract class ConditionNode
    {
        public abstract GameStatData.NodeReturnType ReturnType();
        public virtual void evalNode(out bool returnVal)
        {
            throw new FunctionNotSupportedExcp(this.GetType().GetMethod(GameStatData.GetCurrentMethod()));
        }
        public virtual void evalNode(out int returnVal)
        {
            throw new FunctionNotSupportedExcp(this.GetType().GetMethod(GameStatData.GetCurrentMethod()));
        }
        //public virtual void accept(PreconditionVisitor vis);
    }
    public class IntNode : ConditionNode
    {
        protected int m_value;
        public override GameStatData.NodeReturnType ReturnType()
        {
            return GameStatData.NodeReturnType.INT;
        }
        public override void evalNode(out int returnVal)
        {
            returnVal = m_value;
        }
        public void SetValue(int value)
        {
            this.m_value = value;
        }
        /*public override void accept(PreconditionVisitor vis)
        {
            vis.Visit(this);
        }*/

    }
    public abstract class BinaryOp : ConditionNode
    {
        protected ConditionNode m_lNode;
        protected ConditionNode m_rNode;

        public virtual void SetNodes(ConditionNode lNode, ConditionNode rNode)
        {
            this.m_lNode = lNode;
            this.m_rNode = rNode;
        }
    }
    public abstract class BinaryMathOp : BinaryOp
    {
        public override GameStatData.NodeReturnType ReturnType()
        {
            return GameStatData.NodeReturnType.INT;
        }
    }
    public class PlusNode : BinaryMathOp
    {
        public override void evalNode(out int returnVal)
        {
            Debug.Assert(m_lNode != null && m_rNode != null &&
                m_lNode.ReturnType() == GameStatData.NodeReturnType.INT && m_rNode.ReturnType() == GameStatData.NodeReturnType.INT);
            int lRetVal;
            int rRetVal;
            m_lNode.evalNode(out lRetVal);
            m_rNode.evalNode(out rRetVal);
            returnVal = lRetVal + rRetVal;
        }

    }
    public class MinusNode : BinaryMathOp
    {
        public override void evalNode(out int returnVal)
        {
            Debug.Assert(m_lNode != null && m_rNode != null &&
                m_lNode.ReturnType() == GameStatData.NodeReturnType.INT && m_rNode.ReturnType() == GameStatData.NodeReturnType.INT);
            int lRetVal;
            int rRetVal;
            m_lNode.evalNode(out lRetVal);
            m_rNode.evalNode(out rRetVal);
            returnVal = lRetVal - rRetVal;
        }
    }
    public class MulNode : BinaryMathOp
    {

        public override void evalNode(out int returnVal)
        {
            Debug.Assert(m_lNode != null && m_rNode != null &&
                m_lNode.ReturnType() == GameStatData.NodeReturnType.INT && m_rNode.ReturnType() == GameStatData.NodeReturnType.INT);
            int lRetVal;
            int rRetVal;
            m_lNode.evalNode(out lRetVal);
            m_rNode.evalNode(out rRetVal);
            returnVal = lRetVal * rRetVal;
        }
    }
    public class DivNode : BinaryMathOp
    {
        public override void evalNode(out int returnVal)
        {
            Debug.Assert(m_lNode != null && m_rNode != null &&
                m_lNode.ReturnType() == GameStatData.NodeReturnType.INT && m_rNode.ReturnType() == GameStatData.NodeReturnType.INT);
            int lRetVal;
            int rRetVal;
            m_lNode.evalNode(out lRetVal);
            m_rNode.evalNode(out rRetVal);
            returnVal = lRetVal / rRetVal;
        }
    }
    public class ModNode : BinaryMathOp
    {
        public override void evalNode(out int returnVal)
        {
            Debug.Assert(m_lNode != null && m_rNode != null &&
                m_lNode.ReturnType() == GameStatData.NodeReturnType.INT && m_rNode.ReturnType() == GameStatData.NodeReturnType.INT);
            int lRetVal;
            int rRetVal;
            m_lNode.evalNode(out lRetVal);
            m_rNode.evalNode(out rRetVal);
            returnVal = lRetVal % rRetVal;
        }
    }
    public class IDNode : ConditionNode
    {
        protected string m_id;
        public override GameStatData.NodeReturnType ReturnType()
        {

            int outparam;
            if (GroundingParams.m_InstanceIntegerVar.TryGetValue(m_id, out outparam))
                return GameStatData.NodeReturnType.INT;
            else
            {
                bool outParamBool;
                if (GroundingParams.m_InstanceBoolVar.TryGetValue(m_id, out outParamBool))
                    return GameStatData.NodeReturnType.BOOL;
            }
            throw new UnknownParameterExc(m_id);
        }
        public void SetId(string id)
        {
            this.m_id = id;
        }
    }
    public abstract class BinaryCompareOp : BinaryOp
    {
        public override GameStatData.NodeReturnType ReturnType()
        {
            return GameStatData.NodeReturnType.BOOL;
        }
    }
    public class EqualNode : BinaryCompareOp
    {
        public override void evalNode(out bool returnVal)
        {
            if (m_lNode.ReturnType() != m_rNode.ReturnType())
                throw new UnxpectedParamTypeExc(m_rNode.ReturnType(), m_lNode.ReturnType(), GameStatData.BoolOperators.EQUAL);
            switch (m_lNode.ReturnType())
            {
                case GameStatData.NodeReturnType.BOOL:
                    bool lRetVal;
                    bool rRetVal;
                    m_lNode.evalNode(out lRetVal);
                    m_rNode.evalNode(out rRetVal);
                    returnVal = lRetVal == rRetVal;
                    break;
                case GameStatData.NodeReturnType.INT:
                    int lRetVal_int;
                    int rRetVal_int;
                    m_lNode.evalNode(out lRetVal_int);
                    m_rNode.evalNode(out rRetVal_int);
                    returnVal = lRetVal_int == rRetVal_int;
                    break;
                default:
                    throw new UnxpectedParamTypeExc(m_lNode.ReturnType(), GameStatData.NodeReturnType.INT, GameStatData.BoolOperators.EQUAL);
            }

        }
    }
    public class ENotqualNode : BinaryCompareOp
    {
        public override void evalNode(out bool returnVal)
        {
            if (m_lNode.ReturnType() != m_rNode.ReturnType())
                throw new UnxpectedParamTypeExc(m_rNode.ReturnType(), m_lNode.ReturnType(),  GameStatData.BoolOperators.NOT_EQUAL);
            switch (m_lNode.ReturnType())
            {
                case GameStatData.NodeReturnType.BOOL:
                    bool lRetVal;
                    bool rRetVal;
                    m_lNode.evalNode(out lRetVal);
                    m_rNode.evalNode(out rRetVal);
                    returnVal = lRetVal != rRetVal;
                    break;
                case GameStatData.NodeReturnType.INT:
                    int lRetVal_int;
                    int rRetVal_int;
                    m_lNode.evalNode(out lRetVal_int);
                    m_rNode.evalNode(out rRetVal_int);
                    returnVal = lRetVal_int != rRetVal_int;
                    break;
                default:
                    throw new UnxpectedParamTypeExc(m_lNode.ReturnType(), GameStatData.NodeReturnType.INT, GameStatData.BoolOperators.NOT_EQUAL);
            }

        }
    }
    public class LessThenNode : BinaryCompareOp
    {
        public override void evalNode(out bool returnVal)
        {
            if (m_lNode.ReturnType() != m_rNode.ReturnType())
                throw new UnxpectedParamTypeExc(m_rNode.ReturnType(), m_lNode.ReturnType(), GameStatData.BoolOperators.LESS_THEN);
            switch (m_lNode.ReturnType())
            {
                case GameStatData.NodeReturnType.INT:
                    int lRetVal_int;
                    int rRetVal_int;
                    m_lNode.evalNode(out lRetVal_int);
                    m_rNode.evalNode(out rRetVal_int);
                    returnVal = lRetVal_int < rRetVal_int;
                    break;
                default:
                    throw new UnxpectedParamTypeExc(m_lNode.ReturnType(), GameStatData.NodeReturnType.INT, GameStatData.BoolOperators.LESS_THEN);
            }

        }
    }
    public class LessOrEQNode : BinaryCompareOp
    {
        public override void evalNode(out bool returnVal)
        {
            if (m_lNode.ReturnType() != m_rNode.ReturnType())
                throw new UnxpectedParamTypeExc(m_rNode.ReturnType(), m_lNode.ReturnType(), GameStatData.BoolOperators.LESS_OR_EQUAL);
            switch (m_lNode.ReturnType())
            {
                case GameStatData.NodeReturnType.INT:
                    int lRetVal_int;
                    int rRetVal_int;
                    m_lNode.evalNode(out lRetVal_int);
                    m_rNode.evalNode(out rRetVal_int);
                    returnVal = lRetVal_int <= rRetVal_int;
                    break;
                default:
                    throw new UnxpectedParamTypeExc(m_lNode.ReturnType(), GameStatData.NodeReturnType.INT, GameStatData.BoolOperators.LESS_OR_EQUAL);
            }

        }
    }
    public class MoreThenNode : BinaryCompareOp
    {
        public override void evalNode(out bool returnVal)
        {
            if (m_lNode.ReturnType() != m_rNode.ReturnType())
                throw new UnxpectedParamTypeExc(m_rNode.ReturnType(), m_lNode.ReturnType(), GameStatData.BoolOperators.MORE_THEN);
            switch (m_lNode.ReturnType())
            {
                case GameStatData.NodeReturnType.INT:
                    int lRetVal_int;
                    int rRetVal_int;
                    m_lNode.evalNode(out lRetVal_int);
                    m_rNode.evalNode(out rRetVal_int);
                    returnVal = lRetVal_int > rRetVal_int;
                    break;
                default:
                    throw new UnxpectedParamTypeExc(m_lNode.ReturnType(), GameStatData.NodeReturnType.INT, GameStatData.BoolOperators.MORE_THEN);
            }

        }
    }
    public class MoreOrEQNode : BinaryCompareOp
    {
        public override void evalNode(out bool returnVal)
        {
            if (m_lNode.ReturnType() != m_rNode.ReturnType())
                throw new UnxpectedParamTypeExc(m_rNode.ReturnType(), m_lNode.ReturnType(), GameStatData.BoolOperators.MORE_OR_EQUAL);
            switch (m_lNode.ReturnType())
            {
                case GameStatData.NodeReturnType.INT:
                    int lRetVal_int;
                    int rRetVal_int;
                    m_lNode.evalNode(out lRetVal_int);
                    m_rNode.evalNode(out rRetVal_int);
                    returnVal = lRetVal_int >= rRetVal_int;
                    break;
                default:
                    throw new UnxpectedParamTypeExc(m_lNode.ReturnType(), GameStatData.NodeReturnType.INT, GameStatData.BoolOperators.MORE_OR_EQUAL);
            }

        }
    }
}
