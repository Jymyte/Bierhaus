using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    Player playerScript;
    private List<Order> orders = new List<Order>();
    private int happiness = 2;

    private void Start() {
        playerScript = player.GetComponent<Player>();
        MakeOrder();
    }

    public void ServeItem(string item) {
        Debug.Log("Serving " + item + "...");

        if (orders.Count > 0) {
            int temp = orders[0].GetAmoutOfBeer();
            Debug.Log(orders[0].GetAmoutOfBeer() + "food: " + orders[0].GetAmountOfFood());

            if (temp > 0) {
                if (playerScript.hasItem(item)) {
                    playerScript.ServeItem(item);
                    temp--;
                    orders[0].SetAmoutOfBeer(temp);
                    IsOrderFulfilled();
                } else Debug.Log(item + " not in inventory");
            } 
        } else {
            Debug.Log("No active order");
        }
    }

    private void MakeOrder() {
        orders.Add(GenerateOrder());
        orders[0].LogOrder();
    }

    private void IsOrderFulfilled() {
        if (orders != null && orders[0].GetAmoutOfBeer() == 0 && orders[0].GetAmountOfFood() == 0) FulfillOrder(0); else Debug.Log("Order not yet fulfilled.");
    }
    private void FulfillOrder(int orderIndex) {
        orders.RemoveAt(orderIndex);
        happiness++;
        Debug.Log("Order fulfilled! Happiness: " + happiness);
    }

    private Order GenerateOrder() {
        Order newOrder = new Order(2, 0);
        return newOrder;
    }
}
