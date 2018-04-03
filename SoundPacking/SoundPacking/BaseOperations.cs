using System;
using System.Collections.Generic;

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
/// The class that contains the lowest level of code to be utilized by higher classes
/// </summary>
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
        throw new NotImplementedException();
    }
    /// <summary>
    /// Create a new folder 
    /// </summary>
    /// <returns>
    /// The newly created folder
    /// </returns>
    static public FolderDescription ConstructFolder()
    {
        throw new NotImplementedException();
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