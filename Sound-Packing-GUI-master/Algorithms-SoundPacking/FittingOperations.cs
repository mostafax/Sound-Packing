using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Algorithms_SoundPacking
{
    class FittingOperations
    {
        static public void worst_fitp(string Source, string Destination)
        {
            List<Audios> MyAudios = new List<Audios>(BasicOperations.Audio_files);
            List<Folder> MyFolders = new List<Folder>(MyAudios.Count);
            PriorityQueue<Folder> PQ = new PriorityQueue<Folder>();
            string path = Destination;
            if (!Directory.Exists(path))
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
            }
            int k = 0;
            string FileName = path;
            string source = Source;
            string destination = " ";
            int count = 0;
            
            for (int i = 0; i < BasicOperations.num_of_rec; i++)
            {
                if (count == 0)
                {
                    Folder Temp_folder = new Folder();
                    Folder temp = new Folder();
                    Temp_folder.hours = MyAudios[i].hours;
                    Temp_folder.min = MyAudios[i].min;
                    Temp_folder.sec = MyAudios[i].sec;
                    Temp_folder.free_space = BasicOperations.max_size;
                    Temp_folder.free_space -= MyAudios[i].total_in_sec;
                    Temp_folder.FolderAudios.Add(MyAudios[i]);
                    Temp_folder.index = k;
                    MyFolders.Add(Temp_folder);
                    PQ.Enqueue(Temp_folder);
                    k++;
                    count++;
                }
                else
                {
                    if (PQ.Peek().free_space >= MyAudios[i].total_in_sec)
                    {
                        Folder Temp_folder = new Folder();
                        Folder temp = new Folder();
                        temp = PQ.Peek();
                        PQ.Dequeue();
                        temp.hours += MyAudios[i].hours;
                        temp.min += MyAudios[i].min;
                        temp.sec += MyAudios[i].sec;
                        temp.free_space -= MyAudios[i].total_in_sec;
                        MyFolders[temp.index].FolderAudios.Add(MyAudios[i]);
                        if (temp.free_space != 0)
                        {
                            PQ.Enqueue(temp);
                        }
                        else
                        {
                            count--;
                        }
                    }
                    else
                    {
                        Folder Temp_folder = new Folder();
                        Folder temp = new Folder();
                        Temp_folder.index = k;
                        Temp_folder.hours = MyAudios[i].hours;
                        Temp_folder.min = MyAudios[i].min;
                        Temp_folder.sec = MyAudios[i].sec;
                        Temp_folder.free_space = BasicOperations.max_size;
                        Temp_folder.free_space -= MyAudios[i].total_in_sec;
                        Temp_folder.FolderAudios.Add(MyAudios[i]);
                        Temp_folder.index = k;
                        MyFolders.Add(Temp_folder);
                        PQ.Enqueue(Temp_folder);
                        k++;
                        count++;
                    }
                }
            }

            FileName = "";
            for (int no = 0; no < MyFolders.Count; no++)
            {
                int size = MyFolders[no].FolderAudios.Count;
                path = Destination;
                path += @"\f" + (no + 1);
                DirectoryInfo di = Directory.CreateDirectory(path);
                FileName = Destination + @"\f" + (no + 1) + "_MetaData.txt";
                System.IO.FileStream F = new FileStream(FileName, FileMode.Create);
                StreamWriter w = new StreamWriter(F);
                w.WriteLine("f" + (no + 1));
                for (int index = 0; index < size; index++)
                {
                    source = Source;
                    source += @"\" + MyFolders[no].FolderAudios[index].name;
                    destination = path + @"\" + MyFolders[no].FolderAudios[index].name;
                    Task.Run(() =>
                    {
                        System.IO.File.Copy(source, destination);

                    });
                    w.WriteLine(MyFolders[no].FolderAudios[index].name + " " + MyFolders[no].FolderAudios[index].hours + ":" + MyFolders[no].FolderAudios[index].min + ":" + MyFolders[no].FolderAudios[index].sec);
                }
                MyFolders[no].ConvertUnits();
                w.WriteLine(MyFolders[no].hours + ":" + MyFolders[no].min + ":" + MyFolders[no].sec);
                w.Close();
            }

        }


        static public void worst_fitp_dec(string Source, string Destination)
        {
            List<Audios> MyAudios = new List<Audios>(BasicOperations.Audio_files);
            List<Folder> MyFolders = new List<Folder>(MyAudios.Count);
            BasicOperations.SortInDecreasing(MyAudios);
            PriorityQueue<Folder> PQ = new PriorityQueue<Folder>();
            string path = Destination;
            if (!Directory.Exists(path))
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
            }
            int k = 0;
            string FileName = path;
            string source = Source;
            string destination = "";
            int count = 0;
            for (int i = 0; i < BasicOperations.num_of_rec; i++)
            {
                if (count == 0)
                {
                    Folder Temp_folder = new Folder();
                    Folder temp = new Folder();
                    Temp_folder.hours = MyAudios[i].hours;
                    Temp_folder.min = MyAudios[i].min;
                    Temp_folder.sec = MyAudios[i].sec;
                    Temp_folder.free_space = BasicOperations.max_size;
                    Temp_folder.free_space -= MyAudios[i].total_in_sec;
                    Temp_folder.FolderAudios.Add(MyAudios[i]);
                    Temp_folder.index = k;
                    MyFolders.Add(Temp_folder);
                    PQ.Enqueue(Temp_folder);
                    k++;
                    count++;

                }
                else
                {
                    if (PQ.Peek().free_space >= MyAudios[i].total_in_sec)
                    {
                        Folder Temp_folder = new Folder();
                        Folder temp = new Folder();
                        temp = PQ.Peek();
                        PQ.Dequeue();
                        temp.hours += MyAudios[i].hours;
                        temp.min += MyAudios[i].min;
                        temp.sec += MyAudios[i].sec;
                        temp.free_space -= MyAudios[i].total_in_sec;
                        MyFolders[temp.index].FolderAudios.Add(MyAudios[i]);
                        if (temp.free_space != 0)
                        {
                            PQ.Enqueue(temp);
                        }
                        else
                        {
                            count--;
                        }
                    }
                    else
                    {
                        Folder Temp_folder = new Folder();
                        Folder temp = new Folder();
                        Temp_folder.index = k;
                        Temp_folder.hours = MyAudios[i].hours;
                        Temp_folder.min = MyAudios[i].min;
                        Temp_folder.sec = MyAudios[i].sec;
                        Temp_folder.free_space = BasicOperations.max_size;
                        Temp_folder.free_space -= MyAudios[i].total_in_sec;
                        Temp_folder.FolderAudios.Add(MyAudios[i]);
                        Temp_folder.index = k;
                        MyFolders.Add(Temp_folder);
                        PQ.Enqueue(Temp_folder);
                        k++;
                        count++;
                    }
                }

            }

            FileName = "";
            for (int no = 0; no < MyFolders.Count; no++)
            {
                int size = MyFolders[no].FolderAudios.Count;
                path = Destination;
                path += @"\f" + (no + 1);
                DirectoryInfo di = Directory.CreateDirectory(path);
                FileName = Destination + @"\f" + (no + 1) + "_MetaData.txt";
                System.IO.FileStream F = new FileStream(FileName, FileMode.Create);
                StreamWriter w = new StreamWriter(F);
                w.WriteLine("f" + (no + 1));
                for (int index = 0; index < size; index++)
                {
                    source = Source;
                    source += @"\" + MyFolders[no].FolderAudios[index].name;
                    destination = path + @"\" + MyFolders[no].FolderAudios[index].name;
                    Task.Run(() =>
                    {
                        System.IO.File.Copy(source, destination);

                    });
                    w.WriteLine(MyFolders[no].FolderAudios[index].name + " " + MyFolders[no].FolderAudios[index].hours + ":" + MyFolders[no].FolderAudios[index].min + ":" + MyFolders[no].FolderAudios[index].sec);
                }
                w.WriteLine(MyFolders[no].hours + ":" + MyFolders[no].min + ":" + MyFolders[no].sec);
                w.Close();
            }

        }




        public static class filling
        {
            public static int count = 0, MaxCount = 0;
            public static List<Audios> Temp_aud = new List<Audios>();
            public static List<Audios> ans = new List<Audios>();
            public static int[,] dp = new int[10000, 10000];
            public static List<Audios> MyAudios = new List<Audios>(BasicOperations.Audio_files);
            public static List<Folder> MyFolders = new List<Folder>(MyAudios.Count);
            public static void knapsack()
            {
                Folder Temp = new Folder();
                int i = 0, j = 0;
                int[,] arr = new int[MyAudios.Count + 1, BasicOperations.max_size + 1];
                for (i = 0; i <= MyAudios.Count; i++)
                {
                    for (j = 0; j <= BasicOperations.max_size; j++)
                    {
                        if (i == 0 || j == 0)
                            arr[i, j] = 0;
                        else if (MyAudios[i - 1].total_in_sec <= j)
                        {
                            arr[i, j] = Math.Max(MyAudios[i - 1].total_in_sec + arr[i - 1, j - MyAudios[i - 1].total_in_sec], arr[i - 1, j]);
                        }
                        else
                        {
                            arr[i, j] = arr[i - 1, j];
                        }
                    }
                }
                i--;
                j--;
                while (i > 0 && j > 0)
                {

                    if (arr[i, j] != arr[i - 1, j])
                    {
                        Temp.FolderAudios.Add(MyAudios[i - 1]);
                        j = j - MyAudios[i - 1].total_in_sec;
                        MyAudios.RemoveAt(i - 1);
                        i--;
                    }
                    else
                    {
                        i--;
                    }
                }
                MyFolders.Add(Temp);

            }





            public static int fill(int i)
            {
                int sol1 = 0, sol2 = 0;
                if (count == BasicOperations.max_size)
                {
                    if (count > MaxCount)
                    {
                        MaxCount = count;
                        ans = new List<Audios>(Temp_aud);
                    }
                    return 0;
                }
                if (count > BasicOperations.max_size)
                    return -1;
                if (i >= BasicOperations.num_of_rec)
                {
                    if (count > MaxCount)
                    {
                        MaxCount = count;
                        ans = new List<Audios>(Temp_aud);
                    }
                    return 0;
                }
                if (dp[i, count] != -2)
                    return dp[i, count];
                if ((count + MyAudios[i].total_in_sec) <= BasicOperations.max_size)
                {
                    Temp_aud.Add(MyAudios[i]);
                    count += MyAudios[i].total_in_sec;
                    //Task.Run(() =>
                    //{
                    //    sol1 = fill(i + 1) + count;
                    //}).Wait();
                    sol1 = fill(i + 1) + count;
                    Temp_aud.RemoveAt(Temp_aud.Count - 1);
                    count -= MyAudios[i].total_in_sec;
                }
                sol2 = fill(i + 1);
                //Task.Run(() =>
                //{
                //    sol2 = fill(i + 1);

                //}).Wait();
                return dp[i, count] = Math.Max(sol1, sol2);
            }
            public static void write(string Source, string Destination)
            {
                while (MyAudios.Count != 0)
                {
                    knapsack();
            
                }
                string destination;
                string source = Source;
                string FileName = "";
                string path = Destination;
                if (!Directory.Exists(path))
                {
                    DirectoryInfo di = Directory.CreateDirectory(path);
                }
                for (int no = 0; no < MyFolders.Count; no++)
                {
                    int size = MyFolders[no].FolderAudios.Count;
                    path = Destination;
                    path += @"\f" + (no + 1);
                    DirectoryInfo di = Directory.CreateDirectory(path);
                    FileName = Destination + @"\f" + (no + 1) + "_MetaData.txt";
                    System.IO.FileStream F = new FileStream(FileName, FileMode.Create);
                    StreamWriter w = new StreamWriter(F);
                    w.WriteLine("f" + (no + 1));
                    for (int index = 0; index < size; index++)
                    {
                        source = Source;
                        source += @"\" + MyFolders[no].FolderAudios[index].name;
                        destination = path + @"\" + MyFolders[no].FolderAudios[index].name;
                        Task.Run(() =>
                        {
                            System.IO.File.Copy(source, destination);

                        });
                        w.WriteLine(MyFolders[no].FolderAudios[index].name + " " + MyFolders[no].FolderAudios[index].hours + ":" + MyFolders[no].FolderAudios[index].min + ":" + MyFolders[no].FolderAudios[index].sec);
                        MyFolders[no].hours += MyFolders[no].FolderAudios[index].hours;
                        MyFolders[no].min += MyFolders[no].FolderAudios[index].min;
                        MyFolders[no].sec += MyFolders[no].FolderAudios[index].sec;
                    }
                    MyFolders[no].ConvertUnits();
                    w.WriteLine(MyFolders[no].hours + ":" + MyFolders[no].min + ":" + MyFolders[no].sec);
                    w.Close();
                }







                //for (int i = 0; i < 10000; i++)
                //{
                //    for (int j = 0; j < 10000; j++)
                //        dp[i, j] = -2;
                //}
                //string path = Destination;
                //if (!Directory.Exists(path))
                //{
                //    DirectoryInfo di = Directory.CreateDirectory(path);
                //}
                //int k = 1;
                //string FileName = path;
                //string source = Source;
                //string destination = "";
                //int no = 0;
                //int h = 0;
                //Folder tmp = new Folder();
                //List<Audios> temp = new List<Audios>();
                //while (BasicOperations.num_of_rec != 0)
                //{
                //    int i = 0;
                //    path = Destination;
                //    source = Source;
                //    destination = "";
                //    FileName = path;
                //    DirectoryInfo di;
                //    fill(0);
                //    no = ans.Count;
                //    temp = ans;
                //    for (int r = 0; r < 10000; r++)
                //    {
                //        for (int j = 0; j < 10000; j++)
                //            dp[r, j] = -2;
                //    }
                //    if (no == 1)
                //    {
                //        for (int num = 0; num < MyAudios.Count; num++)
                //        {

                //            tmp.FolderAudios.Add(MyAudios[num]);
                //            MyFolders.Add(tmp);
                //            //path = Destination;
                //            //FileName = path;
                //            //source = Source;
                //            //path += @"\f" + k;
                //            //di = Directory.CreateDirectory(path);
                //            //source += @"\" + MyAudios[num].name;
                //            //destination = path + @"\" + MyAudios[num].name;
                //            //System.IO.File.Copy(source, destination);
                //            //FileName += @"\f" + k + "_MetaData.txt";
                //            //System.IO.FileStream F1 = new FileStream(FileName, FileMode.Create);
                //            //StreamWriter w1 = new StreamWriter(F1);
                //            //w1.WriteLine("f" + k);
                //            //w1.WriteLine(MyAudios[num].name + " " + MyAudios[num].hours + ":" + MyAudios[num].min + ":" + MyAudios[num].sec);
                //            //w1.WriteLine(MyAudios[num].hours + ":" + MyAudios[num].min + ":" + MyAudios[num].sec);
                //            //w1.Close();
                //            //k++;
                //            //i++;

                //        }
                //        return;
                //    }
                //    else
                //    {
                //        temp = ans;
                //        tmp.FolderAudios = temp;
                //        MyFolders.Add(tmp);
                //        //path += @"\f" + k;
                //        //di = Directory.CreateDirectory(path);
                //        //int w3 = 0;
                //        //for (; i < no; i++)
                //        //{
                //        //    source = Source;
                //        //    source += @"\" + ans[w3].name;
                //        //    destination = path + @"\" + ans[w3].name;
                //        //    System.IO.File.Copy(source, destination);
                //        //    w3++;
                //        //}
                //        //FileName += @"\f" + k + "_MetaData.txt";
                //        //System.IO.FileStream F = new FileStream(FileName, FileMode.Create);
                //        //StreamWriter w = new StreamWriter(F);
                //        //w.WriteLine("f" + k);
                //        //Audios tem = new Audios();
                //        //for (int j = 0; j < no; j++)
                //        //{
                //        //    w.WriteLine(ans[j].name + " " + ans[j].hours + ":" + ans[j].min + ":" + ans[j].sec);
                //        //    tem.hours += ans[j].hours;
                //        //    tem.min += ans[j].min;
                //        //    tem.sec += ans[j].sec;
                //        //}
                //        //w.WriteLine(tem.hours + ":" + tem.min + ":" + tem.sec);
                //        //k++;
                //        //w.Close();
                //        int var = 0;
                //        for (int it = 0; it < no; it++)
                //        {
                //            var = ans[it].index - 1 - h;
                //            if (var < 0)
                //                var = 0;
                //            MyAudios.RemoveAt(var);
                //            h++;
                //        }

                //        BasicOperations.num_of_rec -= no;
                //        ans.Clear();
                //        count = 0;
                //        MaxCount = 0;
                //    }
                //}
                ////for (int i = 0; i < MyFolders.Count; i++)
                ////{
                ////    path = Destination;
                ////    path += @"\f" + (i + 1);
                ////    DirectoryInfo di = Directory.CreateDirectory(path);
                ////    FileName = Destination;
                ////    FileName += @"\f" + (i + 1) + "_MetaData.txt";
                ////    System.IO.FileStream F = new FileStream(FileName, FileMode.Create);
                ////    StreamWriter w = new StreamWriter(F);
                ////    w.WriteLine("f" + k);
                ////    for (int j = 0; j < MyFolders[i].FolderAudios.Count; j++)
                ////    {
                ////        source = Source;
                ////        source += @"\" + MyFolders[i].FolderAudios[j].name;
                ////        destination = path + @"\" + MyFolders[i].FolderAudios[j].name;
                ////        System.IO.File.Copy(source, destination);
                ////        w.WriteLine(MyFolders[i].FolderAudios[j].name + " " + MyFolders[i].FolderAudios[j].hours + ":" + MyFolders[i].FolderAudios[j].min + ":" + MyFolders[i].FolderAudios[j].sec);
                ////        MyFolders[i].hours += MyFolders[i].FolderAudios[j].hours;
                ////        MyFolders[i].min += MyFolders[i].FolderAudios[j].min;
                ////        MyFolders[i].sec += MyFolders[i].FolderAudios[j].sec;
                ////    }
                ////    w.WriteLine(MyFolders[i].hours + ":" + MyFolders[i].min + ":" + MyFolders[i].sec);
                ////    w.Close();

                ////}
            }

        }






        static public void First_fit(string Source, string Destination)
        {
            List<Audios> MyAudios = new List<Audios>(BasicOperations.Audio_files);
            List<Folder> MyFolders = new List<Folder>(MyAudios.Count);
            /// Initlaize the Prameters
            /// Sorting the Files 
            BasicOperations.SortInDecreasing(MyAudios);
            string path = Destination;
            // Adding the Frist file
            if (!Directory.Exists(path))
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
            }
            string FileName = path;
            string source = Source;
            string destination = "";
            int count = 0;
            for (int i = 0; i < BasicOperations.num_of_rec; i++)
            {
                bool test = true;
                Folder Temp_folder = new Folder();
                if (count == 0)
                {
                    Temp_folder.hours = MyAudios[i].hours;
                    Temp_folder.min = MyAudios[i].min;
                    Temp_folder.sec = MyAudios[i].sec;
                    Temp_folder.free_space = BasicOperations.max_size;
                    Temp_folder.free_space -= MyAudios[i].total_in_sec;
                    Temp_folder.FolderAudios.Add(MyAudios[i]);
                    // Satr el Folder El 5ayaly
                    MyFolders.Add(Temp_folder);
                    count++;
                }
                else
                {
                    for (int j = 0; j < count; j++)
                    {
                        if (MyFolders[j].free_space >= MyAudios[i].total_in_sec)
                        {
                            // Zy Ely Foo2
                            MyFolders[j].hours += MyAudios[i].hours;
                            MyFolders[j].min += MyAudios[i].min;
                            MyFolders[j].sec += MyAudios[i].sec;
                            MyFolders[j].free_space -= MyAudios[i].total_in_sec;
                            MyFolders[j].FolderAudios.Add(MyAudios[i]);
                            test = false;
                            break;
                        }
                    }
                    if (test)
                    {
                        // Adding in A new Folder w Zy El Frist Condition  
                        Temp_folder.hours = MyAudios[i].hours;
                        Temp_folder.min = MyAudios[i].min;
                        Temp_folder.sec = MyAudios[i].sec;
                        Temp_folder.free_space = BasicOperations.max_size;
                        Temp_folder.free_space -= MyAudios[i].total_in_sec;
                        Temp_folder.FolderAudios.Add(MyAudios[i]);
                        MyFolders.Add(Temp_folder);
                        count++;
                    }
                }
            }
            FileName = "";
            for (int no = 0; no < MyFolders.Count; no++)
            {
                int size = MyFolders[no].FolderAudios.Count;
                path = Destination;
                path += @"\f" + (no + 1);
                DirectoryInfo di = Directory.CreateDirectory(path);
                FileName = Destination + @"\f" + (no + 1) + "_MetaData.txt";
                System.IO.FileStream F = new FileStream(FileName, FileMode.Create);
                StreamWriter w = new StreamWriter(F);
                w.WriteLine("f" + (no + 1));
                for (int index = 0; index < size; index++)
                {
                    source = Source;
                    source += @"\" + MyFolders[no].FolderAudios[index].name;
                    destination = path + @"\" + MyFolders[no].FolderAudios[index].name;
                    Task.Run(() =>
                    {
                        System.IO.File.Copy(source, destination);

                    });
                    w.WriteLine(MyFolders[no].FolderAudios[index].name + " " + MyFolders[no].FolderAudios[index].hours + ":" + MyFolders[no].FolderAudios[index].min + ":" + MyFolders[no].FolderAudios[index].sec);
                }
                MyFolders[no].ConvertUnits();
                w.WriteLine(MyFolders[no].hours + ":" + MyFolders[no].min + ":" + MyFolders[no].sec);
                w.Close();
            }

        }
        

        static public void worst_fit(string Source, string Destination)
        {
            List<Audios> MyAudios = new List<Audios>(BasicOperations.Audio_files);
            List<Folder> MyFolders = new List<Folder>(MyAudios.Count);

            string path = Destination;
            if (!Directory.Exists(path))
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
            }
            string FileName = path;
            string source = Source;
            string destination = " ";
            int count = 0;
            for (int i = 0; i < BasicOperations.num_of_rec; i++)
            {
                long max = 0;
                int index = 0;
                bool test = false;
                Folder Temp_folder = new Folder();
                if (count == 0)
                {
                    Temp_folder.hours = MyAudios[i].hours;
                    Temp_folder.min = MyAudios[i].min;
                    Temp_folder.sec = MyAudios[i].sec;
                    Temp_folder.free_space = BasicOperations.max_size;
                    Temp_folder.free_space -= MyAudios[i].total_in_sec;
                    Temp_folder.FolderAudios.Add(MyAudios[i]);
                    MyFolders.Add(Temp_folder);
                    count++;
                }
                else
                {
                    for (int j = 0; j < count; j++)
                    {
                        if (MyFolders[j].free_space > max && MyFolders[j].free_space >= MyAudios[i].total_in_sec)
                        {
                            max = MyFolders[j].free_space;
                            index = j;
                            test = true;
                        }
                    }
                    if (test)
                    {
                        MyFolders[index].hours += MyAudios[i].hours;
                        MyFolders[index].min += MyAudios[i].min;
                        MyFolders[index].sec += MyAudios[i].sec;
                        MyFolders[index].free_space -= MyAudios[i].total_in_sec;
                        MyFolders[index].FolderAudios.Add(MyAudios[i]);

                    }
                    else
                    {
                        Temp_folder.hours = MyAudios[i].hours;
                        Temp_folder.min = MyAudios[i].min;
                        Temp_folder.sec = MyAudios[i].sec;
                        Temp_folder.free_space = BasicOperations.max_size;
                        Temp_folder.free_space -= MyAudios[i].total_in_sec;
                        Temp_folder.FolderAudios.Add(MyAudios[i]);
                        MyFolders.Add(Temp_folder);
                        count++;
                    }
                }

            }

            FileName = "";
            for (int no = 0; no < MyFolders.Count; no++)
            {
                int size = MyFolders[no].FolderAudios.Count;
                path = Destination;
                path += @"\f" + (no + 1);
                DirectoryInfo di = Directory.CreateDirectory(path);
                FileName = Destination + @"\f" + (no + 1) + "_MetaData.txt";
                System.IO.FileStream F = new FileStream(FileName, FileMode.Create);
                StreamWriter w = new StreamWriter(F);
                w.WriteLine("f" + (no + 1));
                for (int index = 0; index < size; index++)
                {
                    source = Source;
                    source += @"\" + MyFolders[no].FolderAudios[index].name;
                    destination = path + @"\" + MyFolders[no].FolderAudios[index].name;
                    Task.Run(() =>
                    {
                        System.IO.File.Copy(source, destination);

                    });
                    w.WriteLine(MyFolders[no].FolderAudios[index].name + " " + MyFolders[no].FolderAudios[index].hours + ":" + MyFolders[no].FolderAudios[index].min + ":" + MyFolders[no].FolderAudios[index].sec);
                }
                MyFolders[no].ConvertUnits();
                w.WriteLine(MyFolders[no].hours + ":" + MyFolders[no].min + ":" + MyFolders[no].sec);
                w.Close();
            }

        }



        static public void worst_fit_dec(string Source, string Destination)
        {
            List<Audios> MyAudios = new List<Audios>(BasicOperations.Audio_files);
            List<Folder> MyFolders = new List<Folder>(MyAudios.Count);
            BasicOperations.SortInDecreasing(MyAudios);
            string path = Destination;
            if (!Directory.Exists(path))
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
            }
            string FileName = path;
            string source = Source;
            string destination = " ";
            int count = 0;
            for (int i = 0; i < BasicOperations.num_of_rec; i++)
            {
                long max = 0;
                int index = 0;
                bool test = false;
                Folder Temp_folder = new Folder();
                if (count == 0)
                {
                    Temp_folder.hours = MyAudios[i].hours;
                    Temp_folder.min = MyAudios[i].min;
                    Temp_folder.sec = MyAudios[i].sec;
                    Temp_folder.free_space = BasicOperations.max_size;
                    Temp_folder.free_space -= MyAudios[i].total_in_sec;
                    Temp_folder.FolderAudios.Add(MyAudios[i]);
                    MyFolders.Add(Temp_folder);
                    count++;
                }
                else
                {
                    for (int j = 0; j < count; j++)
                    {
                        if (MyFolders[j].free_space > max && MyFolders[j].free_space >= MyAudios[i].total_in_sec)
                        {
                            max = MyFolders[j].free_space;
                            index = j;
                            test = true;
                        }
                    }
                    if (test)
                    {
                        MyFolders[index].hours += MyAudios[i].hours;
                        MyFolders[index].min += MyAudios[i].min;
                        MyFolders[index].sec += MyAudios[i].sec;
                        MyFolders[index].free_space -= MyAudios[i].total_in_sec;
                        MyFolders[index].FolderAudios.Add(MyAudios[i]);

                    }
                    else
                    {
                        Temp_folder.hours = MyAudios[i].hours;
                        Temp_folder.min = MyAudios[i].min;
                        Temp_folder.sec = MyAudios[i].sec;
                        Temp_folder.free_space = BasicOperations.max_size;
                        Temp_folder.free_space -= MyAudios[i].total_in_sec;
                        Temp_folder.FolderAudios.Add(MyAudios[i]);
                        MyFolders.Add(Temp_folder);
                        count++;
                    }
                }

            }

            FileName = "";
            for (int no = 0; no < MyFolders.Count; no++)
            {
                int size = MyFolders[no].FolderAudios.Count;
                path = Destination;
                path += @"\f" + (no + 1);
                DirectoryInfo di = Directory.CreateDirectory(path);
                FileName = Destination + @"\f" + (no + 1) + "_MetaData.txt";
                System.IO.FileStream F = new FileStream(FileName, FileMode.Create);
                StreamWriter w = new StreamWriter(F);
                w.WriteLine("f" + (no + 1));
                for (int index = 0; index < size; index++)
                {
                    source = Source;
                    source += @"\" + MyFolders[no].FolderAudios[index].name;
                    destination = path + @"\" + MyFolders[no].FolderAudios[index].name;
                    Task.Run(() =>
                    {
                        System.IO.File.Copy(source, destination);

                    });
                    w.WriteLine(MyFolders[no].FolderAudios[index].name + " " + MyFolders[no].FolderAudios[index].hours + ":" + MyFolders[no].FolderAudios[index].min + ":" + MyFolders[no].FolderAudios[index].sec);
                }
                MyFolders[no].ConvertUnits();
                w.WriteLine(MyFolders[no].hours + ":" + MyFolders[no].min + ":" + MyFolders[no].sec);
                w.Close();
            }

        } 


        static public class BestFit
        {
            public static List<Audios> MyAudios = new List<Audios>(BasicOperations.Audio_files);

            //intializing the folders list .. will contain the data of each folder
            public static List<Folder> BFfolders = new List<Folder>(MyAudios.Count);
            public static bool fits = false;
            static public void Best_fit()
            {
                //   BasicOperations.Initlaize("AudiosInfo.txt", "readme.txt");
                for (int i = 0; i < MyAudios.Count; i++)
                {
                    fits = false;
                    if (BFfolders.Count == 0)
                    {
                        Folder tmp = new Folder();
                        tmp.free_space = BasicOperations.max_size;
                        BFfolders.Add(tmp);
                        BFfolders[0].free_space -= MyAudios[i].total_in_sec;
                        BFfolders[0].hours += MyAudios[i].hours;
                        BFfolders[0].min += MyAudios[i].min;
                        BFfolders[0].sec += MyAudios[i].sec;
                        BFfolders[0].FolderAudios.Add(MyAudios[i]);
                        fits = true;
                    }
                    else
                    {
                        BFfolders.Sort((x, y) => x.free_space.CompareTo(y.free_space));
                        for (int j = 0; j < BFfolders.Count; j++)
                        {
                            if (BFfolders[j].free_space >= MyAudios[i].total_in_sec)
                            {
                                BFfolders[j].free_space -= MyAudios[i].total_in_sec;
                                BFfolders[j].hours += MyAudios[i].hours;
                                BFfolders[j].min += MyAudios[i].min;
                                BFfolders[j].sec += MyAudios[i].sec;
                                BFfolders[j].FolderAudios.Add(MyAudios[i]);
                                fits = true;
                                break;

                            }
                        }
                    }

                    if (fits == false)
                    {

                        Folder tmp = new Folder();
                        tmp.free_space = BasicOperations.max_size;
                        BFfolders.Add(tmp);
                        BFfolders[BFfolders.Count - 1].free_space -= MyAudios[i].total_in_sec;
                        BFfolders[BFfolders.Count - 1].hours += MyAudios[i].hours;
                        BFfolders[BFfolders.Count - 1].min += MyAudios[i].min;
                        BFfolders[BFfolders.Count - 1].sec += MyAudios[i].sec;
                        BFfolders[BFfolders.Count - 1].FolderAudios.Add(MyAudios[i]);
                        fits = true;
                    }

                }

            }



            static public void BestFitFilling(string InputFolderpath, string OutputFolderpath)
            {

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
                    //if (!Directory.Exists(folderpath))
                    //{
                    //    DirectoryInfo di3 = Directory.CreateDirectory(folderpath);
                    //}
                    string folderMetaData = bestfitpath + @"\F" + foldercount + "_METADATA.txt";
                    System.IO.FileStream F1 = new FileStream(folderMetaData, FileMode.Create);
                    StreamWriter w1 = new StreamWriter(F1);
                    w1.WriteLine("F" + foldercount);

                    for (int j = 0; j < BFfolders[i].FolderAudios.Count; j++)
                    {
                        string filepath = InputFolderpath + @"\" + BFfolders[i].FolderAudios[j].name;
                        string filetarget = folderpath + @"\" + BFfolders[i].FolderAudios[j].name;
                        Task.Run(() =>
                        {
                            System.IO.File.Copy(filepath, filetarget, true);

                        });

                  //      System.IO.File.Copy(filepath, filetarget);

                        w1.WriteLine(BFfolders[i].FolderAudios[j].name + " " + BFfolders[i].FolderAudios[j].hours + ":" + BFfolders[i].FolderAudios[j].min + ":" + BFfolders[i].FolderAudios[j].sec);


                    }
                    BFfolders[i].ConvertUnits();
                    w1.WriteLine(BFfolders[i].hours + ":" + BFfolders[i].min + ":" + BFfolders[i].sec);
                    w1.Close();



                }

                MyAudios.Clear();
            }
        }

    }
}




