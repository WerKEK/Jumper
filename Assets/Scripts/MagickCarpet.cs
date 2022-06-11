﻿using UnityEngine;

public class MagickCarpet : MonoBehaviour
{
    public Transform left, right;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var leftWall = Physics2D.Raycast(left.position, Vector2.left, 0.3f);
            var rightWall = Physics2D.Raycast(right.position, Vector2.right, 0.3f);

            if (Input.GetAxis("Horizontal") > 0 && !rightWall.collider && collision.transform.position.x > transform.position.x || Input.GetAxis("Horizontal") < 0 && !leftWall.collider && collision.transform.position.x < transform.position.x)
                transform.position = new Vector3(collision.transform.position.x, transform.position.y, transform.position.z);
        }
    }
}
