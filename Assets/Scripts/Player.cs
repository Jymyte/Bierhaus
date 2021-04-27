using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private List<string> inventory = new List<string>();
    
    public void AddItem(string item) {
        if  (inventory.Count < 5) {
            inventory.Add(item);

            foreach(string element in inventory) {
                Debug.Log(element);
            }
        } else {
            Debug.Log("Inventory full");
        }
    }
    public void ServeItem(string item) {
        if (inventory.Contains(item)) {
            //int temp = inventory.IndexOf("beer");
            inventory.Remove(item);
        } else {
            Debug.Log(item + " not in inventory");
        }
    }
}
