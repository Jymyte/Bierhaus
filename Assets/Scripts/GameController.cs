using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    public List<string> items = new List<string>();
    
    //Remember to insert values respectively in the same order as the items above. 0-100. Total of roll weights should not exceed 100.
    [SerializeField]
    public List<int> rollWeigh = new List<int>();

}
