using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private List<string> inventory = new List<string>();
    
    public void AddBeer() {
        inventory.Add("beer");
        foreach(string element in inventory) {
            Debug.Log(element);
        }
    }
    public void ServeBeer() {
        if (inventory.Contains("beer")) {
            //int temp = inventory.IndexOf("beer");
            inventory.Remove("beer");
        }
    }
}
