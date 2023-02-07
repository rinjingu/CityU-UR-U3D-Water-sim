using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///The class defines param of a prop object in the simulator and provides methods to cast param
///</summary>
public class Prop
{
    //private static GameObject[] publicPropsList = GameObject.Find("startupPage").GetComponent<sceneGeneration>().publicPropsList;
    ///<summary>ID of the prop's Prefab, -1 represent the prop is empty. Detail ID reference see Documentation</summary>
    public int PrefabID;
    public float[] position;
    public float[] rotation;

    public Prop()
    {
        PrefabID = -1;
        position = new float[] { 0, 0, 0 };
        rotation = new float[] { 0, 0, 0 };
    }

    public Vector3 getPositionVector()
    {
        return new Vector3(position[0], position[1], position[2]);
    }

    public Vector3 getRotationVector()
    {
        return new Vector3(rotation[0], rotation[1], rotation[2]);
    }

    /*
    public i getPrefab(){
        return publicPropsList[PrefabID];
    }
    */
}