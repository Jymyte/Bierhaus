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

    [SerializeField]
    private List<GameObject> inventoryIcons = new List<GameObject>();
    private List<string> inventory = new List<string>();
    private int inventroySize;

    private void Start() {
        nma = ecm_agent.GetComponent<NavMeshAgent>();
        inventroySize = globals.inventorySize;
    }
    
    public void AddItem(string item) {
        if  (inventory.Count < inventroySize) {
            inventory.Add(item);

            //Function call for updating the inventory hud.

            foreach(string element in inventory) {
                Debug.Log(element);
            }
        } else {
            Debug.Log("Inventory full");
        }
        updateInventoryHUD();
    }
    public void ServeItem(string item) {
        inventory.Remove(item);
        updateInventoryHUD();
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
