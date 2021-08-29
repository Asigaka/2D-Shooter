using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    [Range(0, 10)]
    [SerializeField] private float smoothFactor;

    private void FixedUpdate()
    {
        Vector3 targetPos = target.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, targetPos, smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothPos;
    }
}
