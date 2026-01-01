using System.Collections;
using UnityEngine;

public class GoalController : MonoBehaviour
{ 

    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private float levelCameraSize;
    [SerializeField] private Vector3 levelCameraPosition;
    [SerializeField] private Vector3 timeTextPosition;

    [SerializeField] private AudioClip closeSound;

    private Animator animator;
    private Collider2D goalCollider;
    private AudioSource audioSource;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        goalCollider = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerLayer.Contains(collision.gameObject.layer))
        {
            animator.SetBool("RoomFinished", true);

            audioSource.pitch = Random.Range(0.8f, 1.1f);
            audioSource.PlayOneShot(closeSound);

            StartCoroutine(CollisionCooldown());
            
            GameEventsController.InvokeRoomFinished(levelCameraPosition, levelCameraSize, timeTextPosition);
        }
    }

    private IEnumerator CollisionCooldown()
    {
        yield return new WaitForSeconds(0.2f);

        goalCollider.offset = Vector3.zero;
        goalCollider.isTrigger = false;
    }
}
