using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order
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

    public int GetAmoutOfBeer() {
        return AmountOfBeer;
    }

    public void SetAmoutOfBeer(int newAmount) {
        AmountOfBeer = newAmount;
    }
    public int GetAmountOfFood() {
        return AmountOfFood;
    }
    public void SetAmountOfFood(int newAmount) {
        AmountOfFood = newAmount;
    }
}
