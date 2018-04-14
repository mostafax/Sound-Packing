using System;
using System.IO;
using System.Collections.Generic;

/// <summary>
/// A class encabsulating all fitting algorithms
/// </summary>
static class FittingOperations
{
    public static class filling
    {
        public static int count = 0, MaxCount = 0;
        public static List<Audoios> Temp_aud = new List<Audoios>();
        public static List<Audoios> ans = new List<Audoios>();
        public static int [,]dp=new int [200,800];
        public static int fill(int i)
        {
            int sol1 = 0, sol2 = 0;
            if (count == BaseOperations.max_size)
            {
                if (count > MaxCount)
                {
                    MaxCount = count;
                    ans = new List<Audoios>(Temp_aud);
                }
                return 0;
            }
            if (count > BaseOperations.max_size)
                return -1;
            if (i >= BaseOperations.num_of_rec)
            {
                if (count > MaxCount)
                {
                    MaxCount = count;
                    ans = new List<Audoios>(Temp_aud);
                }
                return 0;
            }
            if (dp[i,count] != -2)
                return dp[i,count];
            if ((count + BaseOperations.Audoi_files[i].total_in_sec) <= BaseOperations.max_size)
            {
                Temp_aud.Add(BaseOperations.Audoi_files[i]);
                count += BaseOperations.Audoi_files[i].total_in_sec;
                sol1 = fill(i + 1) + count;
                Temp_aud.RemoveAt(Temp_aud.Count - 1);
                count -= BaseOperations.Audoi_files[i].total_in_sec;
            }

            sol2 = fill(i + 1);


            return dp[i,count]= Math.Max(sol1, sol2);
        }
        public static void write(string Source, string Destination)
        {
            BaseOperations.Initlaize("AudiosInfo.txt", "readme.txt");
            for (int i=0;i<200;i++)
            {
                for (int j = 0; j < 800; j++)
                    dp[i, j] = -2;
            }
            string path = Destination;
            if (!Directory.Exists(path))
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
            }
            int k = 1;
            string FileName = path;
            string source = Source;
            string destination = "";
            int no = 0;
            int h = 0;
            while (BaseOperations.num_of_rec != 0)
            {
                int i = 0;
                path = Destination;
                source = Source;
                destination = "";
                FileName = path;
                DirectoryInfo di;
                fill(0);
                no = ans.Count;
                for (int r=0; r<200; r++)

                {
                    for (int j = 0; j < 800; j++)
                        dp[i, j] = -2;
                }
                if (no == 1)
                {
                    for (int num = 0; num < BaseOperations.Audoi_files.Count; num++)
                    {
                        path = Destination;
                        FileName = path;
                        source = Source;
                        path += @"\f" + k;
                        di = Directory.CreateDirectory(path);
                        source += @"\" + (BaseOperations.Audoi_files[num].index) + ".mp3";
                        destination = path + @"\" + (BaseOperations.Audoi_files[num].index) + ".mp3";
                        System.IO.File.Copy(source, destination);
                        Console.WriteLine(source + "  " + destination);
                        FileName += @"\f" + k + "_MetaData.txt";
                        System.IO.FileStream F1 = new FileStream(FileName, FileMode.Create);
                        StreamWriter w1 = new StreamWriter(F1);
                        w1.WriteLine("f" + k);
                        w1.WriteLine(BaseOperations.Audoi_files[num].name + " " + BaseOperations.Audoi_files[num].hourse + ":" + BaseOperations.Audoi_files[num].min + ":" + BaseOperations.Audoi_files[num].sec);
                        w1.WriteLine(BaseOperations.Audoi_files[num].hourse + ":" + BaseOperations.Audoi_files[num].min + ":" + BaseOperations.Audoi_files[num].sec);
                        w1.Close();
                        k++;
                        i++;
                    }
                    return;
                }
                path += @"\f" + k;
                di = Directory.CreateDirectory(path);
                int w3 = 0;
                for (; i < no; i++)
                {
                    source = Source;
                    source += @"\" + (ans[w3].index) + ".mp3";
                    destination = path + @"\" + (ans[w3].index) + ".mp3";
                    System.IO.File.Copy(source, destination);
                    w3++;
                }
                FileName += @"\f" + k + "_MetaData.txt";
                System.IO.FileStream F = new FileStream(FileName, FileMode.Create);
                StreamWriter w = new StreamWriter(F);
                w.WriteLine("f" + k);
                Audoios tem = new Audoios();
                for (int j = 0; j < no; j++)
                {
                    w.WriteLine(ans[j].name + " " + ans[j].hourse + ":" + ans[j].min + ":" + ans[j].sec);
                    tem.hourse += ans[j].hourse;
                    tem.min += ans[j].min;
                    tem.sec += ans[j].sec;
                }
                w.WriteLine(tem.hourse + ":" + tem.min + ":" + tem.sec);
                k++;
                w.Close();
                int var = 0;
                for (int it = 0; it < no; it++)
                {
                    var = ans[it].index - 1 - h;
                    if (var < 0)
                        var = 0;
                    BaseOperations.Audoi_files.RemoveAt(var);
                    h++;
                }

                BaseOperations.num_of_rec -= no;
                ans.Clear();
                count = 0;
                MaxCount = 0;

            }
        }
    };




    static public void worst_fit(string Source, string Destination)
    {
        string path = Destination;
        if (!Directory.Exists(path))
        {
            DirectoryInfo di = Directory.CreateDirectory(path);
        }
        int k = 1;
        string FileName = path;
        string source = Source;
        string destination = "";
        for (int i = 0; i < BaseOperations.num_of_rec; i++)
        {
            path = Destination;
            source = Source;
            destination = "";
            FileName = path;
            int directoryCount = System.IO.Directory.GetDirectories(path).Length;
            long max = 0;
            int index = 0;
            bool test = false;
            Folder Temp_folder = new Folder();
            if (directoryCount == 0)
            {
                path += @"\f" + k;
                DirectoryInfo di = Directory.CreateDirectory(path);
                Temp_folder.hourse = BaseOperations.Audoi_files[i].hourse;
                Temp_folder.min = BaseOperations.Audoi_files[i].min;
                Temp_folder.sec = BaseOperations.Audoi_files[i].sec;
                Temp_folder.free_space = BaseOperations.max_size;
                Temp_folder.free_space -= BaseOperations.Audoi_files[i].total_in_sec;
                BaseOperations.Audoi_Folders.Add(Temp_folder);
                source += @"\" + (BaseOperations.Audoi_files[i].index) + ".mp3";
                destination = path + @"\" + (BaseOperations.Audoi_files[i].index) + ".mp3";
                System.IO.File.Move(source, destination);
                FileName += @"\f" + k + "_MetaData.txt";
                System.IO.FileStream F = new FileStream(FileName, FileMode.Create);
                StreamWriter w = new StreamWriter(F);
                w.WriteLine("f" + k);
                w.WriteLine(BaseOperations.Audoi_files[i].name + " " + BaseOperations.Audoi_files[i].hourse + ":" + BaseOperations.Audoi_files[i].min + ":" + BaseOperations.Audoi_files[i].sec);
                w.Close();
                k++;

            }
            else
            {
                for (int j = 0; j < directoryCount; j++)
                {
                    if (BaseOperations.Audoi_Folders[j].free_space > max && BaseOperations.Audoi_Folders[j].free_space >= BaseOperations.Audoi_files[i].total_in_sec)
                    {
                        max = BaseOperations.Audoi_Folders[j].free_space;
                        index = j;
                        test = true;
                    }
                }
                if (test)
                {
                    BaseOperations.Audoi_Folders[index].hourse += BaseOperations.Audoi_files[i].hourse;
                    BaseOperations.Audoi_Folders[index].min += BaseOperations.Audoi_files[i].min;
                    BaseOperations.Audoi_Folders[index].sec += BaseOperations.Audoi_files[i].sec;
                    BaseOperations.Audoi_Folders[index].free_space -= BaseOperations.Audoi_files[i].total_in_sec;
                    path += @"\f" + (index + 1);
                    source += @"\" + (BaseOperations.Audoi_files[i].index) + ".mp3";
                    destination = path + @"\" + (BaseOperations.Audoi_files[i].index) + ".mp3";
                    System.IO.File.Move(source, destination);
                    FileName += @"\f" + (index + 1) + "_MetaData.txt";
                    System.IO.FileStream F = new FileStream(FileName, FileMode.Append);
                    StreamWriter w = new StreamWriter(F);
                    w.WriteLine(BaseOperations.Audoi_files[i].name + " " + BaseOperations.Audoi_files[i].hourse + ":" + BaseOperations.Audoi_files[i].min + ":" + BaseOperations.Audoi_files[i].sec);
                    w.Close();
                }
                else
                {
                    path += @"\f" + k;
                    DirectoryInfo di = Directory.CreateDirectory(path);
                    Temp_folder.hourse = BaseOperations.Audoi_files[i].hourse;
                    Temp_folder.min = BaseOperations.Audoi_files[i].min;
                    Temp_folder.sec = BaseOperations.Audoi_files[i].sec;
                    Temp_folder.free_space = BaseOperations.max_size;
                    Temp_folder.free_space -= BaseOperations.Audoi_files[i].total_in_sec;
                    BaseOperations.Audoi_Folders.Add(Temp_folder);
                    source += @"\" + (BaseOperations.Audoi_files[i].index) + ".mp3";
                    destination = path + @"\" + (BaseOperations.Audoi_files[i].index) + ".mp3";
                    System.IO.File.Move(source, destination);
                    FileName += @"\f" + k + "_MetaData" + ".txt";
                    System.IO.FileStream F = new FileStream(FileName, FileMode.Create);
                    StreamWriter w = new StreamWriter(F);
                    w.WriteLine("f" + k);
                    w.WriteLine(BaseOperations.Audoi_files[i].name + " " + BaseOperations.Audoi_files[i].hourse + ":" + BaseOperations.Audoi_files[i].min + ":" + BaseOperations.Audoi_files[i].sec);
                    w.Close();
                    k++;
                }
            }

        }
        int no = 0;
        FileName = Destination + @"\f" + (no + 1) + "_MetaData.txt";
        for (; no < k - 1; no++)
        {
            FileName = @"c:\Output\[1] worstfit\f" + (no + 1) + "_MetaData.txt";
            System.IO.FileStream F = new FileStream(FileName, FileMode.Append);
            StreamWriter w = new StreamWriter(F);
            w.WriteLine(BaseOperations.Audoi_Folders[no].hourse + ":" + BaseOperations.Audoi_Folders[no].min + ":" + BaseOperations.Audoi_Folders[no].sec);
            w.Close();
        }

    }





    /// <summary>
    /// A class encabsulating all FirstFit stuff
    /// </summary>
    static class FirstFit
    {
        /// <summary>
        /// The entry point of the algorithm
        /// </summary>
        static public void Start()
        {
            //Initializing BaseOperations
            BaseOperations.Initlaize("AudiosInfo.txt", "readme.txt");
            //Iterate through files
            foreach (FileDescription file in BaseOperations.Files)
            {
                bool FoundFolder = false;
                //iterate through folders
                foreach (FolderDescription folder in BaseOperations.Folders)
                {
                    //check the file duration if it fits in the folder 
                    if (folder.CheckLength(file.Duration))
                    {
                        FoundFolder = true;
                        folder.CurrentLength += file.Duration;
                        BaseOperations.MoveFile(file, folder);
                        break;
                    }
                }
                //if there is no folder a new one will be created 
                if (!FoundFolder)
                {
                    FolderDescription folder = BaseOperations.ConstructFolder();
                    if (!folder.CheckLength(file.Duration))
                    {
                        throw new ArgumentOutOfRangeException("File size is larger than max folder size");
                    }
                    folder.CurrentLength += file.Duration;
                    BaseOperations.MoveFile(file, folder);
                }
            }
        }
    }

}



