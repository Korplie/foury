using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSentence : MonoBehaviour
{
    // Start is called before the first frame update
    public string[] senteences;
       
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void talk()
    {
        if(DialogueManager.instance.dg.alpha==0)
        DialogueManager.instance.OnDialogue(senteences);


    }
  
    //public void OnMouseDown()
    //{
    //    DialogueManager.instance.OnDialogue(senteences);


    //}



}
