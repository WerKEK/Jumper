using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float timeToDisable = 10f;

    private void Start()
    {
        StartCoroutine(SetDisabled());
    }
    private void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }
    private IEnumerator SetDisabled()
    {
        yield return new WaitForSeconds(timeToDisable);
        gameObject.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        StopCoroutine(SetDisabled());
        gameObject.SetActive(false);
    }
}
