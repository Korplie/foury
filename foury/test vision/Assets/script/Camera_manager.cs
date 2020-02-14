using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_manager : MonoBehaviour
{

    public Transform target;
    public bool iscamera;
    private float speed=3112f;
    void Start()
    {
        
    }

  
    void LateUpdate()
    {

        Vector3 tt = new Vector3(target.position.x, target.position.y+0.7f , target.position.z);

        if (iscamera)
        {
            transform.position = Vector3.Lerp(transform.position, tt, Time.deltaTime * speed);
            transform.position = new Vector3(transform.position.x, transform.position.y , -10f);
        }
      else
        {
            transform.position = Vector3.Lerp(transform.position, tt, Time.deltaTime * speed);
            transform.position = new Vector3(transform.position.x, transform.position.y , 10f);
        }
    }
}
