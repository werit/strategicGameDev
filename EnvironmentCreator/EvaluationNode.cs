using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;

namespace EnvironmentCreator
{
    /** @brief Main class representing all nodes used in evaluation tree of expresions.
     * 
     * 
     */
    public abstract class EvaluationNode
    {
        
        
        
        public virtual void evalNode(Dictionary<string, string> mapParam){
            throw new FunctionNotSupportedExcp(this.GetType().GetMethod(GameStatData.GetCurrentMethod()));
        }
        /**
         * Method says true if node is IdNode and has two part name.
         * Which means that name contains also instance name and name of parameter.
         * Like "target.HP", where target is speudo instance in action and HP is actual variable on some instance which corresponds to target.
         */
        public virtual bool isInstanceNode()
        {
            return false;
        }
        public virtual void evalNode(Dictionary<string, string> mapParam, out bool returnVal)
        {
            throw new FunctionNotSupportedExcp(this.GetType().GetMethod(GameStatData.GetCurrentMethod()));
        }
        public virtual void evalNode(Dictionary<string, string> mapParam, out int returnVal)
        {
            throw new FunctionNotSupportedExcp(this.GetType().GetMethod(GameStatData.GetCurrentMethod()));
        }
        public abstract GameStatData.NodeReturnType ReturnType(Dictionary<string, string> mapParam);
        public bool isReturnTypeOfBothBool(EvaluationNode node1, EvaluationNode node2, Dictionary<string, string> mapParam)
        {
            return (node1.ReturnType(mapParam) == GameStatData.NodeReturnType.BOOL || node1.ReturnType(mapParam) == GameStatData.NodeReturnType.INT_BOOL) && isReturnTypeSame(node1, node2, mapParam);
        }
        public bool isReturnTypeOfBothInt(EvaluationNode node1, EvaluationNode node2, Dictionary<string, string> mapParam)
        {
            return (node1.ReturnType(mapParam) == GameStatData.NodeReturnType.INT || node1.ReturnType(mapParam) == GameStatData.NodeReturnType.INT_BOOL) && isReturnTypeSame(node1, node2, mapParam);
        }
        public bool isReturnTypeSame(EvaluationNode node1, EvaluationNode node2, Dictionary<string, string> mapParam)
        {
            return node1.ReturnType(mapParam) == node2.ReturnType(mapParam) ||
                ((node1.ReturnType(mapParam) == GameStatData.NodeReturnType.INT_BOOL && (node2.ReturnType(mapParam) == GameStatData.NodeReturnType.INT || node2.ReturnType(mapParam) == GameStatData.NodeReturnType.BOOL)) ||
                (node2.ReturnType(mapParam) == GameStatData.NodeReturnType.INT_BOOL && (node1.ReturnType(mapParam) == GameStatData.NodeReturnType.INT || node1.ReturnType(mapParam) == GameStatData.NodeReturnType.BOOL)));
        }
    }



    public class IntNode : EvaluationNode
    {
        protected int m_value;
        public override GameStatData.NodeReturnType ReturnType(Dictionary<string, string> mapParam)
        {
            return GameStatData.NodeReturnType.INT;
        }
        public override void evalNode(Dictionary<string, string> mapParam,out int returnVal)
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
        public override GameStatData.NodeReturnType ReturnType(Dictionary<string, string> mapParam)
        {
            return GameStatData.NodeReturnType.INT;
        }
        protected int result(Dictionary<string, string> mapParam, GameStatData.AritmeticOperators op)
        {
            Debug.Assert(m_lNode != null && m_rNode != null &&
                isReturnTypeOfBothInt(m_lNode, m_rNode, mapParam));
            int lRetVal;
            int rRetVal;
            m_lNode.evalNode(mapParam, out lRetVal);
            m_rNode.evalNode(mapParam, out rRetVal);

            switch (op)
            {
                case GameStatData.AritmeticOperators.ADDITION:
                    return lRetVal + rRetVal;
                case GameStatData.AritmeticOperators.SUBSTRACTION:
                    return lRetVal - rRetVal;
                case GameStatData.AritmeticOperators.DIVISION:
                    return lRetVal / rRetVal;
                case GameStatData.AritmeticOperators.MODULATION:
                    return lRetVal % rRetVal;
                case GameStatData.AritmeticOperators.MULTIPLICATION:
                    return lRetVal * rRetVal;
                default: throw new Exception();// throw new exception ARITMATIC OPERATION NOT DEFINED.
            }
        }
    }
    public class PlusNode : BinaryMathOp
    {
        public override void evalNode(Dictionary<string, string> mapParam,out int returnVal)
        {
            returnVal = result(mapParam,GameStatData.AritmeticOperators.ADDITION);
        }

    }
    public class MinusNode : BinaryMathOp
    {
        public override void evalNode(Dictionary<string, string> mapParam,out int returnVal)
        {
            returnVal = result(mapParam,GameStatData.AritmeticOperators.SUBSTRACTION);
        }
    }
    public class MulNode : BinaryMathOp
    {

        public override void evalNode(Dictionary<string, string> mapParam,out int returnVal)
        {
            returnVal = result(mapParam, GameStatData.AritmeticOperators.MULTIPLICATION);
        }
    }
    public class DivNode : BinaryMathOp
    {
        public override void evalNode(Dictionary<string, string> mapParam,out int returnVal)
        {
            returnVal = result(mapParam,GameStatData.AritmeticOperators.DIVISION);
        }
    }
    public class ModNode : BinaryMathOp
    {
        public override void evalNode(Dictionary<string, string> mapParam,out int returnVal)
        {
            returnVal = result(mapParam,GameStatData.AritmeticOperators.MODULATION);
        }
    }
    public class IDNode : EvaluationNode
    {
        // parameter identifier after dot
        protected string m_id;
        // instance identifier
        protected string m_instance_id;
        public override GameStatData.NodeReturnType ReturnType(Dictionary<string, string> mapParam)
        {
            if(!mapParam.ContainsKey(this.m_instance_id)){
                // throw some error
            }
            // actual name of instance
            string translation = mapParam[this.m_instance_id];
            if (GroundingParams.m_InstanceBoolVar.ContainsKey(translation) && GroundingParams.m_InstanceBoolVar[translation].ContainsKey(m_id) 
                &&
                GroundingParams.m_InstanceIntegerVar.ContainsKey(translation) && GroundingParams.m_InstanceIntegerVar[translation].ContainsKey(m_id))
            //if (GroundingParams.m_InstanceBoolVar.ContainsKey(m_id) && GroundingParams.m_InstanceIntegerVar.ContainsKey(m_id))
                return GameStatData.NodeReturnType.INT_BOOL;
            else
                if (GroundingParams.m_InstanceIntegerVar.ContainsKey(translation) && GroundingParams.m_InstanceIntegerVar[translation].ContainsKey(m_id))
                    return GameStatData.NodeReturnType.INT;
                else
                {
                    if (GroundingParams.m_InstanceBoolVar.ContainsKey(translation) && GroundingParams.m_InstanceBoolVar[translation].ContainsKey(m_id))
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
        /**
         * Method returns true if node carries information about instance.
         * That menas that it reference to variable of instance.
         * Otherwise returns false.
         */
        public override bool isInstanceNode()
        {
            if (m_instance_id == null)
                return false;
            else
                return true;
        }
    }
    public abstract class BinaryCompareOp : BinaryOp
    {
        public override GameStatData.NodeReturnType ReturnType(Dictionary<string, string> mapParam)
        {
            return GameStatData.NodeReturnType.BOOL;
        }
    }
    public class EqualNode : BinaryCompareOp
    {
        public override void evalNode(Dictionary<string, string> mapParam,out bool returnVal)
        {
            if (!isReturnTypeSame(m_lNode,m_rNode,mapParam))
                throw new UnxpectedParamTypeExc(m_rNode.ReturnType(mapParam), m_lNode.ReturnType(mapParam), GameStatData.BoolOperators.EQUAL);
            bool lRetVal;
            bool rRetVal;
            int lRetVal_int;
            int rRetVal_int;
            switch (m_lNode.ReturnType(mapParam))
            {
                case GameStatData.NodeReturnType.BOOL:
                    m_lNode.evalNode(mapParam,out lRetVal);
                    m_rNode.evalNode(mapParam,out rRetVal);
                    returnVal = lRetVal == rRetVal;
                    break;
                case GameStatData.NodeReturnType.INT:
                    m_lNode.evalNode(mapParam,out lRetVal_int);
                    m_rNode.evalNode(mapParam,out rRetVal_int);
                    returnVal = lRetVal_int == rRetVal_int;
                    break;
                case GameStatData.NodeReturnType.INT_BOOL:
                    if (m_rNode.ReturnType(mapParam) == GameStatData.NodeReturnType.INT)
                    {
                        m_lNode.evalNode(mapParam,out lRetVal_int);
                        m_rNode.evalNode(mapParam,out rRetVal_int);
                        returnVal = lRetVal_int == rRetVal_int;
                    }
                    else
                        if (m_rNode.ReturnType(mapParam) == GameStatData.NodeReturnType.BOOL)
                        {
                            m_lNode.evalNode(mapParam,out lRetVal);
                            m_rNode.evalNode(mapParam,out rRetVal);
                            returnVal = lRetVal == rRetVal;
                        }
                        else
                            throw new CannotChooseVariableTypeExc(m_lNode.ToString());
                    break;
                default:
                    throw new UnxpectedParamTypeExc(m_lNode.ReturnType(mapParam), GameStatData.NodeReturnType.INT, GameStatData.BoolOperators.EQUAL);
            }

        }
    }
    public class NotEqualNode : BinaryCompareOp
    {
        public override void evalNode(Dictionary<string, string> mapParam,out bool returnVal)
        {
            if (!isReturnTypeSame(m_lNode, m_rNode,mapParam))
                throw new UnxpectedParamTypeExc(m_rNode.ReturnType(mapParam), m_lNode.ReturnType(mapParam),  GameStatData.BoolOperators.NOT_EQUAL);
            bool lRetVal;
            bool rRetVal;
            int lRetVal_int;
            int rRetVal_int;
            switch (m_lNode.ReturnType(mapParam))
            {
                case GameStatData.NodeReturnType.BOOL:
                    m_lNode.evalNode(mapParam,out lRetVal);
                    m_rNode.evalNode(mapParam,out rRetVal);
                    returnVal = lRetVal != rRetVal;
                    break;
                case GameStatData.NodeReturnType.INT:
                    m_lNode.evalNode(mapParam,out lRetVal_int);
                    m_rNode.evalNode(mapParam,out rRetVal_int);
                    returnVal = lRetVal_int != rRetVal_int;
                    break;
                case GameStatData.NodeReturnType.INT_BOOL:
                    if (m_rNode.ReturnType(mapParam) == GameStatData.NodeReturnType.INT)
                    {
                        m_lNode.evalNode(mapParam,out lRetVal_int);
                        m_rNode.evalNode(mapParam,out rRetVal_int);
                        returnVal = lRetVal_int != rRetVal_int;
                    }
                    else
                        if (m_rNode.ReturnType(mapParam) == GameStatData.NodeReturnType.BOOL)
                        {
                            m_lNode.evalNode(mapParam,out lRetVal);
                            m_rNode.evalNode(mapParam,out rRetVal);
                            returnVal = lRetVal != rRetVal;
                        }
                        else
                            throw new CannotChooseVariableTypeExc(m_lNode.ToString());
                    break;
                default:
                    throw new UnxpectedParamTypeExc(m_lNode.ReturnType(mapParam), GameStatData.NodeReturnType.INT, GameStatData.BoolOperators.NOT_EQUAL);
            }

        }
    }
    public class LessThenNode : BinaryCompareOp
    {
        public override void evalNode(Dictionary<string, string> mapParam,out bool returnVal)
        {
            if (!isReturnTypeOfBothInt(m_lNode, m_rNode,mapParam))
                throw new UnxpectedParamTypeExc(m_rNode.ReturnType(mapParam), m_lNode.ReturnType(mapParam), GameStatData.BoolOperators.LESS_THEN);
            int lRetVal_int;
            int rRetVal_int;
            m_lNode.evalNode(mapParam,out lRetVal_int);
            m_rNode.evalNode(mapParam,out rRetVal_int);
            returnVal = lRetVal_int < rRetVal_int;
        }
    }
    public class LessOrEQNode : BinaryCompareOp
    {
        public override void evalNode(Dictionary<string, string> mapParam,out bool returnVal)
        {
            if (!isReturnTypeOfBothInt(m_lNode, m_rNode,mapParam))
                throw new UnxpectedParamTypeExc(m_rNode.ReturnType(mapParam), m_lNode.ReturnType(mapParam), GameStatData.BoolOperators.LESS_OR_EQUAL);
            int lRetVal_int;
            int rRetVal_int;
            m_lNode.evalNode(mapParam,out lRetVal_int);
            m_rNode.evalNode(mapParam,out rRetVal_int);
            returnVal = lRetVal_int <= rRetVal_int;

        }
    }
    public class MoreThenNode : BinaryCompareOp
    {
        public override void evalNode(Dictionary<string, string> mapParam,out bool returnVal)
        {
            if (!isReturnTypeOfBothInt(m_lNode, m_rNode,mapParam))
                throw new UnxpectedParamTypeExc(m_rNode.ReturnType(mapParam), m_lNode.ReturnType(mapParam), GameStatData.BoolOperators.MORE_THEN);
            int lRetVal_int;
            int rRetVal_int;
            m_lNode.evalNode(mapParam,out lRetVal_int);
            m_rNode.evalNode(mapParam,out rRetVal_int);
            returnVal = lRetVal_int > rRetVal_int;

        }
    }
    public class MoreOrEQNode : BinaryCompareOp
    {
        public override void evalNode(Dictionary<string, string> mapParam,out bool returnVal)
        {
            if (!isReturnTypeOfBothInt(m_lNode, m_rNode,mapParam))
                throw new UnxpectedParamTypeExc(m_rNode.ReturnType(mapParam), m_lNode.ReturnType(mapParam), GameStatData.BoolOperators.MORE_OR_EQUAL);
            int lRetVal_int;
            int rRetVal_int;
            m_lNode.evalNode(mapParam,out lRetVal_int);
            m_rNode.evalNode(mapParam,out rRetVal_int);
            returnVal = lRetVal_int >= rRetVal_int;

        }
    }
    public abstract class NewAssign : EvaluationNode
    {
        protected EvaluationNode m_lNode;
        protected EvaluationNode m_rNode;

        public virtual void SetNodes(EvaluationNode lNode, EvaluationNode rNode)
        {
            this.m_lNode = lNode;
            this.m_rNode = rNode;
        }
        public void assignVl(int value, Dictionary<string, string> mapParam, GameStatData.AssignOperators op)
        {
            //test    PREROB NA FUNKCIU
            // id node is guaranted by grammar
            if (!m_lNode.isInstanceNode())
            { //throw some exception
            }
            // split name to instance name and variable name
            string[] wrds = m_lNode.ToString().Split('.');
            string trgInstance;
            // get instance name from map
            if (!mapParam.TryGetValue(wrds[0], out trgInstance))
            {
                // throw some error not existing instance for mapping
            }
            if (GroundingParams.m_InstanceIntegerVar.ContainsKey(trgInstance))
            {

                if (!GroundingParams.m_InstanceIntegerVar[trgInstance].ContainsKey(wrds[1]))
                {
                    // throw some error not existing variable of instance
                }
                switch (op)
                {
                    case GameStatData.AssignOperators.ASSIGN:
                        GroundingParams.m_InstanceIntegerVar[trgInstance][wrds[1]] = value;
                        break;
                    case GameStatData.AssignOperators.ADD_ASSIGN:
                        GroundingParams.m_InstanceIntegerVar[trgInstance][wrds[1]] += value;
                        break;
                    case GameStatData.AssignOperators.DIVISION_ASSIGN:
                        GroundingParams.m_InstanceIntegerVar[trgInstance][wrds[1]] /= value;
                        break;
                    case GameStatData.AssignOperators.MODULO_ASSIGN:
                        GroundingParams.m_InstanceIntegerVar[trgInstance][wrds[1]] %= value;
                        break;
                    case GameStatData.AssignOperators.MULTIPL_ASSIGN:
                        GroundingParams.m_InstanceIntegerVar[trgInstance][wrds[1]] *= value;
                        break;
                    case GameStatData.AssignOperators.SUBSTRACT_ASSIGN:
                        GroundingParams.m_InstanceIntegerVar[trgInstance][wrds[1]] -= value;
                        break;
                    //default: throw new Exceptions(); // Exception for unknown assign type
                }

            }
            else
            {
                // throw some error not existing container of variables for instance
            }
            //test
        }
        public void assignVl(bool value, Dictionary<string, string> mapParam)
        {
            //test    PREROB NA FUNKCIU
            // id node is guaranted by grammar
            if (!m_lNode.isInstanceNode())
            { //throw some exception
            }
            // split name to instance name and variable name
            string[] wrds = m_lNode.ToString().Split('.');
            string trgInstance;
            // get instance name from map
            if (!mapParam.TryGetValue(wrds[0], out trgInstance))
            {
                // throw some error not existing instance for mapping
            }
            if (GroundingParams.m_InstanceBoolVar.ContainsKey(trgInstance))
            {

                if (!GroundingParams.m_InstanceBoolVar[trgInstance].ContainsKey(wrds[1]))
                {
                    // throw some error not existing variable of instance
                }
                GroundingParams.m_InstanceBoolVar[trgInstance][wrds[1]] = value;
            }
            else
            {
                // throw some error not existing container of variables for instance
            }
            //test
        }
    }
    public class Assign : NewAssign
    {
        
        public override void evalNode(Dictionary<string,string> mapParam)
        {
            if (!m_lNode.isReturnTypeSame(m_lNode, m_rNode, mapParam))
                throw new UnxpectedParamTypeExc(m_rNode.ReturnType(mapParam), m_lNode.ReturnType(mapParam), GameStatData.AssignOperators.ASSIGN);
            bool rRetVal;
            int rRetVal_int;
            switch (m_lNode.ReturnType(mapParam))
            {
                case GameStatData.NodeReturnType.BOOL:
                    m_rNode.evalNode(mapParam,out rRetVal);
                    //test    PREROB NA FUNKCIU
                    assignVl(rRetVal, mapParam);
                    //test
                    //GroundingParams.m_InstanceBoolVar[m_lNode.ToString()] = rRetVal;
                    break;
                case GameStatData.NodeReturnType.INT:
                    m_rNode.evalNode(mapParam,out rRetVal_int);
                    assignVl(rRetVal_int, mapParam,GameStatData.AssignOperators.ASSIGN);
                    //GroundingParams.m_InstanceIntegerVar[m_lNode.ToString()] = rRetVal_int;
                    break;
                case GameStatData.NodeReturnType.INT_BOOL:
                    if (m_rNode.ReturnType(mapParam) == GameStatData.NodeReturnType.INT)
                    {
                        m_rNode.evalNode(mapParam,out rRetVal_int);
                        assignVl(rRetVal_int, mapParam, GameStatData.AssignOperators.ASSIGN);
                        //GroundingParams.m_InstanceIntegerVar[m_lNode.ToString()] = rRetVal_int;
                    }
                    else
                        if (m_rNode.ReturnType(mapParam) == GameStatData.NodeReturnType.BOOL)
                        {
                            m_rNode.evalNode(mapParam,out rRetVal);
                            assignVl(rRetVal, mapParam);
                            //GroundingParams.m_InstanceBoolVar[m_lNode.ToString()] = rRetVal;
                        }
                        else
                            throw new CannotChooseVariableTypeExc(m_lNode.ToString());
                    break;
                default:
                    throw new UnxpectedParamTypeExc(m_lNode.ReturnType(mapParam), GameStatData.NodeReturnType.INT, GameStatData.AssignOperators.ASSIGN);
            }
        }
    }
    public class SubstractAssign : NewAssign 
    {
        public override void evalNode(Dictionary<string, string> mapParam)
        {
            if (!m_lNode.isReturnTypeOfBothInt(m_lNode, m_rNode, mapParam))
                throw new UnxpectedParamTypeExc(m_rNode.ReturnType(mapParam), m_lNode.ReturnType(mapParam), GameStatData.AssignOperators.SUBSTRACT_ASSIGN);
            int rRetVal_int;
            m_rNode.evalNode(mapParam,out rRetVal_int);
            assignVl(rRetVal_int, mapParam, GameStatData.AssignOperators.SUBSTRACT_ASSIGN);
            //GroundingParams.m_InstanceIntegerVar[m_lNode.ToString()] -= rRetVal_int;
        }
    }
    public class AddAssign : NewAssign
    {
        public override void evalNode(Dictionary<string, string> mapParam)
        {
            if (!m_lNode.isReturnTypeOfBothInt(m_lNode, m_rNode, mapParam))
                throw new UnxpectedParamTypeExc(m_rNode.ReturnType(mapParam), m_lNode.ReturnType(mapParam), GameStatData.AssignOperators.SUBSTRACT_ASSIGN);
            int rRetVal_int;
            m_rNode.evalNode(mapParam,out rRetVal_int);
            assignVl(rRetVal_int, mapParam,GameStatData.AssignOperators.ADD_ASSIGN);
            //GroundingParams.m_InstanceIntegerVar[m_lNode.ToString()] += rRetVal_int;
        }
    }
    public class DivisionAssign : NewAssign 
    {
        public override void evalNode(Dictionary<string, string> mapParam)
        {
            if (!m_lNode.isReturnTypeOfBothInt(m_lNode, m_rNode, mapParam))
                throw new UnxpectedParamTypeExc(m_rNode.ReturnType(mapParam), m_lNode.ReturnType(mapParam), GameStatData.AssignOperators.SUBSTRACT_ASSIGN);
            int rRetVal_int;
            m_rNode.evalNode(mapParam,out rRetVal_int);
            assignVl(rRetVal_int, mapParam, GameStatData.AssignOperators.DIVISION_ASSIGN);
            //GroundingParams.m_InstanceIntegerVar[m_lNode.ToString()] /= rRetVal_int;
        }
    }
    public class MultiplAssign : NewAssign
    {
        public override void evalNode(Dictionary<string, string> mapParam)
        {
            if (!m_lNode.isReturnTypeOfBothInt(m_lNode, m_rNode, mapParam))
                throw new UnxpectedParamTypeExc(m_rNode.ReturnType(mapParam), m_lNode.ReturnType(mapParam), GameStatData.AssignOperators.SUBSTRACT_ASSIGN);
            int rRetVal_int;
            m_rNode.evalNode(mapParam,out rRetVal_int);
            assignVl(rRetVal_int, mapParam, GameStatData.AssignOperators.MULTIPL_ASSIGN);
            //GroundingParams.m_InstanceIntegerVar[m_lNode.ToString()] *= rRetVal_int;
        }
    }
    public class ModAssign : NewAssign
    {
        public override void evalNode(Dictionary<string, string> mapParam)
        {
            if (!m_lNode.isReturnTypeOfBothInt(m_lNode, m_rNode, mapParam))
                throw new UnxpectedParamTypeExc(m_rNode.ReturnType(mapParam), m_lNode.ReturnType(mapParam), GameStatData.AssignOperators.SUBSTRACT_ASSIGN);
            int rRetVal_int;
            m_rNode.evalNode(mapParam,out rRetVal_int);
            assignVl(rRetVal_int, mapParam, GameStatData.AssignOperators.MODULO_ASSIGN);
        }
    }
    /**
     * Class encapsulation function for evaluation purpouses.
     * TODO
     */
    //public class FnNode : EvaluationNode
    //{
    //    // function to be evaluated
    //    //private Function m_function;
    //    public override void evalNode(Dictionary<string, string> mapParam)
    //    {
    //        base.evalNode(mapParam);
    //    }
    //    public override void evalNode(Dictionary<string, string> mapParam, out bool returnVal)
    //    {
    //        base.evalNode(mapParam, out returnVal);
    //    }
    //    public override bool isInstanceNode()
    //    {
    //        return false;
    //    }
    //}
}
