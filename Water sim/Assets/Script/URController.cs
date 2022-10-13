using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class URController : MonoBehaviour
{
    Vector3 inVector = new Vector3();
    Vector3 velocity = new Vector3();
    Rigidbody m_Rigidbody;

    [SerializeField]
    float m_Speed = 3.0f;

    [SerializeField]
    float m_RotSpeed = 2.1f;

    [SerializeField]
    float m_liftingSpeed = 1.0f;

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
