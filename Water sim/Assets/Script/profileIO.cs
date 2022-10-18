using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using SharpConfig;

public class profileIO : MonoBehaviour
{
    [SerializeField]
    private GameObject MainUR;

    //define the default setting
    private Vector3 initialPosition = new Vector3(0f, 1.5f, 0f);
    private Vector3 initialRotation = new Vector3(0f, 0f, 0f);


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void findOrBuild()
    {

    }

    public void buildScene()
    {
        Rigidbody m_Rigidbody = MainUR.GetComponent<Rigidbody>();

        m_Rigidbody.position = initialPosition;
        Quaternion m_Rotation = Quaternion.Euler(initialRotation);
        m_Rigidbody.rotation = m_Rotation;
    }
}
