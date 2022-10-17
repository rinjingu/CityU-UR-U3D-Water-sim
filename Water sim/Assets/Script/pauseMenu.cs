using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenu : MonoBehaviour
{
    public GameObject subPanel; 
    // Start is called before the first frame update
    void Start()
    {
        RectTransform textTrans = subPanel.GetComponent<RectTransform>();
        textTrans.anchoredPosition = new Vector2(20 + (textTrans.rect.width)/3, 0);
    }

    // Update is called once per frame
    void Update()
    {
        RectTransform textTrans = subPanel.GetComponent<RectTransform>();
        textTrans.anchoredPosition = new Vector2(20 + (textTrans.rect.width)/3, 0);
    }

    
}
