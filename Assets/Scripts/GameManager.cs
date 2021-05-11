using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Globals globals;
    [SerializeField]
    private List<GameObject> tables = new List<GameObject>();
    [SerializeField]
    private List<Table> tableScripts = new List<Table>();
    private int tableToHandle = 0;

    private void Start() {
        GetTableScripts();
        InitializeTables();
        StartTimers();

        foreach(GameObject table in tables) {
            Debug.Log(table.name);
        }
    }

    private void GetTableScripts() {
        foreach(GameObject table in tables) {
            tableScripts.Add(table.GetComponent<Table>());
        }
    }

    private void InitializeTables() {
        RollTableOrder();
    }

    private void AnotherRound() {
        Debug.Log("Another round!");
        tableToHandle = 0;
        RollTableOrder();
        StartTimers();
    }

    private void RollTableOrder() {
        for (int i = 0; i < tables.Count; i++) {
            GameObject temp = tables[i];
            int randomIndex = Random.Range(i, tables.Count);
            tables[i] = tables[randomIndex];
            tables[randomIndex] = temp;
        }
    }

    private void StartTimers() {
        float timer = globals.timeInBetweenOrder;

        foreach (GameObject table in tables) {
            FunctionTimer.Create(TimerAction, timer, "Timer");
            timer += globals.timeInBetweenOrder;
        }
    }

    private void TimerAction() {
        tableScripts[tableToHandle].MakeOrder();
        Debug.Log("timer test " + tables[tableToHandle].name);
        tableToHandle++;
        
        if (tableToHandle >= tables.Count) AnotherRound(); 
    }
}
