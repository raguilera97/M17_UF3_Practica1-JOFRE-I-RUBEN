using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimapa : MonoBehaviour
{
    public Transform target;

    private void LateUpdate()
    {
        Vector3 newPosition = target.position;

        newPosition.y = transform.position.y;

        transform.position = newPosition;
    }
}
