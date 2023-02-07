using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///Controller of the controllable object in the simulator.
///</summary>
///<remarks>
///Edit this class and attach methods from other classes into this class in order to change movement method.
///</remarks>
public class URController : MonoBehaviour
{
    private Vector3 inVector = new Vector3();
    public Vector3 velocity = new Vector3();
    private Rigidbody m_Rigidbody;
    public float m_Speed;
    public float m_RotSpeed;
    public float m_liftingSpeed;

    // Start is called before the first frame update
    void Start()
    {
        inVector = new Vector3(0.0f, 0.0f, 0.0f);
        velocity = new Vector3(0.0f, 0.0f, 0.0f);
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        getDirection();
        UpdateMovement();
    }

    // Wait for improvement
    // Read input keynodes to change object's rotation
    void getDirection()
    {
        float m_rot = 0.0f;
        if (Input.GetKey(KeyCode.D))
        {
            m_rot = 1.0f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_rot = -1.0f;
        }

        inVector = new Vector3(0.0f, m_rot * 15f, 0.0f);
        Quaternion m_Rotation = Quaternion.Euler(inVector * Time.fixedDeltaTime * m_RotSpeed);
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * m_Rotation);
    }

    // Wait for improvement
    // Read input keynodes to change object's position
    void UpdateMovement()
    {
        float m_acc = 0.0f;
        float m_lift = 0.0f;

        if (Input.GetKey(KeyCode.W))
        {
            m_acc = 1.0f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_acc = -1.0f;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            m_lift = 1.0f;
        }
        if (Input.GetKey(KeyCode.C))
        {
            m_lift = -1.0f;
        }

        velocity = new Vector3(
            m_acc * m_Speed * (float)Math.Cos(m_Rigidbody.rotation.eulerAngles.y * Math.PI / 180),
            m_lift * m_liftingSpeed,
            -1
                * m_acc
                * m_Speed
                * (float)Math.Sin(m_Rigidbody.rotation.eulerAngles.y * Math.PI / 180)
        );
        m_Rigidbody.MovePosition(m_Rigidbody.position + velocity * Time.fixedDeltaTime);
    }
}
