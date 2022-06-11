using System.Collections;
using UnityEngine;

public class airPatrol : MonoBehaviour
{
    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float waitTime = 2f;
    private bool _canGo = true;
    
    private void Start()
    {
        var position = point1.position;
        gameObject.transform.position = new Vector3(position.x, position.y, transform.position.z);
    }

    private void Update()
    {
        if (_canGo)
            transform.position = Vector3.MoveTowards(transform.position, point1.position, speed * Time.deltaTime);
        if (transform.position == point1.position)
        {
            (point1, point2) = (point2, point1);
            _canGo = false;
            StartCoroutine(Waiting());
        }
    }
    private IEnumerator Waiting()
    {
        yield return new WaitForSeconds(waitTime);
        transform.eulerAngles = transform.rotation.y == 0 ? new Vector3(0, 180, 0) : new Vector3(0, 0, 0);
        _canGo = true;
    }
}
