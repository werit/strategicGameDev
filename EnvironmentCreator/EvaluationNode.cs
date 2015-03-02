using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;

namespace EnvironmentCreator
{
    /// <summary>
    ///  Main class representing all nodes used in evaluation tree of expresions.
    ///  Class might be used as base for assign nodes, plus nodes and others.
    ///  Class consist of methods that determine type of node, for integer or boolean evaluation and\n
    ///  also evaluation methods for booth types.
    /// </summary>
    public abstract class EvaluationNode
    {
        /// <summary>
        /// Base method for evaluation node that return neither integer nor boolean value.
        /// Method throws exception if it is not redefined.
        /// </summary>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        public virtual void evalNode(Dictionary<string, string> mapParam){
            throw new FunctionNotSupportedExcp(this.GetType().GetMethod(GameStatData.GetCurrentMethod()));
        }
        /// <summary>
        /// Method says true if node is IdNode and has two part name.
        /// Which means that name contains also instance name and name of parameter.
        /// Like "target.HP", where target is speudo instance in action and HP is actual variable on some instance which corresponds to target.
        /// </summary>
        /// <returns>Returns true if <see cref="IDNode"/>.</returns>
        public virtual bool isInstanceNode()
        {
            return false;
        }
        /// <summary>
        /// Base method for evaluation node that return boolean value.
        /// Method throws exception if method is not redefined in descendant and method is called on this instance.
        /// </summary>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        /// <param name="returnVal">Parameter returns true or false depending on it's evaluation.</param>
        public virtual void evalNode(Dictionary<string, string> mapParam, out bool returnVal)
        {
            throw new FunctionNotSupportedExcp(this.GetType().GetMethod(GameStatData.GetCurrentMethod()));
        }
        /// <summary>
        /// Base method for evaluation node that return integer value.
        /// Method throws exception if method is not redefined in descendant and method is called on this instance.
        /// </summary>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        /// <param name="returnVal">Parameter returns integer depending on it's evaluation.</param>
        public virtual void evalNode(Dictionary<string, string> mapParam, out int returnVal)
        {
            throw new FunctionNotSupportedExcp(this.GetType().GetMethod(GameStatData.GetCurrentMethod()));
        }
        /// <summary>
        /// Method gives information about return type of node.\n
        /// Return types are defined in <see cref="GameStatData.NodeReturnType"/>.
        /// This must be redefined in descendant types.
        /// </summary>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        /// <returns>Returns <see cref="GameStatData.NodeReturnType"/> depending on evaluation node.</returns>
        public abstract GameStatData.NodeReturnType ReturnType(Dictionary<string, string> mapParam);
        /// <summary>
        /// Method determines if evaluation nodes passed as parameters have both return type boolean.\n
        /// It should be used to check if user used right parameters for evaluation of booleans.\n
        /// For example if we are comparing boolean to variable of <see cref="Instance"/>, then variable have to be of boolean type.
        /// </summary>
        /// <param name="node1">First node which is tested if it returns boolean value.</param>
        /// <param name="node2">Second node which is tested if it returns boolean value.</param>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        /// <returns></returns>
        public bool isReturnTypeOfBothBool(EvaluationNode node1, EvaluationNode node2, Dictionary<string, string> mapParam)
        {
            return (node1.ReturnType(mapParam) == GameStatData.NodeReturnType.BOOL || node1.ReturnType(mapParam) == GameStatData.NodeReturnType.INT_BOOL) && isReturnTypeSame(node1, node2, mapParam);
        }
        /// <summary>
        /// Method determines if evaluation nodes passed as parameters have both return type integer.\n
        /// It should be used to check if user used right parameters for evaluation of integers.\n
        /// For example if we are comparing integer to a variable of <see cref="Instance"/>, then variable have to be of integer type.
        /// </summary>
        /// <param name="node1">First node which is tested if it returns integer value.</param>
        /// <param name="node2">Second node which is tested if it returns integer value.</param>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        /// <returns></returns>
        public bool isReturnTypeOfBothInt(EvaluationNode node1, EvaluationNode node2, Dictionary<string, string> mapParam)
        {
            return (node1.ReturnType(mapParam) == GameStatData.NodeReturnType.INT || node1.ReturnType(mapParam) == GameStatData.NodeReturnType.INT_BOOL) && isReturnTypeSame(node1, node2, mapParam);
        }
        /// <summary>
        /// Method tests if <see cref="EvaluationNode"/>s passed as parameters returns values of the same type.\n
        /// Return types tested are from <see cref="GameStatData.NodeReturnType"/>.
        /// </summary>
        /// <param name="node1">First node which is tested if it returns same type as second node.</param>
        /// <param name="node2">Second node which is tested if it returns same type as first node.</param>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        /// <returns>Returns true if both nodes passed as parameter return same type. Otherwise false is returned.</returns>
        public bool isReturnTypeSame(EvaluationNode node1, EvaluationNode node2, Dictionary<string, string> mapParam)
        {
            return node1.ReturnType(mapParam) == node2.ReturnType(mapParam) ||
                ((node1.ReturnType(mapParam) == GameStatData.NodeReturnType.INT_BOOL && (node2.ReturnType(mapParam) == GameStatData.NodeReturnType.INT || node2.ReturnType(mapParam) == GameStatData.NodeReturnType.BOOL)) ||
                (node2.ReturnType(mapParam) == GameStatData.NodeReturnType.INT_BOOL && (node1.ReturnType(mapParam) == GameStatData.NodeReturnType.INT || node1.ReturnType(mapParam) == GameStatData.NodeReturnType.BOOL)));
        }
    }


    /// <summary>
    /// Class encapsulating storage of integer value.
    /// Class redefine all integer associated execution methods.
    /// Sole purpose of class is returning integer stored in it while being able to store this integer in tree structure of <see cref="EvaluationNode"/>,/n
    /// which is recursively evaluated throught virtual methods. 
    /// </summary>
    public class IntNode : EvaluationNode
    {
        /// <summary>
        /// Internal storage of integer value.
        /// This value is returned, when node is evaluated.
        /// </summary>
        protected int m_value;
        /// <summary>
        /// Override parent method and returns data type corresponding to integer value;
        /// </summary>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        /// <returns>Returns <see cref="GameStatData.NodeReturnType"/> coresponding to integer. Therefore <see cref="GameStatData.NodeReturnType.INT"/>.</returns>
        public override GameStatData.NodeReturnType ReturnType(Dictionary<string, string> mapParam)
        {
            return GameStatData.NodeReturnType.INT;
        }
        /// <summary>
        /// Method overrides parent method and make class useable for evaluation, in which integer is returned.
        /// </summary>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        /// <param name="returnVal">Vaule is inicialized to integer value of this node.</param>
        public override void evalNode(Dictionary<string, string> mapParam,out int returnVal)
        {
            returnVal = m_value;
        }
        /// <summary>
        /// Method sets integer value of this node to <paramref name="value"/>.
        /// </summary>
        /// <param name="value">Integer value which will be stored in this node.</param>
        public void SetValue(int value)
        {
            this.m_value = value;
        }
        /// <summary>
        /// Constructor creating node with int value passed in parameter as return value during evaluation of node.
        /// </summary>
        /// <param name="value">Value of node.</param>
        public IntNode(int value)
        {
            m_value = value;
        }

    }
    /// <summary>
    /// Class encapsulating evaluation of binary node.\n
    /// Class adds internal storage for two <see cref="EvaluationNode"/>s and method which handls setting those nodes.
    /// </summary>
    public abstract class BinaryOp : EvaluationNode
    {
        /// <summary>
        /// Left node of binary node.
        /// </summary>
        protected EvaluationNode m_lNode;
        /// <summary>
        /// right node of binary node.
        /// </summary>
        protected EvaluationNode m_rNode;
        /// <summary>
        /// Method sets passed parameters as left and right nodes of this binary node.
        /// </summary>
        /// <param name="lNode">Parameter of left node of binary node.</param>
        /// <param name="rNode">Parameter of right node of binary node.</param>
        public virtual void SetNodes(EvaluationNode lNode, EvaluationNode rNode)
        {
            this.m_lNode = lNode;
            this.m_rNode = rNode;
        }
    }
    /// <summary>
    /// Class encapsulating evaluation of mathematical operation of binary node.<see cref="BinaryOp"/>\n
    /// Supported mathematical operations are <see cref="GameStatData.AritmeticOperators"/> .\n
    /// Class redefines <see cref="BinaryMathOp.ReturnType"/> to integer value.
    /// </summary>
    public abstract class BinaryMathOp : BinaryOp
    {
        /// <summary>
        /// Redefinition of ancestor method.\n
        /// Math operation can return only integer, therefore redefinition is set to integer.
        /// </summary>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        /// <returns>Integer value from <see cref="GameStatData.NodeReturnType"/>.</returns>
        public override GameStatData.NodeReturnType ReturnType(Dictionary<string, string> mapParam)
        {
            return GameStatData.NodeReturnType.INT;
        }
        /// <summary>
        /// Method determines value of this node depending on operation type of node, which is passed as parameter <paramref name="op"/>\n
        /// and values of child nodes, which are evaluated by this method.
        /// </summary>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        /// <param name="op">Methematical operation of node,which is applied to result values of children nodes.</param>
        /// <returns></returns>
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
    /// <summary>
    /// Class defining evaluation of addition of two numbers.\n
    /// Class redefines evaluation method of node.
    /// </summary>
    public class PlusNode : BinaryMathOp
    {
        /// <summary>
        /// Method redefining ancestor method, which returns integer value of addition between left and right children nodes.
        /// </summary>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        /// <param name="returnVal">Value of node after addition operation of left and right children nodes.</param>
        public override void evalNode(Dictionary<string, string> mapParam,out int returnVal)
        {
            returnVal = result(mapParam,GameStatData.AritmeticOperators.ADDITION);
        }

    }
    /// <summary>
    /// Class defining evaluation of subtraction of two numbers.\n
    /// Class redefines evaluation method of node.
    /// </summary>
    public class MinusNode : BinaryMathOp
    {
        /// <summary>
        /// Method redefining ancestor method, which returns integer value of addition between left and right children nodes.
        /// </summary>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        /// <param name="returnVal">Value of node after subtraction operation of left and right children nodes.</param>
        public override void evalNode(Dictionary<string, string> mapParam,out int returnVal)
        {
            returnVal = result(mapParam,GameStatData.AritmeticOperators.SUBSTRACTION);
        }
    }
    /// <summary>
    /// Class defining evaluation of multiplication of two numbers.\n
    /// Class redefines evaluation method of node.
    /// </summary>
    public class MulNode : BinaryMathOp
    {
        /// <summary>
        /// Method redefining ancestor method, which returns integer value of addition between left and right children nodes.
        /// </summary>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        /// <param name="returnVal">Value of node after multiplication operation of left and right children nodes.</param>
        public override void evalNode(Dictionary<string, string> mapParam,out int returnVal)
        {
            returnVal = result(mapParam, GameStatData.AritmeticOperators.MULTIPLICATION);
        }
    }
    /// <summary>
    /// Class defining evaluation of division of two numbers.\n
    /// Class redefines evaluation method of node.
    /// </summary>
    public class DivNode : BinaryMathOp
    {
        /// <summary>
        /// Method redefining ancestor method, which returns integer value of addition between left and right children nodes.
        /// </summary>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        /// <param name="returnVal">Value of node after division operation of left and right children nodes.</param>
        public override void evalNode(Dictionary<string, string> mapParam,out int returnVal)
        {
            returnVal = result(mapParam,GameStatData.AritmeticOperators.DIVISION);
        }
    }
    /// <summary>
    /// Class defining evaluation of modulation of two numbers.\n
    /// Class redefines evaluation method of node.
    /// </summary>
    public class ModNode : BinaryMathOp
    {
        /// <summary>
        /// Method redefining ancestor method, which returns integer value of addition between left and right children nodes.
        /// </summary>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        /// <param name="returnVal">Value of node after modulation operation of left and right children nodes.</param>
        public override void evalNode(Dictionary<string, string> mapParam,out int returnVal)
        {
            returnVal = result(mapParam,GameStatData.AritmeticOperators.MODULATION);
        }
    }
    /// <summary>
    /// Class defines encapsulation for instance parameter identifier.\n
    /// It's evaluation and also defines method for return type which is dependent on stored indentifier.\n
    /// Class stores name of <see cref="Instance"/> and name of its variable. Both in string format.
    /// </summary>
    public class IDNode : EvaluationNode
    {
        /// <summary>
        /// Variable of instance identifier.
        /// </summary>
        protected string m_id;
        /// <summary>
        /// Instance identifier.
        /// </summary>
        protected string m_instance_id;
        /// <summary>
        /// Method redefining ancestor method. Return value is dependent on type of variable, which is determined at run time.
        /// This is due to possibility of definition of <see cref="Action"/> ahead of <see cref="Types"/>.
        /// METHOD THROWS ERROR IF INSTANCE ID CANNOT BE FOUND.
        /// </summary>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        /// <returns>Method returns one of <see cref="GameStatData.NodeReturnType"/> depending on stored variable type.</returns>
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
        /// <summary>
        /// Method sets <see cref="Instance"/> bool or integer variable identifier.
        /// </summary>
        /// <param name="id">Name of variable.</param>
        public void SetId(string id)
        {
            if(id != null)
                this.m_id = id;
        }
        /// <summary>
        /// Constructor creates class with <see cref="Instance"/> name in string format passed as parameter and
        /// its viariable name in string format passed in parameter <paramref name="param"/>.
        /// </summary>
        /// <param name="instance">Name of <see cref="Instance"/> in string format.</param>
        /// <param name="param">Name of <see cref="Instance"/>'s variable in string format.</param>
        public IDNode(string instance, string param)
        {
            this.m_id = param;
            this.m_instance_id = instance;
        }
        /// <summary>
        /// Method redefines ancestor method.\n
        /// Method takes string format of <see cref="Instance"/> name and concatenate it with stored name of variable.
        /// </summary>
        /// <returns>Method returns name of <see cref="Instance"/> and name of variable of this instance separated by dot.</returns>
        public override string ToString()
        {
            if (m_instance_id != null)
                return this.m_instance_id + '.' + this.m_id;
            else
                return this.m_id;
        }
        /// <summary>
        /// Method redefines ancestor method and 
        /// returns true if node carries information about instance name.\n
        /// Method does not say whether <see cref="Instance"/> does exist or not.
        /// </summary>
        /// <returns>True if name of <see cref="Instance"/> is filled, false otherwise.</returns>
        public override bool isInstanceNode()
        {
            if (m_instance_id == null)
                return false;
            else
                return true;
        }
    }
    /// <summary>
    /// Class encapsulating evaluation of copmare operation of binary node.<see cref="BinaryOp"/>\n
    /// Supported compare operations are ==,!=,<,>,<=,>=.\n
    /// Class redefines <see cref="BinaryMathOp.ReturnType"/> to boolean value.
    /// </summary>
    public abstract class BinaryCompareOp : BinaryOp
    {
        /// <summary>
        /// Redefinition of ancestor method.\n
        /// Compare operation can return only boolean, therefore redefinition is set to boolean.
        /// </summary>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        /// <returns>Boolean value from <see cref="GameStatData.NodeReturnType"/>.</returns>
        public override GameStatData.NodeReturnType ReturnType(Dictionary<string, string> mapParam)
        {
            return GameStatData.NodeReturnType.BOOL;
        }
    }
    /// <summary>
    /// Class redefines evaluation of determining equality of two nodes storing return value from <see cref="BinaryMathOp.ReturnType"/>.\n
    /// </summary>
    public class EqualNode : BinaryCompareOp
    {
        /// <summary>
        /// Method redefining ancestor method, which returns boolean value depending on equality of children nodes.
        /// </summary>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        /// <param name="returnVal">Boolean value of node between equality operation of left and right children nodes.</param>
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
    /// <summary>
    /// Class redefines evaluation of determining unequality of two nodes storing return value from <see cref="BinaryMathOp.ReturnType"/>.\n
    /// </summary>
    public class NotEqualNode : BinaryCompareOp
    {
        /// <summary>
        /// Method redefining ancestor method, which returns boolean value depending on not equality of children nodes.
        /// </summary>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        /// <param name="returnVal">Boolean value of node between unequality operation of left and right children nodes.</param>
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
    /// <summary>
    /// Class redefines evaluation of determining truthfulness of comparision of two nodes storing integer return value.\n
    /// Comparision operation is left node is less then right node
    /// </summary>
    public class LessThenNode : BinaryCompareOp
    {
        /// <summary>
        /// Method redefining ancestor method, which returns boolean value depending on comparision of left and right node to less then operation.
        /// </summary>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        /// <param name="returnVal">Boolean value of node between less then operation of left and right children nodes.</param>
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
    /// <summary>
    /// Class redefines evaluation of determining truthfulness of comparision of two nodes storing integer return value.\n
    /// Comparision operation is left node is less then or equal to right node
    /// </summary>
    public class LessOrEQNode : BinaryCompareOp
    {
        /// <summary>
        /// Method redefining ancestor method, which returns boolean value depending on comparision of left and right node to less then or equal operation.
        /// </summary>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        /// <param name="returnVal">Boolean value of node between less then or equal operation of left and right children nodes.</param>
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
    /// <summary>
    /// Class redefines evaluation of determining truthfulness of comparision of two nodes storing integer return value.\n
    /// Comparision operation is left node is more then right node
    /// </summary>
    public class MoreThenNode : BinaryCompareOp
    {
        /// <summary>
        /// Method redefining ancestor method, which returns boolean value depending on comparision of left and right node to more then operation.
        /// </summary>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        /// <param name="returnVal">Boolean value of node between more then operation of left and right children nodes.</param>
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
    /// <summary>
    /// Class redefines evaluation of determining truthfulness of comparision of two nodes storing integer return value.\n
    /// Comparision operation is left node is more then or equal to right node
    /// </summary>
    public class MoreOrEQNode : BinaryCompareOp
    {
        /// <summary>
        /// Method redefining ancestor method, which returns boolean value depending on comparision of left and right node to more then or equal operation.
        /// </summary>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        /// <param name="returnVal">Boolean value of node between more then or equal operation of left and right children nodes.</param>
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
    /// <summary>
    /// Class encapsulation assigning value of right operand to left operand.
    /// Class represents right and left operand as nodes of binary node assign.
    /// Class presents assign method that is used when assigning left node value of right node.
    /// </summary>
    public abstract class NewAssign : EvaluationNode
    {
        /// <summary>
        /// Variable represnting return type of a node.
        /// </summary>
        protected GameStatData.NodeReturnType m_returnType;
        /// <summary>
        /// Left node of assign node. Node whom value of right node is assigned.
        /// </summary>
        protected EvaluationNode m_lNode;
        /// <summary>
        /// Right node of assign node. Node whose value is assigned to left node.
        /// </summary>
        protected EvaluationNode m_rNode;
        /// <summary>
        /// Method gives ability to insert nodes which are used in assign operation.
        /// Constructor does not have this ability, therefore this method is used.
        /// </summary>
        /// <param name="lNode">Left side of assign. Node to which will be assigned value of right node.</param>
        /// <param name="rNode">Right side of assign. Node which value will be assigned to left node.</param>
        public virtual void SetNodes(EvaluationNode lNode, EvaluationNode rNode)
        {
            this.m_lNode = lNode;
            this.m_rNode = rNode;
        }
        /// <summary>
        /// Method processing assignng value to integer typed left node.
        /// Method takes argument <paramref name="value"/> and assign this value to left node.\n
        /// This assign is dependent on one of <see cref="GameStatData.AssignOperators"/>.
        /// For example <see cref="GameStatData.AssignOperators.DIVISION_ASSIGN"/> takes <paramref name="value"/> and divide left node by it \n
        /// and then set left node to this new value.
        /// </summary>
        /// <param name="value">Integer value of right node.</param>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        /// <param name="op">Operation whish is performed during assigning.</param>
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
        /// <summary>
        /// Method processing assignng value to boolean typed left node.
        /// Method takes argument <paramref name="value"/> and assign this value to left node.\n
        /// </summary>
        /// <param name="value">Boolean value of right node.</param>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
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
    /// <summary>
    /// Class encapsulating assign of integer to <see cref="Instance"/> integer variable or boolean to <see cref="Instance"/> boolean variable.\n
    /// Assign is performed between left node, which stores variable, and right node storing value to be assigned.\n
    /// Return type of assign depend on parameters in children nodes.
    /// </summary>
    public class Assign : NewAssign
    {
        /// <summary>
        /// Method evaluates right node and his value is assigned to left node. Both nodes have to have same return type.
        /// </summary>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
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
        /// <summary>
        /// Method overrides ancestor method and determines return type of node.
        /// Return type might be boolean or integer.
        /// </summary>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        /// <returns>Returns boolean or integer values from <see cref="GameStatData.NodeReturnType"/>.</returns>
        public override GameStatData.NodeReturnType ReturnType(Dictionary<string, string> mapParam)
        {
            System.Diagnostics.Debug.Assert(this.m_lNode != null && this.m_rNode != null);
            switch (m_lNode.ReturnType(mapParam))
            {
                case GameStatData.NodeReturnType.BOOL:
                    return GameStatData.NodeReturnType.BOOL;
                    
                case GameStatData.NodeReturnType.INT:
                    return GameStatData.NodeReturnType.INT;
                case GameStatData.NodeReturnType.INT_BOOL:
                    if (m_rNode.ReturnType(mapParam) == GameStatData.NodeReturnType.INT)
                        return GameStatData.NodeReturnType.INT;
                    else
                        if (m_rNode.ReturnType(mapParam) == GameStatData.NodeReturnType.BOOL)
                            return GameStatData.NodeReturnType.BOOL;
                        else
                            throw new CannotChooseVariableTypeExc(m_lNode.ToString());
                default:
                    throw new UnxpectedParamTypeExc(m_lNode.ReturnType(mapParam), GameStatData.NodeReturnType.INT, GameStatData.AssignOperators.ASSIGN);
            }
        }
    }
    /// <summary>
    /// Class encapsulating subtraction assign of integer to <see cref="Instance"/> integer variable .\n
    /// Assign is performed between left node, which stores variable, and right node storing value to be assigned.\n
    /// Return type of this assign is integer.
    /// </summary>
    public class SubstractAssign : NewAssign 
    {
        /// <summary>
        /// Method evaluates right node and his value is subtracted from left node and then result is assigned to it. Both nodes have to have integer return type.
        /// </summary>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        public override void evalNode(Dictionary<string, string> mapParam)
        {
            if (!m_lNode.isReturnTypeOfBothInt(m_lNode, m_rNode, mapParam))
                throw new UnxpectedParamTypeExc(m_rNode.ReturnType(mapParam), m_lNode.ReturnType(mapParam), GameStatData.AssignOperators.SUBSTRACT_ASSIGN);
            int rRetVal_int;
            m_rNode.evalNode(mapParam,out rRetVal_int);
            assignVl(rRetVal_int, mapParam, GameStatData.AssignOperators.SUBSTRACT_ASSIGN);
            //GroundingParams.m_InstanceIntegerVar[m_lNode.ToString()] -= rRetVal_int;
        }
        /// <summary>
        /// Method overrides ancestor method and set return type of node to integer.
        /// </summary>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        /// <returns>Returns integer value from <see cref="GameStatData.NodeReturnType"/>.</returns>
        public override GameStatData.NodeReturnType ReturnType(Dictionary<string, string> mapParam)
        {
            return GameStatData.NodeReturnType.INT;
        }
    }
    /// <summary>
    /// Class encapsulating addition assign of integer to <see cref="Instance"/> integer variable .\n
    /// Assign is performed between left node, which stores variable, and right node storing value to be assigned.\n
    /// Return type of this assign is integer.
    /// </summary>
    public class AddAssign : NewAssign
    {
        /// <summary>
        /// Method evaluates right node and his value is added to left node and then result is assigned to it. Both nodes have to have integer return type.
        /// </summary>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        public override void evalNode(Dictionary<string, string> mapParam)
        {
            if (!m_lNode.isReturnTypeOfBothInt(m_lNode, m_rNode, mapParam))
                throw new UnxpectedParamTypeExc(m_rNode.ReturnType(mapParam), m_lNode.ReturnType(mapParam), GameStatData.AssignOperators.SUBSTRACT_ASSIGN);
            int rRetVal_int;
            m_rNode.evalNode(mapParam,out rRetVal_int);
            assignVl(rRetVal_int, mapParam,GameStatData.AssignOperators.ADD_ASSIGN);
            //GroundingParams.m_InstanceIntegerVar[m_lNode.ToString()] += rRetVal_int;
        }
        /// <summary>
        /// Method overrides ancestor method and set return type of node to integer.
        /// </summary>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        /// <returns>Returns integer value from <see cref="GameStatData.NodeReturnType"/>.</returns>
        public override GameStatData.NodeReturnType ReturnType(Dictionary<string, string> mapParam)
        {
            return GameStatData.NodeReturnType.INT;
        }
    }
    /// <summary>
    /// Class encapsulating division assign of integer to <see cref="Instance"/> integer variable .\n
    /// Assign is performed between left node, which stores variable, and right node storing value to be assigned.\n
    /// Return type of this assign is integer.
    /// </summary>
    public class DivisionAssign : NewAssign 
    {
        /// <summary>
        /// Method evaluates right node and his value is divisor for left node and result is then assigned to it. Both nodes have to have integer return type.
        /// </summary>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        public override void evalNode(Dictionary<string, string> mapParam)
        {
            if (!m_lNode.isReturnTypeOfBothInt(m_lNode, m_rNode, mapParam))
                throw new UnxpectedParamTypeExc(m_rNode.ReturnType(mapParam), m_lNode.ReturnType(mapParam), GameStatData.AssignOperators.SUBSTRACT_ASSIGN);
            int rRetVal_int;
            m_rNode.evalNode(mapParam,out rRetVal_int);
            assignVl(rRetVal_int, mapParam, GameStatData.AssignOperators.DIVISION_ASSIGN);
            //GroundingParams.m_InstanceIntegerVar[m_lNode.ToString()] /= rRetVal_int;
        }
        /// <summary>
        /// Method overrides ancestor method and set return type of node to integer.
        /// </summary>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        /// <returns>Returns integer value from <see cref="GameStatData.NodeReturnType"/>.</returns>
        public override GameStatData.NodeReturnType ReturnType(Dictionary<string, string> mapParam)
        {
            return GameStatData.NodeReturnType.INT;
        }
    }
    /// <summary>
    /// Class encapsulating multiplication assign of integer to <see cref="Instance"/> integer variable .\n
    /// Assign is performed between left node, which stores variable, and right node storing value to be assigned.\n
    /// Return type of this assign is integer.
    /// </summary>
    public class MultiplAssign : NewAssign
    {
        /// <summary>
        /// Method evaluates right node and his value is multipicator for left node and then result is assigned to it. Both nodes have to have integer return type.
        /// </summary>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        public override void evalNode(Dictionary<string, string> mapParam)
        {
            if (!m_lNode.isReturnTypeOfBothInt(m_lNode, m_rNode, mapParam))
                throw new UnxpectedParamTypeExc(m_rNode.ReturnType(mapParam), m_lNode.ReturnType(mapParam), GameStatData.AssignOperators.SUBSTRACT_ASSIGN);
            int rRetVal_int;
            m_rNode.evalNode(mapParam,out rRetVal_int);
            assignVl(rRetVal_int, mapParam, GameStatData.AssignOperators.MULTIPL_ASSIGN);
            //GroundingParams.m_InstanceIntegerVar[m_lNode.ToString()] *= rRetVal_int;
        }
        /// <summary>
        /// Method overrides ancestor method and set return type of node to integer.
        /// </summary>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        /// <returns>Returns integer value from <see cref="GameStatData.NodeReturnType"/>.</returns>
        public override GameStatData.NodeReturnType ReturnType(Dictionary<string, string> mapParam)
        {
            return GameStatData.NodeReturnType.INT;
        }
    }
    /// <summary>
    /// Class encapsulating modulation assign of integer to <see cref="Instance"/> integer variable .\n
    /// Assign is performed between left node, which stores variable, and right node storing value to be assigned.\n
    /// Return type of this assign is integer.
    /// </summary>
    public class ModAssign : NewAssign
    {
        /// <summary>
        /// Method evaluates right node and his value is modulator for left node and then result is assigned to it. Both nodes have to have integer return type.
        /// </summary>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        public override void evalNode(Dictionary<string, string> mapParam)
        {
            if (!m_lNode.isReturnTypeOfBothInt(m_lNode, m_rNode, mapParam))
                throw new UnxpectedParamTypeExc(m_rNode.ReturnType(mapParam), m_lNode.ReturnType(mapParam), GameStatData.AssignOperators.SUBSTRACT_ASSIGN);
            int rRetVal_int;
            m_rNode.evalNode(mapParam,out rRetVal_int);
            assignVl(rRetVal_int, mapParam, GameStatData.AssignOperators.MODULO_ASSIGN);
        }
        /// <summary>
        /// Method overrides ancestor method and set return type of node to integer.
        /// </summary>
        /// <param name="mapParam">Dictionary characterizing couple consisting of name of parameter and instance name attributable to that parameter.</param>
        /// <returns>Returns integer value from <see cref="GameStatData.NodeReturnType"/>.</returns>
        public override GameStatData.NodeReturnType ReturnType(Dictionary<string, string> mapParam)
        {
            return GameStatData.NodeReturnType.INT;
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
