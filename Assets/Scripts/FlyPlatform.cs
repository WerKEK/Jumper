using UnityEngine;

public class FlyPlatform : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private float speed = 1f;
    [SerializeField] private int i = 1;

    private void Start()
    {
        transform.position = new Vector3(points[0].position.x, points[0].position.y, transform.position.z);   
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            var posX = transform.position.x;
            var posY = transform.position.y;
            transform.position = Vector3.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
            var pos = collision.gameObject.transform.position;
            pos = new Vector3(pos.x + transform.position.x - posX, pos.y + transform.position.y - posY, pos.z);
            
            if (transform.position == points[i].position)
            {
                if (i < points.Length - 1)
                    i++;
                else
                    i = 0;
            }
        }
    }
}










