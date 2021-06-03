using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private GameObject speechBubbleNumberObject;
    private Image speechBubbleNumber;
    [SerializeField]
    private List<Sprite> numberImages;
    private int happiness;
    private bool playerIsNear;
    private int startTimer;
    private int orderCounter = 0;
    private float orderTimeOut;
    private float showOrderTime;

    //Delegate
    public delegate void MoodDelegate(bool fulfilled, int happiness);
    public MoodDelegate handleMoodChange;

    private void Start() {
        playerScript = player.GetComponent<Player>();
        orderTimeOut = ordersScribtable.orderTimeOutSeconds;
        showOrderTime = ordersScribtable.showOrderTime;
        speechBubbleNumber = speechBubbleNumberObject.GetComponent<Image>();
        happiness = globals.defaultHappiness;
        //Delegate
        //handleMoodChange += AdjustMood;
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
                        playerScript.QueueServeItem(item);
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
        AdjustMood(true);
        handleMoodChange(true, happiness);
        Debug.Log("Order fulfilled! Happiness: " + happiness);
    }

    private void AdjustMood(bool fulfilled) {
        if(fulfilled) happiness++;
        else happiness--;
    }

    private Order GenerateOrder() {
        int nbrOfItems = ordersScribtable.RollNbrOfItems();
        Order newOrder = new Order(nbrOfItems, 0, orderTimeOut, "OrderTimeOut" + orderCounter);
        FunctionTimer.Create(TimeoutAction, orderTimeOut, "OrderTimeOut" + orderCounter);

        ShowOrder(nbrOfItems);
        FunctionTimer.Create(HideOrder, showOrderTime, "HideOrder");

        orderCounter++;
        return newOrder;
    }

    private void TimeoutAction() {
        orders.RemoveAt(0);
        AdjustMood(false);
        handleMoodChange(false, happiness);
        Debug.Log("order timed out " + "Happiness: " + happiness);
    }

    private void ShowOrder(int amount) {
        speechBubbleNumber.sprite = numberImages[amount - 1];
        speechBubbleNumberObject.transform.parent.gameObject.SetActive(true);
    }
    private void HideOrder() {
        speechBubbleNumberObject.transform.parent.gameObject.SetActive(false);
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
