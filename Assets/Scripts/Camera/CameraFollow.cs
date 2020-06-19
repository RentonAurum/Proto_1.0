using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform Player;

    private Vector3 _OffsetCamera;

    [Range(0.01f, 1.0f)]
    public float Smooth = 0.5f;

    void Start()
    {
        _OffsetCamera = transform.position - Player.position;
    }

    void LateUpdate()
    {
        Vector3 lookPos = Player.position + _OffsetCamera;
        transform.position = Vector3.Slerp(transform.position, lookPos, Smooth);
    }
}
