using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tabPage : MonoBehaviour
{
    public Text thisText;
    public GameObject MainUR;

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
        textTrans.anchoredPosition = new Vector2(20 + (textTrans.rect.width)/2, -20 - (textTrans.rect.height)/2);

        //Info of UR
        Transform UR_transform = MainUR.GetComponent<Transform>();
        thisText.text += "Position : " + UR_transform.position;
        nextLine(5);

        //Info of Scene Preset
        //To-do
        thisText.text += "Scene Info";
        nextLine();
        thisText.text += "test";
    }

    void nextLine(int line = 1){
        for(int i = 0; i < line; i++){
            thisText.text += "\n";
        }
    }
}
