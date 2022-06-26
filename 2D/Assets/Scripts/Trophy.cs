using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trophy : MonoBehaviour
{
    public Vector3 rotationDirection;
    public float speed;

    private void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        transform.Rotate(rotationDirection * speed * Time.deltaTime);
    }
}
