using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private List<string> inventory = new List<string>();
    
    private void AddBeer() {
        inventory.Add("beer");
    }
}
