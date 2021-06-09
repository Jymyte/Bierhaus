using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimationController : MonoBehaviour
{
    [SerializeField]
    private Globals globals;
    private Animator npcAnim;
    void Start()
    {
        npcAnim = gameObject.GetComponent<Animator>();
    }

    public void PlayAnim(string animBool) {
        StartCoroutine(PlayAnimIEnumerator(animBool));
    }

    private IEnumerator PlayAnimIEnumerator(string animBool) {
        npcAnim.SetBool(animBool, true);
        yield return new WaitForSeconds(globals.NPCAnimationTime);
        npcAnim.SetBool(animBool, false);
    } 
}
