using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    private const float Speed = 3f;

    private void Start()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }
    private void FixedUpdate()
    {
        Vector3 position = target.position;
        position.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, position, Speed * Time.deltaTime);
    }
}
