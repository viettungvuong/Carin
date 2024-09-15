using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RefillType{
    Energy,
    Health,
    Bullet,
    Money
}
public class RefillButton : MonoBehaviour
{
    GameObject player;
    public RefillType refillType;

    private Player playerComponent;
    private PlayerAttack playerAttack;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        playerComponent = player.GetComponent<Player>();
        playerAttack = player.GetComponent<PlayerAttack>();
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")){ // only refill when not in car
            // refill respectively
            switch (refillType){
                case RefillType.Energy:
                    playerComponent.RefillEnergy(10);
                    break;
                case RefillType.Health:
                    playerComponent.RefillHealth(10);
                    break;
                case RefillType.Bullet:
                    playerAttack.RefillBullets(10);
                    break;
                case RefillType.Money:
                    playerComponent.AddMoney(50);
                    break;
            }

            this.gameObject.SetActive(false);
        }
    }
}
