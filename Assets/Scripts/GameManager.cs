using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Globals globals;
    [SerializeField]
    private List<GameObject> tables = new List<GameObject>();
    private Table tableScript;

    private void Start() {
        InitializeTables();
        foreach(GameObject table in tables) {
            Debug.Log(table.name);
        }
    }
    private void InitializeTables() {
        RollTableOrder();
    }

    private void RollTableOrder() {
        for (int i = 0; i < tables.Count; i++) {
         GameObject temp = tables[i];
         int randomIndex = Random.Range(i, tables.Count);
         tables[i] = tables[randomIndex];
         tables[randomIndex] = temp;
     }
    }
}
