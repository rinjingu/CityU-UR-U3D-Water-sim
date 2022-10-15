using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainGUI : MonoBehaviour
{
    public GameObject tabGUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tabGUI.SetActive(Input.GetKey(KeyCode.Tab));


    }
}
