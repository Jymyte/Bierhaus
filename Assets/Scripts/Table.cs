using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    Player playerScript;

    private void Start() {
        playerScript = player.GetComponent<Player>();
    }

    public void ServeBeerIf() {
        Debug.Log("Serving beer...");
        playerScript.ServeBeer();
    }
}
