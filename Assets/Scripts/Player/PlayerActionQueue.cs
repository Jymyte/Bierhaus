using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionQueue : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private Player playerScript;
    public Queue<string> playerActions = new Queue<string>();

    [SerializeField]
    private Globals globals;

    void Start() {
        playerScript = player.GetComponent<Player>();
        StartCoroutine(CoroutineCoordinator());
    }

    IEnumerator CoroutineCoordinator() {
        while (true) {
            while (playerActions.Count > 0) {
                yield return StartCoroutine(playerActions.Dequeue());
            }
            yield return null;
        }
    }

    public IEnumerator ServeBeer() {
        playerScript.anim.SetBool("isServing", true);
        playerScript.FreezePlayer(true);
        yield return new WaitForSeconds(globals.serveActionTime);
        if (playerActions.Count == 0) {
            playerScript.anim.SetBool("isServing", false);
            playerScript.FreezePlayer(false);
        }
        playerScript.updateInventoryHUD(false);
    }

    public IEnumerator GetBeer() {
        playerScript.anim.SetBool("isTaking", true);
        playerScript.FreezePlayer(true);
        yield return new WaitForSeconds(globals.addItemActionTime);
        if (playerActions.Count == 0) {
            playerScript.anim.SetBool("isTaking", false);
            playerScript.FreezePlayer(false);
        }
        playerScript.updateInventoryHUD(true);
    }
}
