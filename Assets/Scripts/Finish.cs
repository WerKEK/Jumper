using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private Sprite finSprite;
    [SerializeField] private Main main;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().sprite = finSprite;
            main.Win();
        }
    }
}
