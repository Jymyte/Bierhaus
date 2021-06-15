using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public Globals globals;

    [SerializeField]
    private GameObject ecm_agent;
    NavMeshAgent nma;
    MyCharacterController myCharacterController;

    [SerializeField]
    private GameObject actionQueue;
    private PlayerActionQueue queue;

    //Animation stuff
    public Animator anim;

    [SerializeField]
    private List<GameObject> inventoryIcons = new List<GameObject>();
    private List<string> inventory = new List<string>();
    private int inventorySize;
    private int itemsInInventory = 0;

    private void Start() {
        nma = ecm_agent.GetComponent<NavMeshAgent>();
        myCharacterController = ecm_agent.GetComponent<MyCharacterController>();
        inventorySize = globals.inventorySize;
        queue = actionQueue.GetComponent<PlayerActionQueue>();

        anim = gameObject.GetComponent<Animator>();
    }
    
    public void QueueAddItem(string item) {
        if  (inventory.Count < inventorySize) {
            queue.playerActions.Enqueue("GetBeer");
            inventory.Add(item);
        } else {
            Debug.Log("Inventory full");
        }
    }

    public void QueueServeItem(string item) {
        queue.playerActions.Enqueue("ServeBeer");
        inventory.Remove(item);
    }

    public void FreezePlayer(bool isFrozen) {
        myCharacterController.setIsBusy(isFrozen);
    }

    public void StopPlayerMovement() {
        nma.isStopped = true;
        nma.ResetPath();
    }
    public bool hasItem(string item) {
        if (inventory.Contains(item)) return true; else return false;
    }
    public void updateInventoryHUD(bool increase) {
        Debug.Log("before:" + itemsInInventory);
        if (increase) itemsInInventory++; else itemsInInventory--;
        Debug.Log("after:" + itemsInInventory);
        foreach (GameObject icon in inventoryIcons) {
            icon.SetActive(false);
        }
        for (int i = 0; i < itemsInInventory; i++) {
            inventoryIcons[i].SetActive(true);
        } 
    }
}
