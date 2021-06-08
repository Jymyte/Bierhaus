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
    float addTableTimer;
    private int nextTableToAdd = 0;
    private int tablesInRound;

    private void Start() {
        timer = ordersScriptable.timeInBetweenOrder;
        tablesInRound = globals.startingActiveTableAmount;
        
        while (tableScripts.Count < globals.startingActiveTableAmount) {
            AddTable();
        }
        SetTablesInQueue();
        
        InitializeTables();
        StartTimers();

    }

    private void AddTable() {
        if (tableScripts.Count < tables.Count) {
            tableScripts.Add(tables[nextTableToAdd].GetComponent<Table>());
            nextTableToAdd++;
        }
    }

    private void SetTablesInQueue() {
        int temp = nextTableToAdd;

        while (temp < tables.Count) {
            FunctionTimer.Create(AddTable, addTableTimer, "AddNewTableTimer");
            addTableTimer += globals.addNewTableTimer;
            temp++;
        }
    }

    private void InitializeTables() {
        RollTableOrder();
    }

    private void AnotherRound() {
        tablesInRound = tableScripts.Count;
        //Debug.Log("Another round!");
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

        foreach (Table table in tableScripts) {
            FunctionTimer.Create(TimerAction, timer, "Timer");
            timer += ordersScriptable.timeInBetweenOrder;
        }
    }

    private void TimerAction() {
        tableScripts[tableToHandle].MakeOrder();
        tableToHandle++;
        
        if (tableToHandle >= tablesInRound) AnotherRound(); 
    }
}
