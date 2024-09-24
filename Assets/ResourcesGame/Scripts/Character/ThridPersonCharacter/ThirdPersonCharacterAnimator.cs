using System.Collections;
using System.Collections.Generic;
using UnityEngine;


	
public class ThirdPersonCharacterAnimator : ThirdPersonCharacterAnimatorBase
{
	Vector3 forawrdOld;
	void Awake()
	{
		this.LoadComponent();
	}
	public override void LoadComponent()
	{
		base.LoadComponent();
	}

	public override void Move(Vector3 move, bool strategy, Vector3 dir)
	{
	
		if (move.magnitude > 1f) move.Normalize();
		CheckGroundStatus();
        move = transform.InverseTransformDirection(move);
       // move = Vector3.ProjectOnPlane(move, m_GroundNormal);
        m_TurnAmount = Mathf.Atan2(move.x, move.z);
		m_ForwardAmount = Mathf.Clamp( move.z,-1f,1f);
		//Debug.Log("move: " + move);
		ApplyExtraTurnRotation(strategy, dir);

		// send input and other state parameters to the animator
		UpdateAnimator(move, strategy);
	}




	public override void UpdateAnimator(Vector3 move,bool strategy)
	{
        m_Animator.SetFloat("Strafe", Mathf.Clamp(move.x, -1, 1), 0.1f, Time.deltaTime);
        // update the animator parameters
        m_Animator.SetFloat("Forward", Mathf.Clamp(move.z, -1, 1), 0.1f, Time.deltaTime);
		
		// the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
		// which affects the movement speed because of the root motion.
		if (m_IsGrounded && move.magnitude > 0)
		{
			m_Animator.speed = m_AnimSpeedMultiplier;
		}
		else
		{
			// don't use that while airborne
			m_Animator.speed = 1;
		}
	}

	public override  void ApplyExtraTurnRotation(bool strategy,Vector3 DirCamera)
	{
		// help the character turn faster (this is in addition to root rotation in the animation)

        if ((m_Animator != null) && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            Quaternion rot = Quaternion.LookRotation(DirCamera, Vector3.up);
            rot = ClampQuaterion(rot, new Vector3(0, 1, 0));
			
            transform.rotation = Quaternion.LerpUnclamped(transform.rotation, rot, Time.deltaTime * m_MovingTurnSpeed);
			float angle = Vector3.SignedAngle(transform.forward, forawrdOld,Vector3.up);
			forawrdOld = transform.forward;
			
			m_Animator.SetFloat("Turn", Mathf.Clamp(angle, -1, 1), 0.1f, Time.deltaTime);

		}
	}
	

	public void OnAnimatorMove()
	{
		// we implement this function to override the default root motion.
		// this allows us to modify the positional speed before it's applied.
		if (/*m_IsGrounded && */Time.deltaTime > 0)
		{
			Vector3 v = (m_Animator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;

			// we preserve the existing y part of the current velocity.
			v.y = m_Rigidbody.velocity.y;
			m_Rigidbody.velocity = v;
		}
	}


	
}


