using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    [SerializeField] private GameObject drop;

    private static readonly int Dead = Animator.StringToHash("dead");
    private bool _isHit;
    
    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !_isHit)
        {
            collision.gameObject.GetComponent<Player>().RecountHp(-1);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 10f, ForceMode2D.Impulse);
        }
    }

    private IEnumerator Death()
    {
        if(drop != null)
        {
            Instantiate(drop, transform.position, Quaternion.identity);
        }
        _isHit = true;
        GetComponent<Animator>().SetBool(Dead, true);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Collider2D>().enabled = false;
        transform.GetChild(0).GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
    public void StartDeath()
    {
        StartCoroutine(Death());
    }
}
