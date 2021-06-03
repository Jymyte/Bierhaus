using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GlobalsScriptable")]
public class Globals : ScriptableObject
{
    public int inventorySize;
    public float serveActionTime;
    public float addItemActionTime;
    public int maxHappiness;
    public int defaultHappiness;
    
}
