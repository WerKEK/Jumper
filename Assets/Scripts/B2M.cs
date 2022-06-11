using UnityEngine;

public class B2M : MonoBehaviour
{
    [SerializeField] private GameObject[] blocks;
    [SerializeField] private Sprite btnDown;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("MarkBox"))
        {
            GetComponent<SpriteRenderer>().sprite = btnDown;
            GetComponent<CircleCollider2D>().enabled = false;
             
            foreach(var obj in blocks)
            {
                Destroy(obj);
            }
        }
    }
}
