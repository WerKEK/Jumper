using UnityEngine;

public class BG : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    [SerializeField] private float parallaxEffect;
    private float length, startpos;
    
    private void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    private void FixedUpdate()
    {
        var position = cam.transform.position;
        var temp = position.x * (1 - parallaxEffect);
        var dist = position.x * parallaxEffect;
        
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        if (temp > startpos + length)
            startpos += length;
        else if (temp < startpos - length)
            startpos -= length;
    }
}
