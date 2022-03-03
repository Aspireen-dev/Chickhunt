using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformRotation : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.up * 45 * Time.deltaTime);
    }
}
