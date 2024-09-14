using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform player, car;
    
    void LateUpdate() {
        Vector3 follow;
        float eulerAngles;
        if (Mode.mode==TypeMode.WALKING){
            follow = player.position;
            eulerAngles = player.eulerAngles.y;
        }
        else{
            follow = car.position;
            eulerAngles = car.eulerAngles.y;
        }
        Vector3 newPosition = follow;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        transform.rotation = Quaternion.Euler(90f, eulerAngles, 0f);
    }
}
