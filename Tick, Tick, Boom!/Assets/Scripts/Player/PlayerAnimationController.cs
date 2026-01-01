using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Animator animator;
    AudioSource audioSource;
    PlayerController playerController;

    [SerializeField] private ParticleSystem deadParticles;
    [SerializeField] private AudioClip deadSound;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        playerController = GetComponent<PlayerController>();

        spriteRenderer.enabled = true;
    }

    private void OnEnable()
    {
        GameEventsController.PlayerDied += AnimatePlayerDead;

        PlayerEventsController.RunStarted += StartWalkAnimation;
        PlayerEventsController.RunStopped += StopWalkAnimation;

        PlayerEventsController.JumpStarted += AnimateJump;
        PlayerEventsController.FallStarted += AnimateFall;
        PlayerEventsController.Landed += EnableOnGroundAnimator;

        PlayerEventsController.WallSlideStarted += StartWallSlideAnimation;
        PlayerEventsController.WallSlideEnded += StopWallSlideAnimation;
        PlayerEventsController.WallJumpStarted += AnimateWallJump;
    }

    private void OnDisable()
    {
        GameEventsController.PlayerDied -= AnimatePlayerDead;

        PlayerEventsController.RunStarted -= StartWalkAnimation;
        PlayerEventsController.RunStopped -= StopWalkAnimation;

        PlayerEventsController.JumpStarted -= AnimateJump;
        PlayerEventsController.FallStarted -= AnimateFall;
        PlayerEventsController.Landed -= EnableOnGroundAnimator;
            
        PlayerEventsController.WallSlideStarted -= StartWallSlideAnimation;
        PlayerEventsController.WallSlideEnded -= StopWallSlideAnimation;
        PlayerEventsController.WallJumpStarted -= AnimateWallJump;
    }

    private void AnimatePlayerDead()
    {
        spriteRenderer.enabled = false;

        deadParticles.Play();

        audioSource.pitch = Random.Range(0.8f, 1.1f);
        audioSource.PlayOneShot(deadSound);

        playerController.DestroyPlayer(deadSound.length);
    }

    private void StartWalkAnimation()
    {
        animator.SetBool("IsRunning", true);
    }
    private void StopWalkAnimation()
    {
        animator.SetBool("IsRunning", false);
    }

    private void AnimateJump()
    {
        animator.SetBool("OnGround", false);
        animator.SetBool("IsJumping", true);
    }
    private void AnimateFall()
    {
        animator.SetBool("IsJumping", false);
        animator.SetBool("IsFalling", true);
    }
    private void EnableOnGroundAnimator()
    {
        animator.SetBool("OnGround", true);
    }

    private void StartWallSlideAnimation()
    {
        animator.SetBool("IsWallSliding", true);
    }
    private void StopWallSlideAnimation()
    {
        animator.SetBool("IsWallSliding", false);
    }
    private void AnimateWallJump()
    {
        animator.SetBool("IsWallSliding", false);
        animator.SetBool("IsJumping", true);
    }
}
