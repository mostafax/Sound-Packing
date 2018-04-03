using System;
using System.Collections.Generic;

class FileDescription
{
    public string Name;
    public int Duration;
}

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

static class BaseOperations
{
    //private
    static private List<FileDescription> files;
    static private List<FolderDescription> folders;
    //public
    static public List<FileDescription> Files
    {
        get
        {
            return files;
        }
    }
    static public List<FolderDescription> Folders
    {
        get
        {
            return folders;
        }
    }
    static public void Initlaize()
    {
        throw new NotImplementedException();
    }
    static public int ConstructFolder()
    {
        throw new NotImplementedException();
    }
    static public void MoveFile(FileDescription SouceFile, FolderDescription DestinationFolder)
    {
        throw new NotImplementedException();
    }
}