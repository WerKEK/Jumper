using UnityEngine;

public class WaterGood : MonoBehaviour
{
    [SerializeField] private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 2f)
        {
            timer = 0f;
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (timer >= 1f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Player>().inWater = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Player>().inWater = false;
        }
    }
}