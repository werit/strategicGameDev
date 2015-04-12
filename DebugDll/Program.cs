using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvironmentCreator;



namespace DebugDll
{
    class Program
    {
        static void Main(string[] args)
        {
            GameStatData.game.Initialize(0.2, "C:\\Users\\msi\\Documents\\UnityProjects\\RTSGame\\Assets\\RST\\worldDesc.txt");
            //Factory fctr = new Factory();
            //IGramChoice choice = Factory.getChoiceObj("car");
            //Console.WriteLine(choice.Buy());
            //fctr.ParsGramm();
            GameStatData.game.Start();
            for (int i = 0; i < 100; i++)
            {
                GameStatData.game.Update(0.2);
            }
        }
    }
}
