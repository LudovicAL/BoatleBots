using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerActions : MonoBehaviour {

	public Player player;

	public float m_RabbitVerticalOffset = 0.0f;
	private Vector3 m_MostRecentGoodRabbitPosition = new Vector3(0.0f, 0.0f, -1.0f);
	public GameObject m_Rabbit;
	public float m_RabbitTurningForce;
	public float m_MaxSpeed;
	public float m_AccelerationSpeed;
	public float m_DeccelerationSpeed;
	public Rigidbody selfRigidbody;
	private float m_Speed;
	private bool m_AreSailsUp;

	public PlayerAttackingLeft playerAttackingLeft = new PlayerAttackingLeft();
    public PlayerAttackingRight playerAttackingRight = new PlayerAttackingRight();

    [System.Serializable]
	public class PlayerAttackingLeft : UnityEvent<PlayerId> { }

    [System.Serializable]
    public class PlayerAttackingRight : UnityEvent<PlayerId> { }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (player.playerId.controls.GetRBumperDown())
        {
            ShootSalvoRight();
        }
        if (player.playerId.controls.GetLBumperDown())
        {
            ShootSalvoLeft();
        }
    }

	private void FixedUpdate() {
		Debug.DrawLine(player.physicPlatform.transform.position, player.physicPlatform.transform.position + (player.physicPlatform.transform.forward * 3.0f));

		Vector3 localOffset = new Vector3(player.playerId.controls.GetLHorizontal(), 0.0f, player.playerId.controls.GetLVertical());
		if (localOffset.magnitude > 0.25f) {
			SetTheRabbit(localOffset);
			m_AreSailsUp = true;
		} else {
			SetTheRabbit(m_MostRecentGoodRabbitPosition);
			m_AreSailsUp = false;
		}

		//Move foward
		if (m_AreSailsUp) {
			m_Speed += (Time.fixedDeltaTime * m_AccelerationSpeed);
			m_Speed = Mathf.Clamp(m_Speed, 0.0f, m_MaxSpeed);
		} else {
			m_Speed -= (Time.fixedDeltaTime * m_DeccelerationSpeed);
			m_Speed = Mathf.Max(m_Speed, 0.0f);
		}
		PushBoatForwards();

		RotateToRabbit();
	}

	public void PushBoatForwards() {
		player.buoyancy.ForwardThrust(m_Speed);
	}

	private void SetTheRabbit(Vector3 localOffset) {
		m_MostRecentGoodRabbitPosition = localOffset;
		m_Rabbit.transform.position = (localOffset.normalized * 2.0f) + player.physicPlatform.transform.position + (Vector3.up * m_RabbitVerticalOffset);
	}

	private void RotateToRabbit() {
		//Rabbit vector
		Vector3 rabbitVector = (m_Rabbit.transform.position - (Vector3.up * m_RabbitVerticalOffset)) - player.physicPlatform.transform.position;
		rabbitVector.y = 0.0f;

		Vector3 frontVector = player.physicPlatform.transform.forward;
		frontVector.y = 0.0f;

		Vector3 rightVector = player.physicPlatform.transform.right;
		rightVector.y = 0.0f;

		float theta = Vector3.Dot(rightVector.normalized, rabbitVector.normalized);
		float angle = 1.0f + (Mathf.Acos(Vector3.Dot(frontVector.normalized, rabbitVector.normalized)) / Mathf.PI);
		//Debug.Log("Boat Angle " + angle);
		if (theta > 0.0) {
			selfRigidbody.AddTorque(Vector3.up * m_RabbitTurningForce * Mathf.Abs(angle));
		} else {
			selfRigidbody.AddTorque(Vector3.up * -m_RabbitTurningForce * Mathf.Abs(angle));
		}
	}

	public void ShootSalvoRight() {
		playerAttackingRight.Invoke(player.playerId);
	}

    public void ShootSalvoLeft() {
        playerAttackingLeft.Invoke(player.playerId);
    }
}
