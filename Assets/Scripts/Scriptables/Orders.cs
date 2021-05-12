using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "OrdersScriptable")]
public class Orders : ScriptableObject
{
    //How many items will be ordered
    public List<int> NbrOfItems = new List<int>();

    //Roll weights: at what percentual chance will the table order any number of items.
    //Remember to insert values respectively in the same order as the items above. 0-100. Total of roll weights should not exceed 100.
    public List<int> rollWeigh = new List<int>();

    //In how many seconds will an order timeout.
    public float orderTimeOutSeconds;
    private List<int> rollTable = new List<int>();

    public int RollNbrOfItems () {
        for (int i = 0; i < NbrOfItems.Count; i++) {
            for (int j = 0; j < rollWeigh[i]; j++) {
                rollTable.Add(NbrOfItems[i]);
            }
        }

        int rnd = Random.Range(0, 99);
        return rollTable[rnd];
    }
}
