using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerAnimations : MonoBehaviour
{
    [Header("Refernces")]
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerMovment movment;
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private Transform graphicsTransform;
    [Space(20)]
    [Header("Parametes")]
    [SerializeField] private string _isMovingParamater;
    [SerializeField] private string _isIsJumpingParamater;
    [SerializeField] private string _isIsFallpingParamater;

    private void Update()
    {
        UpdateAnimations();
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        if (movment.GetCurrentInputX() > 0)
        {
            graphicsTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (movment.GetCurrentInputX() < 0)
        {
            graphicsTransform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }

        



private void UpdateAnimations()
    {
        animator.SetBool(_isMovingParamater, movment.isMoving());

        if (playerRigidbody.velocity.y > 0.1f)
        {
            animator.SetBool(_isIsJumpingParamater, true);
            animator.SetBool(_isIsFallpingParamater, false);
        }
        else if (playerRigidbody.velocity.y < -0.1f)
        {
            animator.SetBool(_isIsJumpingParamater, false);
            animator.SetBool(_isIsFallpingParamater, true);
        }
        else
        {
            animator.SetBool(_isIsJumpingParamater, false);
            animator.SetBool(_isIsFallpingParamater, false);
        }
    }
}
