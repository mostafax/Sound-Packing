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



            //  BaseOperations.Initlaize("AudiosInfo.txt", "readme.txt");
            // for (int i = 0; i < BaseOperations.Audio_files.Count; i++)
            // Console.WriteLine(BaseOperations.Audio_files[i].total_in_sec);
            //BaseOperations.SortInDecreasing();
            // FittingOperations.worst_fit(@"c:\Audios", @"c:\Output\[1] worestfit");
            //FittingOperations.filling.write(@"c:\Audios", @"c:\Output\[4] folderfilling");
            //            BaseOperations.Initlaize("AudiosInfo.txt", "readme.txt");

            //    FittingOperations.BestFit.Best_fit();


            //set the source (input-audios folder) in the same way here
            //       string input = @"C:\Audios\";
            //set the destination(output folder)in the same way here
            //     string output = @"C:\";



            //   FittingOperations.BestFit.BestFitFilling(input, output);
            ///Just more
            ///
            ///

            // FittingOperations.First_fit(@"c:\Audios", @"c:\Output\[1] FirstfitDecreasing");
            FittingOperations.worst_fitp(@"c:\Audios", @"c:\Output\[1] FirstfitDecreasing");

        }
    }
}
