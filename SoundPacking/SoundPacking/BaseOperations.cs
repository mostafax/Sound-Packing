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
public class Folder
{
    public int hours, min, sec;
    public long free_space;
    /// <summary>
    /// used in best fit 
    /// </summary>
    public List<Audios> FolderAudios = new List<Audios>();
};

/// <summary>
/// The Aduios and Its Details
/// </summary>
public class Audios
{
    public string name;
    public int hours, min, sec, total_in_sec, index;

    /// <summary>
    /// The class that contains the lowest level of code to be utilized by higher classes
    /// </summary>
}

static class BaseOperations
{
    //private
    static private List<FileDescription> files;
    static private List<FolderDescription> folders;
    static public List<Audios> Audio_files = new List<Audios>();
    static public List<Folder> Audio_Folders = new List<Folder>();
    static public int max_size = 0;
    static public int num_of_rec = 0;

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
    /// initlaize the two lists (Audio_files,Folders)
    /// </summary>
    /// 
    public static void Initlaize(string FileName1, string FileName2)
    {
        //Reading From AudiosInfo ..
        FileStream fs = new FileStream(FileName1, FileMode.Open);
        StreamReader sr = new StreamReader(fs);
        //Reading the first line in the File which contain the Nember of Audios Files..
        string num_of_records = sr.ReadLine();
        //Converting the string value into int by using TypeCasting..
        num_of_rec = (int)num_of_records[0] - '0';
        //Makeing Array of string to hold the Data from File to split it..
        string[] records = new string[num_of_rec];
        //Makeing a Temp Variable to hold the Data and push it in the List..
        //Loop to Get the Data and split it..
        for (int i = 0; i < num_of_rec; i++)
        {
            Audios aud = new Audios();
            records = sr.ReadLine().Split(' ');
            aud.name = records[0];
            string[] temp = records[1].Split(':');
            aud.hours = int.Parse(temp[0]);
            aud.min = int.Parse(temp[1]);
            aud.sec = int.Parse(temp[2]);
            aud.total_in_sec = aud.hours * 3600 + aud.min * 60 + aud.sec;
            aud.index = i + 1;
            Audio_files.Add(aud);
        }

        //Closing the Folder after Geting the Data..
        sr.Close();
        //Reading from Readme to konw the Max_Size of Folder..
        FileStream f = new FileStream(FileName2, FileMode.Open);
        StreamReader s = new StreamReader(f);
        s.ReadLine();
        string[] Temp = new string[2];
        string[] size = new string[2];
        Temp = s.ReadLine().Split('=');
        size = Temp[1].Split(' ');
        //Closing the File after Reading from it..
        s.Close();
        ////Converting the string value into int by using TypeCasting..
        max_size = int.Parse(size[1]);


    }
    public static void SortInIncreasing()
    {
        Audio_files.Sort((x, y) => x.total_in_sec.CompareTo(y.total_in_sec));
    }

    public static void SortInDecreasing()
    {
        Audio_files.Sort((x, y) => y.total_in_sec.CompareTo(x.total_in_sec));
    }
    /// <summary>
    /// Create Once folder OUTPUT 
    /// </summary>

    // Specify the directory you want to manipulate.
    // Always Change it on yor PC
    // string path = @"C:\Users\Mostafax\Documents\GitHub\Sound-Packing-\SoundPacking\SoundPacking\bin\Debug\OUTPUT";

    // Try to create the directory OUTPUT File Once.
    //DirectoryInfo di = Directory.CreateDirectory(path);


    /// <summary>
    /// Create a new folder 
    /// </summary>

    /// <returns>
    /// The newly created folder
    /// </returns>
    static int i = 1;
    static public FolderDescription ConstructFolder()
    {

        // Specify the directory you want to manipulate.
        // Always Change it on yor PC
        //Creating Folder With Numbers
        string path = @"C:\Users\Mostafax\Documents\GitHub\Sound-Packing-\SoundPacking\SoundPacking\bin\Debug\OUTPUT\" + i;
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

