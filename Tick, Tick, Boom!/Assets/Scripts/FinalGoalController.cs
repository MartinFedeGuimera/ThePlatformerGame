using UnityEngine;

public class FinalGoalController : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerLayer.Contains(collision.gameObject.layer))
        {
            animator.SetBool("LevelFinished", true);
            GameEventsController.InvokeLevelFinished();
        }
    }
}
