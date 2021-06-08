using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order
{
    private int amountOfBeer;
    private int amountOfFood;
    private float orderTimeout;
    private string orderName;


    public Order(int beer, int food, float timeout, string name) {
        amountOfBeer = beer;
        amountOfFood = food;
        orderTimeout = timeout;
        orderName = name;
    }

    /* public void LogOrder() {
        Debug.Log("Beer: " + amountOfBeer + " Food: " + amountOfFood);
    } */

    public int GetAmountOfBeer() {
        return amountOfBeer;
    }

    public void SetAmountOfBeer(int newAmount) {
        amountOfBeer = newAmount;
    }
    public int GetAmountOfFood() {
        return amountOfFood;
    }
    public void SetAmountOfFood(int newAmount) {
        amountOfFood = newAmount;
    }
    public string GetOrderName() {
        return orderName;
    }
}
