using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;

namespace EnvironmentCreator
{
    public abstract class EvaluationNode
    {
        public abstract GameStatData.NodeReturnType ReturnType();
        public bool isReturnTypeOfBothBool(EvaluationNode node1, EvaluationNode node2)
        {
            return (node1.ReturnType() == GameStatData.NodeReturnType.BOOL || node1.ReturnType() == GameStatData.NodeReturnType.INT_BOOL) && isReturnTypeSame(node1, node2);
        }
        public bool isReturnTypeOfBothInt(EvaluationNode node1, EvaluationNode node2)
        {
            return (node1.ReturnType() == GameStatData.NodeReturnType.INT || node1.ReturnType() == GameStatData.NodeReturnType.INT_BOOL) && isReturnTypeSame(node1, node2);
        }
        public bool isReturnTypeSame(EvaluationNode node1, EvaluationNode node2)
        {
            return node1.ReturnType() == node2.ReturnType() ||
                ((node1.ReturnType() == GameStatData.NodeReturnType.INT_BOOL && (node2.ReturnType() == GameStatData.NodeReturnType.INT || node2.ReturnType() == GameStatData.NodeReturnType.BOOL)) ||
                (node2.ReturnType() == GameStatData.NodeReturnType.INT_BOOL && (node1.ReturnType() == GameStatData.NodeReturnType.INT || node1.ReturnType() == GameStatData.NodeReturnType.BOOL)));
        }
        public virtual void evalNode(out bool returnVal)
        {
            throw new FunctionNotSupportedExcp(this.GetType().GetMethod(GameStatData.GetCurrentMethod()));
        }
        public virtual void evalNode(out int returnVal)
        {
            throw new FunctionNotSupportedExcp(this.GetType().GetMethod(GameStatData.GetCurrentMethod()));
        }
        public virtual void evalNode(){
            throw new FunctionNotSupportedExcp(this.GetType().GetMethod(GameStatData.GetCurrentMethod()));
        }
    }
    public class IntNode : EvaluationNode
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
        public IntNode() { }
        public IntNode(int value)
        {
            m_value = value;
        }

    }
    public abstract class BinaryOp : EvaluationNode
    {
        protected EvaluationNode m_lNode;
        protected EvaluationNode m_rNode;

        public virtual void SetNodes(EvaluationNode lNode, EvaluationNode rNode)
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
                isReturnTypeOfBothInt(m_lNode, m_rNode));
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
                isReturnTypeOfBothInt(m_lNode, m_rNode));
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
                isReturnTypeOfBothInt(m_lNode, m_rNode));
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
                isReturnTypeOfBothInt(m_lNode, m_rNode));
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
                isReturnTypeOfBothInt(m_lNode,m_rNode));
            int lRetVal;
            int rRetVal;
            m_lNode.evalNode(out lRetVal);
            m_rNode.evalNode(out rRetVal);
            returnVal = lRetVal % rRetVal;
        }
    }
    public class IDNode : EvaluationNode
    {
        // parameter identifier after dot
        protected string m_id;
        // instance identifier
        protected string m_instance_id;
        public override GameStatData.NodeReturnType ReturnType()
        {
            if (GroundingParams.m_InstanceBoolVar.ContainsKey(m_id) && GroundingParams.m_InstanceIntegerVar.ContainsKey(m_id))
                return GameStatData.NodeReturnType.INT_BOOL;
            else
                if (GroundingParams.m_InstanceIntegerVar.ContainsKey(m_id))
                    return GameStatData.NodeReturnType.INT;
                else
                {
                    if (GroundingParams.m_InstanceBoolVar.ContainsKey(m_id))
                        return GameStatData.NodeReturnType.BOOL;
                }
            throw new UnknownParameterExc(m_id);
        }
        /*
         * Method sets parameter identifier
         */
        public void SetId(string id)
        {
            if(id != null)
                this.m_id = id;
        }

        public IDNode() { }
        public IDNode(string instance, string param)
        {
            this.m_id = param;
            this.m_instance_id = instance;
        }
        public IDNode(string parameter){
            this.m_id = parameter;
        }
        public override string ToString()
        {
            if (m_instance_id != null)
                return this.m_instance_id + '.' + this.m_id;
            else
                return this.m_id;
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
            if (!isReturnTypeSame(m_lNode,m_rNode))
                throw new UnxpectedParamTypeExc(m_rNode.ReturnType(), m_lNode.ReturnType(), GameStatData.BoolOperators.EQUAL);
            bool lRetVal;
            bool rRetVal;
            int lRetVal_int;
            int rRetVal_int;
            switch (m_lNode.ReturnType())
            {
                case GameStatData.NodeReturnType.BOOL:
                    m_lNode.evalNode(out lRetVal);
                    m_rNode.evalNode(out rRetVal);
                    returnVal = lRetVal == rRetVal;
                    break;
                case GameStatData.NodeReturnType.INT:
                    m_lNode.evalNode(out lRetVal_int);
                    m_rNode.evalNode(out rRetVal_int);
                    returnVal = lRetVal_int == rRetVal_int;
                    break;
                case GameStatData.NodeReturnType.INT_BOOL:
                    if (m_rNode.ReturnType() == GameStatData.NodeReturnType.INT)
                    {
                        m_lNode.evalNode(out lRetVal_int);
                        m_rNode.evalNode(out rRetVal_int);
                        returnVal = lRetVal_int == rRetVal_int;
                    }
                    else
                        if (m_rNode.ReturnType() == GameStatData.NodeReturnType.BOOL)
                        {
                            m_lNode.evalNode(out lRetVal);
                            m_rNode.evalNode(out rRetVal);
                            returnVal = lRetVal == rRetVal;
                        }
                        else
                            throw new CannotChooseVariableTypeExc(m_lNode.ToString());
                    break;
                default:
                    throw new UnxpectedParamTypeExc(m_lNode.ReturnType(), GameStatData.NodeReturnType.INT, GameStatData.BoolOperators.EQUAL);
            }

        }
    }
    public class NotEqualNode : BinaryCompareOp
    {
        public override void evalNode(out bool returnVal)
        {
            if (!isReturnTypeSame(m_lNode, m_rNode))
                throw new UnxpectedParamTypeExc(m_rNode.ReturnType(), m_lNode.ReturnType(),  GameStatData.BoolOperators.NOT_EQUAL);
            bool lRetVal;
            bool rRetVal;
            int lRetVal_int;
            int rRetVal_int;
            switch (m_lNode.ReturnType())
            {
                case GameStatData.NodeReturnType.BOOL:
                    m_lNode.evalNode(out lRetVal);
                    m_rNode.evalNode(out rRetVal);
                    returnVal = lRetVal != rRetVal;
                    break;
                case GameStatData.NodeReturnType.INT:
                    m_lNode.evalNode(out lRetVal_int);
                    m_rNode.evalNode(out rRetVal_int);
                    returnVal = lRetVal_int != rRetVal_int;
                    break;
                case GameStatData.NodeReturnType.INT_BOOL:
                    if (m_rNode.ReturnType() == GameStatData.NodeReturnType.INT)
                    {
                        m_lNode.evalNode(out lRetVal_int);
                        m_rNode.evalNode(out rRetVal_int);
                        returnVal = lRetVal_int != rRetVal_int;
                    }
                    else
                        if (m_rNode.ReturnType() == GameStatData.NodeReturnType.BOOL)
                        {
                            m_lNode.evalNode(out lRetVal);
                            m_rNode.evalNode(out rRetVal);
                            returnVal = lRetVal != rRetVal;
                        }
                        else
                            throw new CannotChooseVariableTypeExc(m_lNode.ToString());
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
            if (!isReturnTypeOfBothInt(m_lNode, m_rNode))
                throw new UnxpectedParamTypeExc(m_rNode.ReturnType(), m_lNode.ReturnType(), GameStatData.BoolOperators.LESS_THEN);
            int lRetVal_int;
            int rRetVal_int;
            m_lNode.evalNode(out lRetVal_int);
            m_rNode.evalNode(out rRetVal_int);
            returnVal = lRetVal_int < rRetVal_int;
        }
    }
    public class LessOrEQNode : BinaryCompareOp
    {
        public override void evalNode(out bool returnVal)
        {
            if (!isReturnTypeOfBothInt(m_lNode, m_rNode))
                throw new UnxpectedParamTypeExc(m_rNode.ReturnType(), m_lNode.ReturnType(), GameStatData.BoolOperators.LESS_OR_EQUAL);
            int lRetVal_int;
            int rRetVal_int;
            m_lNode.evalNode(out lRetVal_int);
            m_rNode.evalNode(out rRetVal_int);
            returnVal = lRetVal_int <= rRetVal_int;

        }
    }
    public class MoreThenNode : BinaryCompareOp
    {
        public override void evalNode(out bool returnVal)
        {
            if (!isReturnTypeOfBothInt(m_lNode, m_rNode))
                throw new UnxpectedParamTypeExc(m_rNode.ReturnType(), m_lNode.ReturnType(), GameStatData.BoolOperators.MORE_THEN);
            int lRetVal_int;
            int rRetVal_int;
            m_lNode.evalNode(out lRetVal_int);
            m_rNode.evalNode(out rRetVal_int);
            returnVal = lRetVal_int > rRetVal_int;

        }
    }
    public class MoreOrEQNode : BinaryCompareOp
    {
        public override void evalNode(out bool returnVal)
        {
            if (!isReturnTypeOfBothInt(m_lNode, m_rNode))
                throw new UnxpectedParamTypeExc(m_rNode.ReturnType(), m_lNode.ReturnType(), GameStatData.BoolOperators.MORE_OR_EQUAL);
            int lRetVal_int;
            int rRetVal_int;
            m_lNode.evalNode(out lRetVal_int);
            m_rNode.evalNode(out rRetVal_int);
            returnVal = lRetVal_int >= rRetVal_int;

        }
    }
    public class Assign : BinaryOp
    {
        public override void evalNode()
        {
            if(!isReturnTypeSame(m_lNode,m_rNode))
                throw new UnxpectedParamTypeExc(m_rNode.ReturnType(), m_lNode.ReturnType(), GameStatData.AssignOperators.ASSIGN);
            bool rRetVal;
            int rRetVal_int;
            switch (m_lNode.ReturnType())
            {
                case GameStatData.NodeReturnType.BOOL:
                    m_rNode.evalNode(out rRetVal);
                    GroundingParams.m_InstanceBoolVar[m_lNode.ToString()] = rRetVal;
                    break;
                case GameStatData.NodeReturnType.INT:
                    m_rNode.evalNode(out rRetVal_int);
                    GroundingParams.m_InstanceIntegerVar[m_lNode.ToString()] = rRetVal_int;
                    break;
                case GameStatData.NodeReturnType.INT_BOOL:
                    if (m_rNode.ReturnType() == GameStatData.NodeReturnType.INT)
                    {
                        m_rNode.evalNode(out rRetVal_int);
                        GroundingParams.m_InstanceIntegerVar[m_lNode.ToString()] = rRetVal_int;
                    }
                    else
                        if (m_rNode.ReturnType() == GameStatData.NodeReturnType.BOOL)
                        {
                            m_rNode.evalNode(out rRetVal);
                            GroundingParams.m_InstanceBoolVar[m_lNode.ToString()] = rRetVal;
                        }
                        else
                            throw new CannotChooseVariableTypeExc(m_lNode.ToString());
                    break;
                default:
                    throw new UnxpectedParamTypeExc(m_lNode.ReturnType(), GameStatData.NodeReturnType.INT, GameStatData.AssignOperators.ASSIGN);
            }
        }
    }
    public class SubstractAssign : BinaryOp
    {
        public override void evalNode()
        {
            if(!isReturnTypeOfBothInt(m_lNode,m_rNode))
                throw new UnxpectedParamTypeExc(m_rNode.ReturnType(), m_lNode.ReturnType(), GameStatData.AssignOperators.SUBSTRACT_ASSIGN);
            int rRetVal_int;
            m_rNode.evalNode(out rRetVal_int);
            GroundingParams.m_InstanceIntegerVar[m_lNode.ToString()] -= rRetVal_int;
        }
    }
    public class AddAssign : BinaryOp
    {
        public override void evalNode()
        {
            if (!isReturnTypeOfBothInt(m_lNode, m_rNode))
                throw new UnxpectedParamTypeExc(m_rNode.ReturnType(), m_lNode.ReturnType(), GameStatData.AssignOperators.SUBSTRACT_ASSIGN);
            int rRetVal_int;
            m_rNode.evalNode(out rRetVal_int);
            GroundingParams.m_InstanceIntegerVar[m_lNode.ToString()] += rRetVal_int;
        }
    }
    public class DivisionAssign : BinaryOp
    {
        public override void evalNode()
        {
            if (!isReturnTypeOfBothInt(m_lNode, m_rNode))
                throw new UnxpectedParamTypeExc(m_rNode.ReturnType(), m_lNode.ReturnType(), GameStatData.AssignOperators.SUBSTRACT_ASSIGN);
            int rRetVal_int;
            m_rNode.evalNode(out rRetVal_int);
            GroundingParams.m_InstanceIntegerVar[m_lNode.ToString()] /= rRetVal_int;
        }
    }
    public class MultiplAssign : BinaryOp
    {
        public override void evalNode()
        {
            if (!isReturnTypeOfBothInt(m_lNode, m_rNode))
                throw new UnxpectedParamTypeExc(m_rNode.ReturnType(), m_lNode.ReturnType(), GameStatData.AssignOperators.SUBSTRACT_ASSIGN);
            int rRetVal_int;
            m_rNode.evalNode(out rRetVal_int);
            GroundingParams.m_InstanceIntegerVar[m_lNode.ToString()] *= rRetVal_int;
        }
    }
    public class ModAssign : BinaryOp
    {
        public override void evalNode()
        {
            if (!isReturnTypeOfBothInt(m_lNode, m_rNode))
                throw new UnxpectedParamTypeExc(m_rNode.ReturnType(), m_lNode.ReturnType(), GameStatData.AssignOperators.SUBSTRACT_ASSIGN);
            int rRetVal_int;
            m_rNode.evalNode(out rRetVal_int);
            GroundingParams.m_InstanceIntegerVar[m_lNode.ToString()] %= rRetVal_int;
        }
    }
}
