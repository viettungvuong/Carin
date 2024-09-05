using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RefillType{
    Energy,
    Health,
    Bullet
}
public class RefillButton : MonoBehaviour
{
    public RefillType refillType;
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")){
            // refill respectively
            switch (refillType){
                case RefillType.Energy:
                    other.gameObject.GetComponent<Player>().RefillEnergy(10);
                    break;
                case RefillType.Health:
                    other.gameObject.GetComponent<Player>().RefillHealth(10);
                    break;
                case RefillType.Bullet:
                    other.gameObject.GetComponent<PlayerAttack>().RefillBullets(10);
                    break;
            }
        }
    }
}
