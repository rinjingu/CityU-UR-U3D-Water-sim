using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///Introduce hydrodynamic in the simulator
///</summary>
///<remarks>
///WIP
///</remarks>
public class Floating : MonoBehaviour
{
    Rigidbody m_Rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() { }

    //private void OnCollisionEnter(Collision other) { }
}
