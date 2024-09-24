using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCharacterAnimatorZombie : ThirdPersonCharacterAnimatorBase
{
	public float heightOffset;
	void Start()
	{
		this.LoadComponent();
	}
	public override void LoadComponent()
	{
		base.LoadComponent();
	}

	public override void Move(Vector3 move, bool strategy, Vector3 dir)
	{
		// convert the world relative moveInput vector into a local-relative
		// turn amount and forward amount required to head in the desired
		// direction.
		if (move.magnitude > 1f) move.Normalize();
		move = transform.InverseTransformDirection(move);
		//this.CheckGroundStatus();
		//move = Vector3.ProjectOnPlane(move, m_GroundNormal);
		m_TurnAmount = Mathf.Atan2(move.x, move.z);
		m_ForwardAmount = move.z;

		this.ApplyExtraTurnRotation(strategy, dir);

		// send input and other state parameters to the animator
		this.UpdateAnimator(move, strategy);

	}


	public override void UpdateAnimator(Vector3 move, bool strategy)
	{

		
		{
			// update the animator parameters
			m_Animator.SetFloat("Speed", Mathf.Clamp(move.z, 0, 1), 0.1f, Time.deltaTime);
			m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
		}



		//// which affects the movement speed because of the root motion.
		if (/*m_IsGrounded && */move.magnitude > 0)
		{
			m_Animator.speed = m_AnimSpeedMultiplier;
		}
		else
		{
			// don't use that while airborne
			m_Animator.speed = 1;
		}
	}
	public override void ApplyExtraTurnRotation(bool strategy, Vector3 dir)
	{
		if (!strategy)
		{
			float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
			transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
		}
		else
		{
			Quaternion rot = Quaternion.LookRotation(dir, Vector3.up);
			rot = ClampQuaterion(rot, new Vector3(0, 1, 0));
			transform.rotation = Quaternion.LerpUnclamped(transform.rotation, rot, Time.deltaTime * 5f);
		}
	}


	public void OnAnimatorMove()
	{
		//Vector3 v = m_Rigidbody.velocity;
		//v.y = agent.nextPosition.y - heightOffset;
		//m_Rigidbody.velocity = v;
		if (Time.deltaTime > 0)
		{
			Vector3 v = (m_Animator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;
			v.y = m_Rigidbody.velocity.y;
			m_Rigidbody.velocity = v;
		}
	}
}
