using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private List<Order> orders = new List<Order>();
    Player playerScript;

    private void Start() {
        playerScript = player.GetComponent<Player>();
        MakeOrder();
    }

    public void ServeBeer(string item) {
        Debug.Log("Serving beer...");
        playerScript.ServeItem("beer");
    }

    private void MakeOrder() {
        orders.Add(GenerateOrder());
        orders[0].LogOrder();
    }

    private Order GenerateOrder() {
        Order newOrder = new Order(2, 3);
        return newOrder;
    }
}
