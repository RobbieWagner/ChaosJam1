using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;

    void Update()
    {
        //transform.position = new Vector3(player.position.x, player.position.y, -10);
        transform.position = new Vector3(0, player.position.y, -10);
    }
}
