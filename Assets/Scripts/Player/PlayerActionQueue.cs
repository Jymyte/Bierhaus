using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionQueue : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private Player playerScript;
    public Queue<string> playerActions = new Queue<string>();

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
        playerScript.FreezePlayer(true);
        yield return new WaitForSeconds(0.5f);
        if (playerActions.Count == 0) playerScript.FreezePlayer(false);
    }
}
