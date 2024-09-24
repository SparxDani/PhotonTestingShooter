using UnityEngine;


	[RequireComponent(typeof(Animator))]
	public class ThirdPersonCharacterAnimatorBase : MonoBehaviour
	{
		#region Speed
		protected float m_MovingTurnSpeed = 360;
		protected float m_StationaryTurnSpeed = 180;
		protected float m_MoveSpeedMultiplier = 1f;
		protected float m_MoveSpeedMultiplierMax = 1f;
		protected float m_AnimSpeedMultiplier = 1f;

		public float MovingTurnSpeed;
		public float StationaryTurnSpeed;

		public float MoveSpeedMultiplierMax;
		public float MoveSpeedMultiplier;
		public float AnimSpeedMultiplier;

		
		public float _MoveSpeedMultiplierMax { get => m_MoveSpeedMultiplierMax; set => m_MoveSpeedMultiplierMax = value; }
		public float _MoveSpeedMultiplier { get => _MoveSpeedMultiplier; set => m_MoveSpeedMultiplierMax = value; }
	
		#endregion

		#region MyRegion
		protected Rigidbody m_Rigidbody;
		protected Animator m_Animator { get; set; }
		public Animator Animator { get=>m_Animator; }
	#endregion


		#region MyRegion
		public bool m_IsGrounded;
		//protected float m_OrigGroundCheckDistance;
		//public float m_GroundCheckDistance;
		protected const float k_Half = 0.5f;
		protected float m_TurnAmount;
		protected float m_ForwardAmount;
		protected Vector3 m_GroundNormal;
	//protected bool m_Crouching;
	#endregion

		Vector3 oldPosition;
		float oldTime = 0;
		public float ForwardAmount { get => m_ForwardAmount; }

		//public LayerMask maskGround;
		void Start()
		{
			
		}
		public virtual void LoadComponent()
		{
			m_Animator = GetComponent<Animator>();
			
			m_Rigidbody = GetComponent<Rigidbody>();

			if(m_Rigidbody)
				m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
			
			m_MovingTurnSpeed = MovingTurnSpeed;
			m_StationaryTurnSpeed = StationaryTurnSpeed;
			m_MoveSpeedMultiplier = MoveSpeedMultiplier;
			m_AnimSpeedMultiplier = AnimSpeedMultiplier;

			oldPosition = transform.position;
			oldTime = Time.time;
		}
		public float CalculateVelocityUnity()
		{
			Vector2 currentPosition = new Vector2(transform.position.x, transform.position.z);
			Vector2 olPosition = new Vector2(oldPosition.x, oldPosition.z);
			Vector2 diffVector = currentPosition - olPosition;
			float diffTime = Time.time - oldTime;
			diffTime = (diffTime == 0) ? 1 : diffTime;

			oldPosition = transform.position;
			oldTime = Time.time;
			return  (new Vector2(diffVector.x / diffTime, diffVector.y / diffTime).magnitude);

		}
		public virtual void Move(Vector3 move, bool strategy, Vector3 dir)
		{
		
		}

		public virtual void Attack()
		{
			
			//if (m_Animator != null && !m_Animator.GetBool("Attack"))
			{
				m_Animator.SetBool("Attack", true);
			}
				
		}
		public virtual void Hit()
		{

			//if (m_Animator != null && !m_Animator.GetBool("Attack"))
			{
				m_Animator.SetBool("Hit", true);
			}

		}
		public virtual void ResetAnimator()
		{
			m_Animator.Rebind();
		}
	
		public virtual void UpdateAnimator(Vector3 move,bool strategy)
			{

			}


		public virtual void ApplyExtraTurnRotation(bool strategy, Vector3 dir)
		{
				
		}


		public Quaternion ClampQuaterion(Quaternion rot, Vector3 clamp)
		{
			rot.y = rot.y * clamp.y;
			rot.x = rot.x * clamp.x;
			rot.z = rot.z * clamp.z;
			return rot;
		}


		public virtual void CheckGroundStatus()
    {
        RaycastHit hitInfo;
        // 0.1f is a small offset to start the ray from inside the character
        // it is also good to note that the transform position in the sample assets is at the base of the character
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo))
        {
            m_GroundNormal = hitInfo.normal;
            m_IsGrounded = true;
            //if(m_Animator!=null)
            m_Animator.applyRootMotion = true;
        }
        else
        {
            m_IsGrounded = false;
            m_GroundNormal = Vector3.up;
            //if (m_Animator != null)
            m_Animator.applyRootMotion = false;
        }
    }
}
