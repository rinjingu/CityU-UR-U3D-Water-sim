using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using SharpConfig;

///<summary>
///Acting as an interface for reading config files in form of both .ini, .cfg and etc.
///</summary>
public class profileIO
{
    ///<value>
    ///The loaded config
    ///</value>
    public Configuration cfg = new Configuration();

    ///<summary>
    ///Create file if not exist, and then load the file into memory
    ///</summary>
    ///<param name="fName">The name of the file to be loaded, accept path</param>
    ///<returns></returns>
    public void findOrBuild(string fName)
    {
        Debug.Log("Finding file with path: " + fName);
        if (!File.Exists(fName))
        {
            Debug.Log("File does not exist! \nCreating new one...");
            saveCFG(fName);
        }else
        {
            Debug.Log("File found.");
        }

        cfg = Configuration.LoadFromFile(fName);
    }

    ///<summary>
    ///Save the config in memory as a file with given name, will override the existing one.
    ///</summary>
    ///<param name="fName">The name of the file to be saved, accept path.</param>
    ///<returns></returns>
    public void saveCFG(string fName)
    {
        Debug.Log("Saving File as " + fName + " at " + GetFilePath("", fName));
        cfg.SaveToFile(fName);
    }

    ///<summary>
    ///Get the value from the config 
    ///</summary>
    ///<remarks>
    ///Return value requires a To-String method and a From-String method
    ///</remarks>
    ///<param name="section">The name of the section</param>
    ///<param name="config">The name of the item in the given section</param>
    ///<returns></returns>
    public T getFromCFG<T>(string section, string config)
    {
        return cfg[section][config].GetValue<T>();
    }

    ///<summary>
    ///Set the value to the config 
    ///</summary>
    ///<remarks>
    ///The value to be set requires a To-String method and a From-String method
    ///</remarks>
    ///<param name="section">The name of the section</param>
    ///<param name="config">The name of the item in the given section</param>
    ///<param name="value">The value to be set, see remarks</param>
    ///<returns></returns>
    public void setToCFG<T>(string section, string config, T value)
    {
        cfg[section][config].SetValue(value);
    }

    /// <summary>
    /// Create file path for where a file is stored on the specific platform given a folder name and file name
    /// </summary>
    /// <param name="FolderName"></param>
    /// <param name="FileName"></param>
    /// <returns></returns>
    private static string GetFilePath(string FolderName, string FileName = "")
    {
        string filePath;
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
        // mac
        filePath = Path.Combine(Application.streamingAssetsPath, ("data/" + FolderName));

        if (FileName != "")
            filePath = Path.Combine(filePath, (FileName));
#elif UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        // windows
        filePath = Path.Combine(Application.persistentDataPath, ("data/" + FolderName));

        if(FileName != "")
            filePath = Path.Combine(filePath, (FileName));
#elif UNITY_ANDROID
        // android
        filePath = Path.Combine(Application.persistentDataPath, ("data/" + FolderName));

        if(FileName != "")
            filePath = Path.Combine(filePath, (FileName));
#elif UNITY_IOS
        // ios
        filePath = Path.Combine(Application.persistentDataPath, ("data/" + FolderName));

        if(FileName != "")
            filePath = Path.Combine(filePath, (FileName));
#endif
        return filePath;
    }
}
