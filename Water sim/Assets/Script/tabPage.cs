using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tabPage : MonoBehaviour
{
    [SerializeField]
    private Text thisText;
    [SerializeField]
    private GameObject MainUR;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //clear the text content
        thisText.text = "";

        RectTransform textTrans = GetComponent<RectTransform>();
        textTrans.anchoredPosition = new Vector2(20 + (textTrans.rect.width) / 2, -20 - (textTrans.rect.height) / 2);

        //Info of UR
        Transform UR_transform = MainUR.GetComponent<Transform>();
        thisText.text += "Position : " + UR_transform.position;
        nextLine();
        thisText.text += "Rotation : " + UR_transform.rotation.eulerAngles;
        nextLine();
        Vector3 thisVelocity = MainUR.GetComponent<URController>().velocity;
        thisText.text += "Linear Velocity : " + Math.Round(Math.Pow((Math.Pow(thisVelocity.x, 2) + Math.Pow(thisVelocity.y, 2) + Math.Pow(thisVelocity.z, 2)), 0.5), 3) + " m/s   /   " + thisVelocity;
        nextLine(5);

        //Info of Scene Preset
        //To be done
        thisText.text += "Scene Info";
        nextLine();
        thisText.text += "test";
        nextLine(5);

        //To be added
    }

    void nextLine(int line = 1)
    {
        for (int i = 0; i < line; i++)
        {
            thisText.text += "\n";
        }
    }
}
