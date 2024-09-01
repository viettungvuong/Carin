using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public Transform player;
    public Vector3 offset; 

    void Start(){
        offset = transform.position - player.position;
    }

    void LateUpdate(){
        transform.position = player.position + offset;
        transform.LookAt(player);
    }
}