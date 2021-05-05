using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerTap : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    Player playerScripti;

    private bool playerIsNear;

    private void Start() {
        playerScripti = player.GetComponent<Player>();
    }

    private void OnMouseDown() {
        if (playerIsNear) {
            Debug.Log("Beer Tap");
            playerScripti.AddItem("beer");
        } else {
            Debug.Log("Player is not by " + this.gameObject.name);
        }
    }

    private void OnTriggerEnter(Collider target) {
        if (target.tag == "Player") {
            playerIsNear = true;
        }
    }

    private void OnTriggerExit(Collider target) {
        if (target.tag == "Player") {
            playerIsNear = false;
        }
    }
}
