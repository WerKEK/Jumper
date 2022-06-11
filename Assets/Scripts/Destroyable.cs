using UnityEngine;

public class Destroyable : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 10f, ForceMode2D.Impulse);
            gameObject.GetComponentInParent<Enemy>().StartDeath();
        }
    }
}
