using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingProtester : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_walkDirection = Vector3.right;

    [SerializeField]
    private float m_walkStrength = 1;

    [SerializeField]
    private float m_maxDeviation = 30;

    [SerializeField]
    private float m_maxSpeed = 10;

    [SerializeField]
    private float m_minSpeed = 20;

    [SerializeField]
    private float m_turnSpeed = 1;
    [SerializeField]
    private float m_acceleration = 1;

    [SerializeField]
    private float m_angle = 0;

    [SerializeField]
    private float m_speed = 0;

    [SerializeField]
    private float m_targetAngle = 0;

    [SerializeField]
    private float m_targetSpeed = 0;

    [SerializeField]
    private float m_turnRatio = 0.01f;

    [SerializeField]
    private float m_roadSize = 10;

    [SerializeField]
    private float m_outOfRoadStrength = 60;

    [SerializeField]
    Animation[] m_animations;

    private Rigidbody m_rigidbody;

	void Start ()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }
	
	void FixedUpdate ()
    {
        Quaternion targetRotation = Quaternion.LookRotation(m_walkDirection);
        if (m_targetAngle > m_angle)
            m_angle += Time.fixedDeltaTime * m_turnSpeed;
        if (m_targetAngle < m_angle)
            m_angle -= Time.fixedDeltaTime * m_turnSpeed;

        if (m_targetSpeed > m_speed)
            m_speed += Time.fixedDeltaTime * m_acceleration;
        if (m_targetSpeed < m_speed)
            m_speed -= Time.fixedDeltaTime * m_acceleration;

        if (Random.Range(0f, 1f) < m_turnRatio)
            m_targetAngle = Random.Range(-m_maxDeviation, m_maxDeviation);
        if (Random.Range(0f, 1f) < m_turnRatio)
            m_targetSpeed = Random.Range(m_minSpeed, m_maxSpeed);

        if (transform.position.z < -m_roadSize / 2)
        {
            m_angle -= m_outOfRoadStrength * Time.fixedDeltaTime;
            m_targetAngle -= m_outOfRoadStrength * Time.fixedDeltaTime;
        }
        if (transform.position.z > m_roadSize / 2)
        {
            m_angle += m_outOfRoadStrength * Time.fixedDeltaTime;
            m_targetAngle += m_outOfRoadStrength * Time.fixedDeltaTime;
        }
        if (m_angle < -m_maxDeviation)
            m_angle = -m_maxDeviation;
        if (m_angle > m_maxDeviation)
            m_angle = m_maxDeviation;
        Quaternion direction = Quaternion.Euler(0, m_angle, 0);
        m_rigidbody.AddForce(direction * Vector3.right * m_speed);

    }
}
