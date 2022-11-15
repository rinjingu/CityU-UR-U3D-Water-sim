using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using SharpConfig;

public class profileIO : MonoBehaviour
{
    public Configuration cfg = new Configuration();

    //create file if not exist, and then load the file into memory
    public void findOrBuild(string fName)
    {
        if (!File.Exists("./" + fName))
        {
            Debug.Log("File does not exist! \nCreating new one...");
            cfg.SaveToFile("./" + fName);
        }

        cfg = Configuration.LoadFromFile("./" + fName);
    }

    //save the file in memory with given name
    public void saveCFG(string fName){
        Debug.Log("Saving File as" + fName);
        cfg.SaveToFile("./" + fName);
    }

    public T getFromCFG<T>(string section, string config)
    {
        return cfg[section][config].GetValue<T>();
    }

    public void setToCFG<T>(string section, string config, T value)
    {
        cfg[section][config].SetValue(value);
    }


}
