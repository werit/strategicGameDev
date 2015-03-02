using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using EnvironmentCreator.Gammars;
using System.IO;

namespace EnvironmentCreator
{
    /// <summary>
    /// Class consist of methods preparing simulation and evaluating it in every tick of game engine.
    /// Class also handes evaluation of end effects of <see cref="Action"/>s.
    /// </summary>
    public class GameProcessing
    {
        /// <summary>
        /// internal variable storing information whether class was initialized.
        /// </summary>
        private bool m_isInitialized = false;
        /// <summary>
        /// Variable storing path information to input file.
        /// </summary>
        private string m_inputFile;
        /// <summary>
        /// Internal storage for time delta passed between two upadtes of game engine.
        /// </summary>
        private double m_timeDelta;
        /// <summary>
        /// Internal storage of effects of actions that have to be evaluated when duration pass.
        /// </summary>
        private List<TriggerEndEff> m_endEffects = new List<TriggerEndEff>();
        /// <summary>
        /// Internal storage for class which generates parameters of <see cref="Actions"/>
        /// </summary>
        private ParamGenerator m_parameterGenerator = new ParamGenerator();
        /// <summary>
        /// Method which returns list of names of Instances of <paramref name="type"/> passed as argument.
        /// </summary>
        /// <param name="type">Parameter is <see cref="Type"/> whose <see cref="Instance"/>s will be used in <see cref="Action"/> as its parameters.</param>
        /// <returns>List of all possible instances and also instances of descendants.</returns>
        private List<string> GetInstanceNamesOfTypeRecursively(Types type)
        {
            List<string> instanceNames = new List<string>();
            for (int i = 0; i < type.GetInst().Length; i++)
            {
                instanceNames.Add(type.GetInst()[i].GetInstanceName());
            }
            for (int i = 0; i < type.GetDesc().Length; i++)
            {
                instanceNames.AddRange(GetInstanceNamesOfTypeRecursively(type.GetDesc()[i]));
            }
            return instanceNames;
        }
        /// <summary>
        /// Method initialize by defining grammar which will be processed.
        /// Grammar is definition for all <see cref="Types"/>,<see cref="Instance"/> and <see cref="Action"/> strunctures.
        /// Method also setting update time of game engine.
        /// </summary>
        /// <param name="delta">Time between two fixed updates in game engine.</param>
        /// <param name="inputFile">File of consisting of input corresponding to grammar definition.</param>
        public void Initialize(double delta,string inputFile)
        {
            m_isInitialized = true;
            m_inputFile = inputFile;
            if (delta > 0.0)
                this.m_timeDelta = delta;
            else
                this.m_timeDelta = 0.2;
        }
        /// <summary>
        /// Method process grammar and defines all structures.
        /// Then world with structures and actions is created by user input.
        /// </summary>
        public void Start()
        {
            if (!m_isInitialized)
                throw new Exception();
            StreamReader fileReader = new StreamReader(this.m_inputFile);
            AntlrInputStream antInpStr = new AntlrInputStream(fileReader.ReadToEnd());
            GStratLexer lexer = new GStratLexer(antInpStr);
            //CalculatorLexer lexer = new CalculatorLexer(antInpStr);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            GStratParser parser = new GStratParser(tokens);
            //CalculatorParser parser = new CalculatorParser(tokens);
            IParseTree tree = parser.root();
            Console.WriteLine(tree.ToStringTree(parser));

            Console.WriteLine(GameStatData.m_int_VIS.Visit(tree));
            // cVis = new CalculatorVisitor();
            //Console.WriteLine(cVis.Visit(tree));
            //Console.WriteLine(cVis.getCount());
        }
        /// <summary>
        /// Method handgling behaviour of world in every tick of game engine.
        /// This method have to be called in fixedUpdate of game engine.
        /// </summary>
        /// <param name="delta">time delta of two updates of game engine.</param>
        public void Update(double delta)
        {
            this.m_timeDelta = delta;
            for (int i = 0; i < m_endEffects.Count; ++i)
            {
                m_endEffects[i].NextIteration((int)m_timeDelta);
            }
            m_endEffects.RemoveAll(delegate(TriggerEndEff eff) { return eff.GetLeftDur() <= 0; });
            // evaluate all actions

            foreach (Action act in GroundingParams.m_actions.Values)
            {
                string[] paramTypeName = act.GetParameterTypesNames();
                Types paramType;
                List<string>[] instanceNames = new List<string>[paramTypeName.Length];
                for (int i = 0; i < paramTypeName.Length; i++)
                {
                    if (GroundingParams.m_types.TryGetValue(paramTypeName[i], out paramType))
                        instanceNames[i] = GetInstanceNamesOfTypeRecursively(paramType);
                }
                m_parameterGenerator.GenerateParamFromType(instanceNames);
                string[] actionParam = m_parameterGenerator.NextParamSet();
                // call action with all possible parameters
                while (actionParam != null)
                {
                    act.Call(actionParam);
                    actionParam = m_parameterGenerator.NextParamSet();
                }
            }
        }
        /// <summary>
        /// Method add end effects of <see cref="Action"/>s, that has been evaluated.
        /// </summary>
        /// <param name="effEnd">End effects to be evaluated with time left to their evaluation.</param>
        public void AddActionEndEff(TriggerEndEff effEnd)
        {
            m_endEffects.Add(effEnd);
        }
        /// <summary>
        /// Property used to set time between updates of game engine.
        /// </summary>
        public double TimeDelta
        {
            get { return this.m_timeDelta; }
            set { this.m_timeDelta = TimeDelta; }
        }
    }
}
