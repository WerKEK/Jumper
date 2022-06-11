using UnityEngine;

public class groundPatrol : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private bool moveLeft = true;
    [SerializeField] private Transform groundDetect;

    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        var groundInfo = Physics2D.Raycast(groundDetect.position,Vector2.down, 1f);

        if (groundInfo.collider != false) return;
        if(moveLeft)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            moveLeft = false; 
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            moveLeft = true;
        }
    }
}
