using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulit_controller : MonoBehaviour
{

    Rigidbody2D rigid;
    public float speed=30f;
    public float nyam = 0;
    // Use this for initialization
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        if(nyam!=0)
        transform.position += new Vector3(-nyam-5, nyam-10, nyam-10);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveVelocity = Vector3.left;
        transform.position += moveVelocity * speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            Destroy(gameObject, 0.1f);
        }
        if (collision.gameObject.layer == 10)
        {
            collision.GetComponent<player_manager>().Damage(25);

        }
    }
}
