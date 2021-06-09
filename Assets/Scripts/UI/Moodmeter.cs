using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moodmeter : MonoBehaviour
{
    [SerializeField]
    private Globals globals;
    [SerializeField]
    private GameObject parentTable;
    [SerializeField]
    private GameObject arm;
    private RectTransform armRectTransform;
    private Vector3 armTarget;
    [SerializeField]
    private GameObject background;
    private RectTransform backgroundRectTransform;
    private float backgroundWidth;
    private Table tableScript;
    private float lerpTime = 100f;
    private Vector3 temp;

    void Start()
    {
        tableScript = parentTable.GetComponent<Table>();
        backgroundRectTransform = background.GetComponent<RectTransform>();
        backgroundWidth = backgroundRectTransform.sizeDelta.x * backgroundRectTransform.localScale.x;
        armRectTransform = arm.GetComponent<RectTransform>();
        tableScript.handleMoodChange += PositionArm;
        tableScript.handleOverAllMoodChange += (bool fulfilled, int happiness) => {StartCoroutine(ResetArm(fulfilled, happiness));};

        PositionArm(true, globals.defaultHappiness);
    }

    void PositionArm(bool fulfilled, int happiness) {
        int temp = globals.maxHappiness + 1;
        float subDiv = backgroundWidth / temp;
        float targetX = subDiv * happiness;
        armTarget = new Vector3(targetX, armRectTransform.position.y, armRectTransform.position.z);
        StartCoroutine("MoveArm");
    }

    private IEnumerator ResetArm(bool fulfilled, int happiness) {
        Debug.Log("RESET ARM happiness: " + happiness);
        yield return new WaitForSeconds(globals.resetTableTimer);
        StopCoroutine("MoveArm");
        PositionArm(fulfilled, happiness);
    }

    private IEnumerator MoveArm() {
        Debug.Log("Happiness: " + tableScript.GetHappiness());
        while (Vector3.Distance(armRectTransform.anchoredPosition3D, armTarget) > 0.05f) {
            armRectTransform.anchoredPosition3D = Vector3.MoveTowards(armRectTransform.anchoredPosition3D, armTarget, lerpTime * Time.deltaTime);
            yield return null;
        }
    }
        
}
