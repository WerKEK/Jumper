using System.Collections;
using UnityEngine;

public class Beetle : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private float waitTime = 2f;
    [SerializeField] private Transform point;
    private bool _isWait;
    private bool _isHidden;
    
    private void Start()
    {
        var position = transform.position;
        point.position = new Vector3(position.x, position.y + 1f, position.z);
    }
    private void Update()
    {
        var position = transform.position;
        if (_isWait == false)
            position = Vector3.MoveTowards(position, point.position, speed*Time.deltaTime);

        if(transform.position == point.position)
        {
            if (_isHidden)
            {
                point.position = new Vector3(position.x, position.y + 1f, position.z);
                _isHidden = false;
            }
            else
            {
                point.position = new Vector3(position.x, position.y - 1f, position.z);
                _isHidden = true;
            }
            _isWait = true;
            StartCoroutine(Waiting());
        }
        
        IEnumerator Waiting()
        {
            yield return new WaitForSeconds(waitTime);
            _isWait = false;
        }
    }
}
