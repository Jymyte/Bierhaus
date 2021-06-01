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

    [SerializeField]
    private List<GameObject> inventoryIcons = new List<GameObject>();
    private List<string> inventory = new List<string>();
    private int inventorySize;

    private void Start() {
        nma = ecm_agent.GetComponent<NavMeshAgent>();
        myCharacterController = ecm_agent.GetComponent<MyCharacterController>();
        inventorySize = globals.inventorySize;
        queue = actionQueue.GetComponent<PlayerActionQueue>();
    }
    
    public void QueueAddItem(string item) {
        if  (inventory.Count < inventorySize) {
            queue.playerActions.Enqueue("GetBeer");
            inventory.Add(item);
            updateInventoryHUD();            
        } else {
            Debug.Log("Inventory full");
        }
    }

    public void QueueServeItem(string item) {
        queue.playerActions.Enqueue("ServeBeer");
        inventory.Remove(item);
        updateInventoryHUD();
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
    private void updateInventoryHUD() {
        foreach (GameObject icon in inventoryIcons) {
            icon.SetActive(false);
        }
        for (int i = 0; i < inventory.Count; i++) {
            inventoryIcons[i].SetActive(true);
        } 
    }
}