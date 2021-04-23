using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerTap : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    Player playerScripti;

    private void Start() {
        playerScripti = player.GetComponent<Player>();
    }

    private void OnMouseDown() {
        Debug.Log("Beer Tap");
        playerScripti.AddBeer();
    }
}
