using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    private int AmountOfBeer;
    private int AmountOfFood;
    
    public Order(int beer, int food) {
        AmountOfBeer = beer;
        AmountOfFood = food;
    }

    public void LogOrder() {
        Debug.Log("Beer: " + AmountOfBeer + " Food: " + AmountOfFood);
    }
}
