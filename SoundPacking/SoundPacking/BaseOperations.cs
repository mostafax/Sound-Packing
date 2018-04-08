using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// A class containing the description of a file
/// </summary>
class FileDescription
{
    public string Name;
    public int Duration;
}

/// <summary>
/// A class containing the description of a folder
/// </summary>
class FolderDescription
{
    public string Name;
    public int MaxLength, CurrentLength;
    public bool CheckLength(int length)
    {
        if (MaxLength - CurrentLength >= length)
            return true;
        return false;
    }
}
/// <summary>
/// The Record and Its Details
/// </summary>
public class Records
{
    public int hourse, min, sec;
    public long free_space;
};

/// <summary>
/// The Aduios and Its Details
/// </summary>
public class Audoios
{
    public string name;
    public int hourse, min, sec, total_in_sec;

    /// <summary>
    /// The class that contains the lowest level of code to be utilized by higher classes
    /// </summary>
}

    static class BaseOperations
    {
        //private
        static private List<FileDescription> files;
        static private List<FolderDescription> folders;
        //public
        /// <summary>
        /// A list containing description of target files
        /// </summary>
        static public List<FileDescription> Files
        {
            get
            {
                return files;
            }
        }
        /// <summary>
        /// A list containing description of target folders
        /// </summary>
        static public List<FolderDescription> Folders
        {
            get
            {
                return folders;
            }
        }
        /// <summary>
        /// initlaize the two lists (file,folder)
        /// </summary>
        static public void Initlaize()
        {

            //Reading From AudiosInfo 
            FileStream fs = new FileStream("AudiosInfo.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);
             //Reading the Number of the Records 
            string num_of_records = sr.ReadLine();
            int num_of_rec = (int)num_of_records[0] - '0';
            string[] records = new string[num_of_rec];
            Audoios[] aud = new Audoios[num_of_rec];

            for (int i = 0; i < num_of_rec; i++)
            {
                records = sr.ReadLine().Split(' ');
                aud[i].name = records[0];
                string[] temp = records[1].Split(':');
                aud[i].hourse = int.Parse(temp[0]);
                aud[i].min = int.Parse(temp[1]);
                aud[i].sec = int.Parse(temp[2]);
                //Seting all Values to Seconds
                aud[i].total_in_sec = aud[i].hourse * 3600 + aud[i].min * 60 + aud[i].sec;
                Console.WriteLine(aud[i].total_in_sec);
            }


            sr.Close();
        //Reading the Number of Audio of the File
        //Reading the Max Lenght of the Floder
        FileStream f = new FileStream("readme.txt", FileMode.Open);
            StreamReader s = new StreamReader(f);
            s.ReadLine();
            string[] Temp = new string[2];
            string[] size = new string[2];
            Temp = s.ReadLine().Split('=');
            size = Temp[1].Split(' ');
            s.Close();

            int size_of_rec = int.Parse(size[1]);
            Records[] folders = new Records[num_of_rec];
            for (int i = 0; i < num_of_rec; i++)
            {
                folders[i].free_space = size_of_rec;
            }
    

        /// <summary>
        /// Create Once folder OUTPUT 
        /// </summary>
  
        // Specify the directory you want to manipulate.
        // Always Change it on yor PC
        string path = @"C:\Users\Mostafax\Documents\GitHub\Sound-Packing-\SoundPacking\SoundPacking\bin\Debug\OUTPUT";

        // Try to create the directory OUTPUT File Once.
        DirectoryInfo di = Directory.CreateDirectory(path);

         }
    /// <summary>
    /// Create a new folder 
    /// </summary>

    /// <returns>
    /// The newly created folder
    /// </returns>
    static int i=1;
    static public FolderDescription ConstructFolder()
        {

        // Specify the directory you want to manipulate.
        // Always Change it on yor PC
        //Creating Folder With Numbers
        string path = @"C:\Users\Mostafax\Documents\GitHub\Sound-Packing-\SoundPacking\SoundPacking\bin\Debug\OUTPUT\"+i;
        i++;
        // Try to create the directory.
        DirectoryInfo di = Directory.CreateDirectory(path);
        FolderDescription f = new FolderDescription()
        {
            Name = i.ToString(),
            MaxLength = 100, //Max Space in One Folder
            CurrentLength = 0,
        };
        folders.Add(f);
        // Returning the folder
        return f;
        
       
        }
        /// <summary>
        /// move the files to the folder
        /// </summary>
        /// <param name="SouceFile">
        /// A FileDescription object containing the description of the source file that is meant to be moved
        /// </param>
        /// <param name="DestinationFolder">
        /// A FolderDescription object containing the description of the target folder that the file will be moved to
        /// </param>
        static public void MoveFile(FileDescription SouceFile, FolderDescription DestinationFolder)
        {


            throw new NotImplementedException();
        }
    }

