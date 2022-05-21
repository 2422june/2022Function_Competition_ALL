using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    Vector3 pos;

    void Update()
    {
        pos = transform.position;
        pos.x = player.transform.position.x;
        transform.position = pos;
    }
}
