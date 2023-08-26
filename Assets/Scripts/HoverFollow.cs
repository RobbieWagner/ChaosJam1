using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform follow;

    private void Update()
    {
        transform.position = follow.position + offset;
    }
}
