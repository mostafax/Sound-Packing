using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundPacking
{
    class Program
    {
        static void Main(string[] args)
        {
            //BaseOperations.Initlaize("AudiosInfo.txt", "readme.txt");
            // BaseOperations.SortInDecreasing();
            // FittingOperations.worst_fit(@"c:\Audios", @"c:\Output\[4] folderfilling");
            //FittingOperations.filling.write(@"c:\Audios", @"c:\Output\[4] folderfilling");
            BaseOperations.Initlaize("AudiosInfo.txt", "readme.txt");

            FittingOperations.BestFit.Best_fit();


            //set the source (input-audios folder) in the same way here
            string input = @"C:\Users\pc\Downloads\Project\sample test\sample 1\INPUT\Audios\";
            //set the destination(output folder)in the same way here
            string output = @"C:\Users\pc\Downloads\Project\sample test\sample 1\output\";



            FittingOperations.BestFit.BestFitFilling(input, output);
            ///Just more
            ///
            ///
        }
    }
}
