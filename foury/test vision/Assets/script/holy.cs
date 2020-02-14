using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holy : MonoBehaviour
{
    public player_manager pl;
    public horse_car_m car;
    public string[] sentences ;
    private bool move = false;
    private bool only=true;
    public GameObject sanjuk;
    private bool iscutscene = false;
    
    public bool isaned=true;
    // Start is called before the first frame update
    void Awake()
    {
        //pl = GameObject.Find("player").GetComponent<player_manager>();
        //car = GameObject.Find("horse_car").GetComponent<horse_car_m>();
        startposition();
        iscutscene = true;
      
    }

    // Update is called once per frame
    void Update()
    {

      
        if (DialogueManager.instance.dg.alpha == 0 && only)
        {
            DialogueManager.instance.OnDialogue(sentences);
            only=false;
        }
        if (DialogueManager.instance.dg.alpha == 1&&iscutscene)
        {
            e_dong();
            pl.ani.SetBool("disavle", true);
                    pl.istalking = true;
        }
        else if (DialogueManager.instance.dg.alpha == 0&&iscutscene )
        {
            pl.istalking = false;
            iscutscene = false;
            pl.ani.SetBool("disavle", false);
            shohwansanj();
           
        }

    }
    //private void FixedUpdate()
    //{
    //    if (DialogueManager.instance.dg.alpha == 1 && iscutscene)
    //    {
    //        e_dong();
    //        pl.ani.SetBool("disavle", true);
    //        pl.istalking = true;
    //    }
    //    else if (DialogueManager.instance.dg.alpha == 0 && iscutscene)
    //    {
    //        pl.istalking = false;
    //        pl.ani.SetBool("disavle", false);
    //    }

    //}

    private void startposition()
    {
        pl.transform.position = new Vector3(120f, pl.transform.position.y, pl.transform.position.z);
        car.transform.position = new Vector3(119.1f, car.transform.position.y, car.transform.position.z);
    }
  

    private void e_dong()
    {
        Vector3 moveVelocity = Vector3.left;
        pl.transform.position += moveVelocity * 1f * Time.deltaTime;
        car.transform.position += moveVelocity * 1f * Time.deltaTime;

    }
    private void shohwansanj()
    {
        Vector2 holy = new Vector2(pl.transform.position.x - 3f, pl.transform.position.y);
        Instantiate(sanjuk, holy, Quaternion.identity);
    }

    
}
