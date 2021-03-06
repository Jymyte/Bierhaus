using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Globals globals;
    public Orders ordersScriptable;
    [SerializeField]
    private List<GameObject> tables = new List<GameObject>();
    private List<Table> tableScripts = new List<Table>();
    private int tableToHandle = 0;
    float timer;

    private void Start() {
        timer = ordersScriptable.timeInBetweenOrder;
        
        GetTableScripts();
        InitializeTables();
        StartTimers();
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
    
    private void GetTableScripts() {
        foreach(GameObject table in tables) {
            tableScripts.Add(table.GetComponent<Table>());
        }
    }

    private void RollTableOrder() {
        for (int i = 0; i < tableScripts.Count; i++) {
            Table temp = tableScripts[i];
            int randomIndex = Random.Range(i, tableScripts.Count);
            tableScripts[i] = tableScripts[randomIndex];
            tableScripts[randomIndex] = temp;
        }
    }

    private void StartTimers() {
        timer = ordersScriptable.timeInBetweenOrder;

        foreach (GameObject table in tables) {
            FunctionTimer.Create(TimerAction, timer, "Timer");
            timer += ordersScriptable.timeInBetweenOrder;
        }
    }

    private void TimerAction() {
        tableScripts[tableToHandle].MakeOrder();
        Debug.Log("timer test " + tables[tableToHandle].name);
        tableToHandle++;
        
        if (tableToHandle >= tables.Count) AnotherRound(); 
    }
}
