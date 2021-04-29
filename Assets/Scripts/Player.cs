using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private List<string> inventory = new List<string>();
    
    public void AddItem(string item) {
        if  (inventory.Count < 5) {
            inventory.Add(item);

            //Function call for updating the inventory hud.

            foreach(string element in inventory) {
                Debug.Log(element);
            }
        } else {
            Debug.Log("Inventory full");
        }
    }
    public void ServeItem(string item) {
        inventory.Remove(item);
    }

    public bool hasItem(string item) {
        if (inventory.Contains(item)) return true; else return false;
    }
}
