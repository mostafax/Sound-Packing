using System;
using System.Collections.Generic;

/// <summary>
/// A class encabsulating all fitting algorithms
/// </summary>
static class FittingOperations
{
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
            BaseOperations.Initlaize();
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



