using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using EnvironmentCreator.Gammars;

namespace EnvironmentCreator
{
    public class Factory
    {
        public void ParsGramm()
        {
            Console.Out.WriteLine("Hello world");
            StreamReader reader = new StreamReader(Console.OpenStandardInput());
            AntlrInputStream antInpStr = new AntlrInputStream(reader.ReadToEnd());
            CalculatorLexer lexer = new CalculatorLexer(antInpStr);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            CalculatorParser parser = new CalculatorParser(tokens);
            IParseTree tree = parser.prog();
            Console.WriteLine(tree.ToStringTree(parser));
            CalculatorVisitor cVis = new CalculatorVisitor();
            Console.WriteLine(cVis.Visit(tree));
            Console.WriteLine(cVis.getCount());
        }
        static public IGramChoice getChoiceObj(String objName){
            IGramChoice choice = null;
            switch (objName.ToLower())
	        {
                case "car" :
                    choice = new Car();
                    break;
                    
	        }
            return choice;
        }
        public void start()
        {
            Console.Out.WriteLine("Hello");
        }
    }
}
