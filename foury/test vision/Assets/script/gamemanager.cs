using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamemanager : MonoBehaviour
{
    // Start is called before the first frame update
    public string[] senteences;
    private int only = 0;
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueManager.instance.dg.alpha == 0 && only == 0)
        {
            DialogueManager.instance.OnDialogue(senteences);
            only++;
        }

    }
    //public void talk()
    //{
    //    if (DialogueManager.instance.dg.alpha == 0)
    //        DialogueManager.instance.OnDialogue(senteences);


    //}

    //public void OnMouseDown()
    //{
    //    DialogueManager.instance.OnDialogue(senteences);


    //}



}
