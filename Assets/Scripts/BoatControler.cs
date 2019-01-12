using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatControler : MonoBehaviour {

	public Player player;
	private Rigidbody selfRigidbody;

    public float m_RabbitVerticalOffset = 0.0f;
    private Vector3 m_MostRecentGoodRabbitPosition = new Vector3(0.0f, 0.0f, -1.0f);
    private bool m_AreSailsUp;
    public GameObject m_Rabbit;
    public float m_RabbitTurningForce;
    public float m_MaxSpeed;
    private float m_Speed;
    public float m_AccelerationSpeed;
    public float m_DeccelerationSpeed;

    void Awake () {
		selfRigidbody = this.GetComponent<Rigidbody> ();
	}

	// Use this for initialization
	void Start () {
    }

	// Update is called once per frame
	void FixedUpdate () {
		Debug.DrawLine(transform.position, transform.position + (transform.forward * 3.0f));

		Vector3 localOffset = new Vector3(player.playerId.controls.GetLHorizontal(), 0.0f, player.playerId.controls.GetLVertical());
        if (localOffset.magnitude > 0.25f)
        {
            SetTheRabbit(localOffset);
            m_AreSailsUp = true;
        }
        else
        {
            SetTheRabbit(m_MostRecentGoodRabbitPosition);
            m_AreSailsUp = false;
        }

        //Move foward
        if (m_AreSailsUp)
        {
            m_Speed += (Time.fixedDeltaTime * m_AccelerationSpeed);
            m_Speed = Mathf.Clamp(m_Speed, 0.0f, m_MaxSpeed);
		}
        else
        {
            m_Speed -= (Time.fixedDeltaTime * m_DeccelerationSpeed);
            m_Speed = Mathf.Max(m_Speed, 0.0f);
        }

        PushBoatForwards();

        RotateToRabbit();
	}

	//Moves the boat foward
	public void PushBoatForwards(){
        gameObject.GetComponent<Buoyancy>().ForwardThrust(m_Speed);
	}

    public void SetTheRabbit(Vector3 localOffset)
    {
        m_MostRecentGoodRabbitPosition = localOffset;
        m_Rabbit.transform.position = (localOffset.normalized * 2.0f) + transform.position + (Vector3.up * m_RabbitVerticalOffset);
    }

    public void RotateToRabbit()
    {
        //Rabbit vector
        Vector3 rabbitVector = (m_Rabbit.transform.position - (Vector3.up * m_RabbitVerticalOffset)) - selfRigidbody.gameObject.transform.position;
        rabbitVector.y = 0.0f;

        Vector3 frontVector = selfRigidbody.gameObject.transform.forward;
        frontVector.y = 0.0f;

        Vector3 rightVector = selfRigidbody.gameObject.transform.right;
        rightVector.y = 0.0f;

        float theta = Vector3.Dot(rightVector.normalized, rabbitVector.normalized);
        float angle = 1.0f + (Mathf.Acos(Vector3.Dot(frontVector.normalized, rabbitVector.normalized)) / Mathf.PI);
        //Debug.Log("Boat Angle " + angle);
        if (theta > 0.0)
        {
            selfRigidbody.AddTorque(Vector3.up * m_RabbitTurningForce * Mathf.Abs(angle));
        }
        else
        {
            selfRigidbody.AddTorque(Vector3.up * -m_RabbitTurningForce * Mathf.Abs(angle));
        }
    }
}
