using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSquare : MonoBehaviour
{
    public float Speed = 2f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.position =
                Vector3.MoveTowards(transform.position, transform.position + Vector3.right, Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position =
                Vector3.MoveTowards(transform.position, transform.position + Vector3.left, Speed * Time.deltaTime);
        }
    }
}
