using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class displayGUI : MonoBehaviour
{
    private bool toggleBool = false;
    private int infoDisplayInt = 0;
    private string[] infoDisplayStrings = { "None", "UR Info", "Scene Info" };

    public GameObject proxy_1;
    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    private void OnGUI()
    {
        toggleBool = GUI.Toggle(new Rect(25, 25, 100, 30), toggleBool, "Show Info");

        if (toggleBool)
        {
            GUI.BeginGroup(new Rect(25, 55, 320, 90));
            GUI.Box(new Rect(0, 0, 320, 90), "");
            GUI.Label(new Rect(10, 0, 300, 30), "Page : " + infoDisplayStrings[infoDisplayInt]);

            infoDisplayInt = GUI.SelectionGrid(
                new Rect(10, 30, 300, 30),
                infoDisplayInt,
                infoDisplayStrings,
                3
            );

            GUI.Label(new Rect(10, 60, 300, 30), infoDisplay(infoDisplayInt));
            GUI.EndGroup();
        }
    }

    private string infoDisplay(int index){
        string info = "";
        switch (index)
        {
            case 1:{
                Transform m_transform = GetComponent<Transform>();
                info += "Position : " + m_transform.position.x + "/" + m_transform.position.y + "/" + m_transform.position.z + "\n";
                info += "Rotation : " + m_transform.rotation.x + "/" + m_transform.rotation.y + "/" + m_transform.rotation.z + "\n";
                return info;
            }
            case 2:{
                Transform p1_transform = proxy_1.GetComponent<Transform>();
                info += "Proxy 1 name : " + proxy_1.name + "\n";
                info += "Proxy 1 position : " + p1_transform.position.x + "/" + p1_transform.position.y + "/" + p1_transform.position.z + "\n";
                return info;
            }
            default:
                return info;
        }
    }
}
