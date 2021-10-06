using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AnimationController : MonoBehaviour
{
    private Animator playerAnimator;


    private void Start() {
     playerAnimator = GetComponentInChildren<Animator>();
    }

    public void IdleAnim()
    {
        playerAnimator.SetFloat("Speed",0,0.1f, Time.deltaTime);
    }
    public void FrontWalking()
    {
        playerAnimator.SetFloat("Speed", 0.25f, 0.1f, Time.deltaTime);
    }
    public void BackwardWalking()
    {
        playerAnimator.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }

    public void LeftWalking()
    {
        playerAnimator.SetFloat("Speed", 0.75f, 0.1f, Time.deltaTime);
    }
    public void RightWalking()
    {
        playerAnimator.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
    }
}
