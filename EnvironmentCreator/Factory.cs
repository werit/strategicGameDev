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
        public void ParseStrat()
        {
            StreamReader reader = new StreamReader(getPathFromUser());
            AntlrInputStream antlrInp = new AntlrInputStream(reader.ReadToEnd());
            GStratLexer lexer = new GStratLexer(antlrInp);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            GStratParser parser = new GStratParser(tokens);
            IParseTree tree = parser.root();
            Console.WriteLine(tree.ToStringTree(parser));
            GStrat_VIS_int visitor = new GStrat_VIS_int();
            Console.WriteLine(visitor.Visit(tree));

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
        /** Method retrieving path to actual grammar instance on disk.
         * 
         * */
        private string getPathFromUser()
        {
            return "C:\\Users\\msi\\Documents\\Visual Studio 2012\\Projects\\EnvironmentCreator\\EnvironmentCreator\\Gammars\\grammarExample.txt";
        }
    }

}
