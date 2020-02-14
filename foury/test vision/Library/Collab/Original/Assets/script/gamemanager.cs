using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamemanager : MonoBehaviour
{
    public player_manager pl;
    public horse_car_m car;
    private bool move=false;
    // Start is called before the first frame update
    void Awake()
    {
        //pl = GameObject.Find("player").GetComponent<player_manager>();
        //car = GameObject.Find("horse_car").GetComponent<horse_car_m>();
        startposition();
        cutscene();
    }
  
    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        
    }

    private void startposition()
    {
        pl.transform.position = new Vector3(120f, pl.transform.position.y, pl.transform.position.z);
        car.transform.position = new Vector3(119.1f, car.transform.position.y, car.transform.position.z);
    }
    
    private void cutscene()
    {
       


    }
    IEnumerator moove()
    {
       
        yield return new WaitForSeconds(3.0f);
       
    }

    }
