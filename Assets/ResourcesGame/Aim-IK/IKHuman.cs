using AimIK.Functions;
using AimIK.Properties;
using UnityEngine;

[ExecuteInEditMode]
public class IKHuman : IKBase
{
    public Transform LeftHand;
    public Transform RightHand;
    public Transform LeftElbow; // Nuevo Transform para el codo izquierdo
    public Transform RightElbow; // Nuevo Transform para el codo derecho
    [Range(0.01f, 0.5f)] public float radiusLeftHand;
    [Range(0.01f, 0.5f)] public float radiusRightHand;
    [Range(0.01f, 0.5f)] public float radiusTarget;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    [ExecuteInEditMode]
    private void LateUpdate()
    {
        anim.Update(0);
        if (IKActive)
        {
            foreach (Part item in IKPart)
            {
                Vector3 IKPosition = (target.position - item.positionOffset);
                item.part.LookAt3D(IKPosition, item.rotationOffset);
                item.part.CheckClamp3D(item.limitRotation, item.GetRotation());
            }
        }
    }

    [ExecuteInEditMode]
    private void OnAnimatorIK(int layerIndex)
    {
        if (IKActive)
        {
            // Control de las manos
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
            anim.SetIKRotation(AvatarIKGoal.LeftHand, LeftHand.rotation);

            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
            anim.SetIKPosition(AvatarIKGoal.LeftHand, LeftHand.position);

            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
            anim.SetIKRotation(AvatarIKGoal.RightHand, RightHand.rotation);

            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
            anim.SetIKPosition(AvatarIKGoal.RightHand, RightHand.position);

            // Control de los codos
            if (LeftElbow)
            {
                anim.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, 1f);
                anim.SetIKHintPosition(AvatarIKHint.LeftElbow, LeftElbow.position);
            }

            if (RightElbow)
            {
                anim.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 1f);
                anim.SetIKHintPosition(AvatarIKHint.RightElbow, RightElbow.position);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (LeftHand)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(LeftHand.position, radiusLeftHand);
        }
        if (RightHand)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(RightHand.position, radiusRightHand);
        }

        if (target)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(target.position, radiusTarget);
        }

        // Dibujar gizmos para los codos
        if (LeftElbow)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(LeftElbow.position, 0.1f); // Ajusta el tamaño si es necesario
        }
        if (RightElbow)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(RightElbow.position, 0.1f); // Ajusta el tamaño si es necesario
        }
    }
}
