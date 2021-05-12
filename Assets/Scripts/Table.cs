using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    public Globals globals;
    public Orders ordersScribtable;
    [SerializeField]
    private GameObject player;
    Player playerScript;
    [SerializeField]
    private List<Order> orders = new List<Order>();
    [SerializeField]
    private int happiness = 2;
    private bool playerIsNear;
    private int startTimer;
    private int orderCounter = 0;
    private float orderTimeOut;

    private void Start() {
        playerScript = player.GetComponent<Player>();
        orderTimeOut = ordersScribtable.orderTimeOutSeconds;
    }

    public void ServeItem(string item) {
        if (playerIsNear) {
            Debug.Log("Serving " + item + "...");
            playerScript.StopPlayerMovement();

            if (orders.Count > 0) {
                int temp = orders[0].GetAmountOfBeer();
                Debug.Log(orders[0].GetAmountOfBeer() + "food: " + orders[0].GetAmountOfFood());

                if (temp > 0) {
                    if (playerScript.hasItem(item)) {
                        playerScript.ServeItem(item);
                        temp--;
                        orders[0].SetAmountOfBeer(temp);
                        IsOrderFulfilled();
                    } else Debug.Log(item + " not in inventory");
                } 
            } else {
                Debug.Log("No active order");
            }
        } else {
            Debug.Log("Player is not by " + this.gameObject.name);
        }
    }

    public void MakeOrder() {
        orders.Add(GenerateOrder());
        orders[0].LogOrder();
        Debug.Log("Orders list size: " + orders.Count);
    }

    private void IsOrderFulfilled() {
        if (orders != null && orders[0].GetAmountOfBeer() == 0 && orders[0].GetAmountOfFood() == 0) FulfillOrder(0); else Debug.Log("Order not yet fulfilled.");
    }

    private void FulfillOrder(int orderIndex) {
        FunctionTimer.StopTimer(orders[orderIndex].GetOrderName());
        orders.RemoveAt(orderIndex);
        happiness++;
        Debug.Log("Order fulfilled! Happiness: " + happiness);
    }

    private Order GenerateOrder() {
        Order newOrder = new Order(ordersScribtable.RollNbrOfItems(), 0, orderTimeOut, "OrderTimeOut" + orderCounter);
        FunctionTimer.Create(TimeoutAction, orderTimeOut, "OrderTimeOut" + orderCounter);
        orderCounter++;
        return newOrder;
    }

    private void TimeoutAction() {
        orders.RemoveAt(0);
        happiness--;
        Debug.Log("order timed out");
    }

    private void OnTriggerEnter(Collider target) {
        if (target.tag == "Player") {
            playerIsNear = true;
        }
    }

    private void OnTriggerExit(Collider target) {
        if (target.tag == "Player") {
            playerIsNear = false;
        }
    }

    private void SetStartTimer(int time) {
        startTimer = time;
    }
}
