using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trophy : MonoBehaviour
{
    [SerializeField] private Vector3 rotationDirection;
    [SerializeField] private float speed;

    private void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        transform.Rotate(rotationDirection * speed * Time.deltaTime);
    }
}
