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
        public static List<Audios> Temp_aud = new List<Audios>();
        public static List<Audios> ans = new List<Audios>();
        public static int [,]dp=new int [200,800];
        public static int fill(int i)
        {
            int sol1 = 0, sol2 = 0;
            if (count == BaseOperations.max_size)
            {
                if (count > MaxCount)
                {
                    MaxCount = count;
                    ans = new List<Audios>(Temp_aud);
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
                    ans = new List<Audios>(Temp_aud);
                }
                return 0;
            }
            if (dp[i,count] != -2)
                return dp[i,count];
            if ((count + BaseOperations.Audio_files[i].total_in_sec) <= BaseOperations.max_size)
            {
                Temp_aud.Add(BaseOperations.Audio_files[i]);
                count += BaseOperations.Audio_files[i].total_in_sec;
                sol1 = fill(i + 1) + count;
                Temp_aud.RemoveAt(Temp_aud.Count - 1);
                count -= BaseOperations.Audio_files[i].total_in_sec;
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
                    for (int num = 0; num < BaseOperations.Audio_files.Count; num++)
                    {
                        path = Destination;
                        FileName = path;
                        source = Source;
                        path += @"\f" + k;
                        di = Directory.CreateDirectory(path);
                        source += @"\" + (BaseOperations.Audio_files[num].index) + ".mp3";
                        destination = path + @"\" + (BaseOperations.Audio_files[num].index) + ".mp3";
                        System.IO.File.Copy(source, destination);
                        Console.WriteLine(source + "  " + destination);
                        FileName += @"\f" + k + "_MetaData.txt";
                        System.IO.FileStream F1 = new FileStream(FileName, FileMode.Create);
                        StreamWriter w1 = new StreamWriter(F1);
                        w1.WriteLine("f" + k);
                        w1.WriteLine(BaseOperations.Audio_files[num].name + " " + BaseOperations.Audio_files[num].hours + ":" + BaseOperations.Audio_files[num].min + ":" + BaseOperations.Audio_files[num].sec);
                        w1.WriteLine(BaseOperations.Audio_files[num].hours + ":" + BaseOperations.Audio_files[num].min + ":" + BaseOperations.Audio_files[num].sec);
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
                Audios tem = new Audios();
                for (int j = 0; j < no; j++)
                {
                    w.WriteLine(ans[j].name + " " + ans[j].hours + ":" + ans[j].min + ":" + ans[j].sec);
                    tem.hours += ans[j].hours;
                    tem.min += ans[j].min;
                    tem.sec += ans[j].sec;
                }
                w.WriteLine(tem.hours + ":" + tem.min + ":" + tem.sec);
                k++;
                w.Close();
                int var = 0;
                for (int it = 0; it < no; it++)
                {
                    var = ans[it].index - 1 - h;
                    if (var < 0)
                        var = 0;
                    BaseOperations.Audio_files.RemoveAt(var);
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
                Temp_folder.hours = BaseOperations.Audio_files[i].hours;
                Temp_folder.min = BaseOperations.Audio_files[i].min;
                Temp_folder.sec = BaseOperations.Audio_files[i].sec;
                Temp_folder.free_space = BaseOperations.max_size;
                Temp_folder.free_space -= BaseOperations.Audio_files[i].total_in_sec;
                BaseOperations.Audio_Folders.Add(Temp_folder);
                source += @"\" + (BaseOperations.Audio_files[i].index) + ".mp3";
                destination = path + @"\" + (BaseOperations.Audio_files[i].index) + ".mp3";
                System.IO.File.Move(source, destination);
                FileName += @"\f" + k + "_MetaData.txt";
                System.IO.FileStream F = new FileStream(FileName, FileMode.Create);
                StreamWriter w = new StreamWriter(F);
                w.WriteLine("f" + k);
                w.WriteLine(BaseOperations.Audio_files[i].name + " " + BaseOperations.Audio_files[i].hours + ":" + BaseOperations.Audio_files[i].min + ":" + BaseOperations.Audio_files[i].sec);
                w.Close();
                k++;

            }
            else
            {
                for (int j = 0; j < directoryCount; j++)
                {
                    if (BaseOperations.Audio_Folders[j].free_space > max && BaseOperations.Audio_Folders[j].free_space >= BaseOperations.Audio_files[i].total_in_sec)
                    {
                        max = BaseOperations.Audio_Folders[j].free_space;
                        index = j;
                        test = true;
                    }
                }
                if (test)
                {
                    BaseOperations.Audio_Folders[index].hours += BaseOperations.Audio_files[i].hours;
                    BaseOperations.Audio_Folders[index].min += BaseOperations.Audio_files[i].min;
                    BaseOperations.Audio_Folders[index].sec += BaseOperations.Audio_files[i].sec;
                    BaseOperations.Audio_Folders[index].free_space -= BaseOperations.Audio_files[i].total_in_sec;
                    path += @"\f" + (index + 1);
                    source += @"\" + (BaseOperations.Audio_files[i].index) + ".mp3";
                    destination = path + @"\" + (BaseOperations.Audio_files[i].index) + ".mp3";
                    System.IO.File.Move(source, destination);
                    FileName += @"\f" + (index + 1) + "_MetaData.txt";
                    System.IO.FileStream F = new FileStream(FileName, FileMode.Append);
                    StreamWriter w = new StreamWriter(F);
                    w.WriteLine(BaseOperations.Audio_files[i].name + " " + BaseOperations.Audio_files[i].hours + ":" + BaseOperations.Audio_files[i].min + ":" + BaseOperations.Audio_files[i].sec);
                    w.Close();
                }
                else
                {
                    path += @"\f" + k;
                    DirectoryInfo di = Directory.CreateDirectory(path);
                    Temp_folder.hours = BaseOperations.Audio_files[i].hours;
                    Temp_folder.min = BaseOperations.Audio_files[i].min;
                    Temp_folder.sec = BaseOperations.Audio_files[i].sec;
                    Temp_folder.free_space = BaseOperations.max_size;
                    Temp_folder.free_space -= BaseOperations.Audio_files[i].total_in_sec;
                    BaseOperations.Audio_Folders.Add(Temp_folder);
                    source += @"\" + (BaseOperations.Audio_files[i].index) + ".mp3";
                    destination = path + @"\" + (BaseOperations.Audio_files[i].index) + ".mp3";
                    System.IO.File.Move(source, destination);
                    FileName += @"\f" + k + "_MetaData" + ".txt";
                    System.IO.FileStream F = new FileStream(FileName, FileMode.Create);
                    StreamWriter w = new StreamWriter(F);
                    w.WriteLine("f" + k);
                    w.WriteLine(BaseOperations.Audio_files[i].name + " " + BaseOperations.Audio_files[i].hours + ":" + BaseOperations.Audio_files[i].min + ":" + BaseOperations.Audio_files[i].sec);
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
            w.WriteLine(BaseOperations.Audio_Folders[no].hours + ":" + BaseOperations.Audio_Folders[no].min + ":" + BaseOperations.Audio_Folders[no].sec);
            w.Close();
        }

    }



    static public void First_fit(string Source, string Destination)
    {
        BaseOperations.Initlaize("AudiosInfo.txt", "readme.txt");
        BaseOperations.SortInDecreasing();
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
            bool test = true;
            Folder Temp_folder = new Folder();
            if (directoryCount == 0)
            {
                path += @"\f" + k;
                DirectoryInfo di = Directory.CreateDirectory(path);
                Temp_folder.hours = BaseOperations.Audio_files[i].hours;
                Temp_folder.min = BaseOperations.Audio_files[i].min;
                Temp_folder.sec = BaseOperations.Audio_files[i].sec;
                Temp_folder.free_space = BaseOperations.max_size;
                Temp_folder.free_space -= BaseOperations.Audio_files[i].total_in_sec;
                BaseOperations.Audio_Folders.Add(Temp_folder);
                source += @"\" + (BaseOperations.Audio_files[i].index) + ".mp3";
                destination = path + @"\" + (BaseOperations.Audio_files[i].index) + ".mp3";
                System.IO.File.Move(source, destination);
                FileName += @"\f" + k + "_MetaData.txt";
                System.IO.FileStream F = new FileStream(FileName, FileMode.Create);
                StreamWriter w = new StreamWriter(F);
                w.WriteLine("f" + k);
                w.WriteLine(BaseOperations.Audio_files[i].name + " " + BaseOperations.Audio_files[i].hours + ":" + BaseOperations.Audio_files[i].min + ":" + BaseOperations.Audio_files[i].sec);
                w.Close();
                k++;

            }
            else
            {
                for (int j = 0; j < directoryCount; j++)
                {
                    if (BaseOperations.Audio_Folders[j].free_space >= BaseOperations.Audio_files[i].total_in_sec)
                    {
                        BaseOperations.Audio_Folders[j].hours += BaseOperations.Audio_files[i].hours;
                        BaseOperations.Audio_Folders[j].min += BaseOperations.Audio_files[i].min;
                        BaseOperations.Audio_Folders[j].sec += BaseOperations.Audio_files[i].sec;
                        BaseOperations.Audio_Folders[j].free_space -= BaseOperations.Audio_files[i].total_in_sec;
                        path += @"\f" + (j + 1);
                        source += @"\" + (BaseOperations.Audio_files[i].index) + ".mp3";
                        destination = path + @"\" + (BaseOperations.Audio_files[i].index) + ".mp3";
                        System.IO.File.Move(source, destination);
                        FileName += @"\f" + (j + 1) + "_MetaData.txt";
                        System.IO.FileStream F = new FileStream(FileName, FileMode.Append);
                        StreamWriter w = new StreamWriter(F);
                        w.WriteLine(BaseOperations.Audio_files[i].name + " " + BaseOperations.Audio_files[i].hours + ":" + BaseOperations.Audio_files[i].min + ":" + BaseOperations.Audio_files[i].sec);
                        w.Close();
                        test = false;
                        break;

                    }
                }
                if (test)
                {
                    path = Destination;
                    source = Source;
                    destination = "";
                    FileName = path;
                    path += @"\f" + k;
                    DirectoryInfo di = Directory.CreateDirectory(path);
                    Temp_folder.hours = BaseOperations.Audio_files[i].hours;
                    Temp_folder.min = BaseOperations.Audio_files[i].min;
                    Temp_folder.sec = BaseOperations.Audio_files[i].sec;
                    Temp_folder.free_space = BaseOperations.max_size;
                    Temp_folder.free_space -= BaseOperations.Audio_files[i].total_in_sec;
                    BaseOperations.Audio_Folders.Add(Temp_folder);
                    source += @"\" + (BaseOperations.Audio_files[i].index) + ".mp3";
                    destination = path + @"\" + (BaseOperations.Audio_files[i].index) + ".mp3";
                    System.IO.File.Move(source, destination);
                    FileName += @"\f" + k + "_MetaData" + ".txt";
                    System.IO.FileStream F = new FileStream(FileName, FileMode.Create);
                    StreamWriter w = new StreamWriter(F);
                    w.WriteLine("f" + k);
                    w.WriteLine(BaseOperations.Audio_files[i].name + " " + BaseOperations.Audio_files[i].hours + ":" + BaseOperations.Audio_files[i].min + ":" + BaseOperations.Audio_files[i].sec);
                    w.Close();
                    k++;
                }

            }
        }
        int no = 0;
        FileName = Destination + @"\f" + (no + 1) + "_MetaData.txt";
        for (; no < k - 1; no++)
        {
            FileName = @"c:\Output\[1] FirstfitDecreasing\f" + (no + 1) + "_MetaData.txt";
            System.IO.FileStream F = new FileStream(FileName, FileMode.Append);
            StreamWriter w = new StreamWriter(F);
            w.WriteLine(BaseOperations.Audio_Folders[no].hours + ":" + BaseOperations.Audio_Folders[no].min + ":" + BaseOperations.Audio_Folders[no].sec);
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

    static public class BestFit
    {

        //intializing the folders list .. will contain the data of each folder
        static public List<Folder> BFfolders = new List<Folder>(BaseOperations.num_of_rec);
        static public bool fits = false;
        static public void Best_fit()
        {
            for (int i = 0; i < BaseOperations.Audio_files.Count; i++)
            {
                fits = false;
                if (BFfolders.Count == 0)
                {
                    Folder tmp = new Folder();
                    tmp.free_space = BaseOperations.max_size;
                    BFfolders.Add(tmp);
                    BFfolders[0].free_space -= BaseOperations.Audio_files[i].total_in_sec;
                    BFfolders[0].hours += BaseOperations.Audio_files[i].hours;
                    BFfolders[0].min += BaseOperations.Audio_files[i].min;
                    BFfolders[0].sec += BaseOperations.Audio_files[i].sec;
                    BFfolders[0].FolderAudios.Add(BaseOperations.Audio_files[i]);
                    fits = true;
                }
                else
                {
                    BFfolders.Sort((x, y) => x.free_space.CompareTo(y.free_space));
                    for (int j = 0; j < BFfolders.Count; j++)
                    {
                        if (BFfolders[j].free_space >= BaseOperations.Audio_files[i].total_in_sec)
                        {
                            BFfolders[j].free_space -= BaseOperations.Audio_files[i].total_in_sec;
                            BFfolders[j].hours += BaseOperations.Audio_files[i].hours;
                            BFfolders[j].min += BaseOperations.Audio_files[i].min;
                            BFfolders[j].sec += BaseOperations.Audio_files[i].sec;
                            BFfolders[j].FolderAudios.Add(BaseOperations.Audio_files[i]);
                            fits = true;
                            break;

                        }
                    }
                }

                if (fits == false)
                {

                    Folder tmp = new Folder();
                    tmp.free_space = BaseOperations.max_size;
                    BFfolders.Add(tmp);
                    BFfolders[BFfolders.Count - 1].free_space -= BaseOperations.Audio_files[i].total_in_sec;
                    BFfolders[BFfolders.Count - 1].hours += BaseOperations.Audio_files[i].hours;
                    BFfolders[BFfolders.Count - 1].min += BaseOperations.Audio_files[i].min;
                    BFfolders[BFfolders.Count - 1].sec += BaseOperations.Audio_files[i].sec;
                    BFfolders[BFfolders.Count - 1].FolderAudios.Add(BaseOperations.Audio_files[i]);
                    fits = true;
                }

            }

        }



        static public void BestFitFilling(string InputFolderpath, string OutputFolderpath)
        {
            BaseOperations.Initlaize("AudiosInfo.txt", "readme.txt");
            // creating output folder if not exist
            if (!Directory.Exists(OutputFolderpath))
            {
                DirectoryInfo di = Directory.CreateDirectory(OutputFolderpath);
            }
            //creating Bestfit folder if not exist
            string bestfitpath = OutputFolderpath + @"\[5] bestfit";
            if (!Directory.Exists(bestfitpath))
            {
                DirectoryInfo di2 = Directory.CreateDirectory(bestfitpath);
            }
            //creating files with the names in BFfolders if not exist
            for (int i = 0; i < BFfolders.Count; i++)
            {
                string foldercount = (i + 1).ToString();
                string folderpath = bestfitpath + @"\F" + foldercount;
                if (!Directory.Exists(folderpath))
                {
                    DirectoryInfo di3 = Directory.CreateDirectory(folderpath);
                }
                string folderMetaData = bestfitpath + @"\F" + foldercount + "_METADATA.txt";
                System.IO.FileStream F1 = new FileStream(folderMetaData, FileMode.Create);
                StreamWriter w1 = new StreamWriter(F1);
                w1.WriteLine("F" + foldercount);

                for (int j = 0; j < BFfolders[i].FolderAudios.Count; j++)
                {
                    string filepath = InputFolderpath + BFfolders[i].FolderAudios[j].name;
                    string filetarget = folderpath + @"\" + BFfolders[i].FolderAudios[j].name;
                    System.IO.File.Copy(filepath, filetarget, true);

                    w1.WriteLine(BFfolders[i].FolderAudios[j].name + " " + BFfolders[i].FolderAudios[j].hours + ":" + BFfolders[i].FolderAudios[j].min + ":" + BFfolders[i].FolderAudios[j].sec);


                }
                w1.WriteLine(BFfolders[i].hours + ":" + BFfolders[i].min + ":" + BFfolders[i].sec);
                w1.Close();



            }


        }

    }

}



