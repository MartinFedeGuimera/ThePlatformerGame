using UnityEngine;

public class JumperController : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float impulse = 10f;

    private bool canImpulse = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerLayer.Contains(collision.gameObject.layer) && canImpulse)
        {
            canImpulse = false;

            Rigidbody2D playerBody = collision.GetComponent<Rigidbody2D>();

            playerBody.velocity = new Vector2(playerBody.velocity.x, 0f); // opcional, para que sea consistente
            playerBody.AddForce(Vector2.up * impulse, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (playerLayer.Contains(collision.gameObject.layer))
        {
            canImpulse = true;
        }
    }
}

