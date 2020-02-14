using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_manager : MonoBehaviour
{
    public int Hp = 100;
    private float curTime;
    public float coolTime ;
    public Transform pos;
    public Vector2 boxsize;
    public int atdamage;
    Rigidbody2D rigid;
    private bool isinvin = false;
    player_manager pl;
    private bool see;
    private int movefleg;
    public float atdelay = 0.5f;
    public float firstdelay = 0.5f;
    private bool canmove = true;
    private bool isat = false;
    private bool isick = false;
    private bool start = true;
    public string[] senteences;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        pl = GameObject.Find("player").GetComponent<player_manager>();
        StartCoroutine("cutscene");
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        move();
    }
    private void OnEnable()
    {
       
    }
    void Update()
    {
        if (start) snje_dong();
        if (isinvin)
        {
            StopCoroutine("attack");
            curTime = coolTime;
            isat = false;
            Debug.Log("어 왜 안멈춰 으아아ㅏ 미안해요");
        }
        if(Hp<=0)
        {
           
            Destroy(gameObject);
        }
        if (transform.position.x - pl.transform.position.x <= 1.5f && transform.position.x - pl.transform.position.x >= -1.5f)
        {
            see = true;
        }
        else see = false;

            if (see)
        {
            
           

            if (transform.position.x - pl.transform.position.x <= 0.5f&& transform.position.x - pl.transform.position.x >=-0.5f)
            {
                
                movefleg = 0;
                if (curTime <= 0&&!isat)
                {
                    StartCoroutine("attack");
                    
                }
                else curTime -= Time.deltaTime;
            }
            else if (transform.position.x - pl.transform.position.x > 0)
            {
                curTime = 0;
                movefleg = 1;//left
            }
            else if (transform.position.x - pl.transform.position.x <= 0)
            {
                curTime = 0;
                movefleg = 2;
            }
        }


        
          
        
    }
    void move()
    {





        if (canmove)
        {
            Vector3 moveVelocity = Vector3.zero;
            if (movefleg == 0)
            {
                moveVelocity = Vector3.zero;
                //ani.SetBool("run", false);
                //ismov = false;

            }
            else if (movefleg == 1)
            {
                //ismov = true;
                moveVelocity = Vector3.left;

                //ani.SetBool("run", true);
                transform.rotation = Quaternion.Euler(0, 180, 0);
                //render.flipX = false;
            }
            else if (movefleg == 2)
            {
                //ismov = true;
                moveVelocity = Vector3.right;
                //ani.SetBool("run", true);
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            transform.position += moveVelocity * 0.5f * Time.deltaTime;

        }



        
    }
    public void Damage(int damage)
    {
        if (!isinvin){
            curTime = coolTime;
            Hp -= damage;
            StartCoroutine("invin");
            
        }

         if (transform.position.x - pl.transform.position.x > 0)
        {
           
            rigid.velocity = Vector2.zero;
            Vector2 jumpVelocity = new Vector2(2, 1.6f);
            rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);
        }
        else if (transform.position.x - pl.transform.position.x <= 0)
        {
            rigid.velocity = Vector2.zero;
            Vector2 jumpVelocity = new Vector2(-2, 1.6f);
            rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);
        }
    }
    public void UpDamage(int damage)
    {
        if (!isinvin)
        {
            rigid.velocity = Vector2.zero;
            Vector2 jumpVelocity = new Vector2(0, 8);


            rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);
            Hp -= damage;
            StartCoroutine("invin");
        }
    }
    public void DownDamage(int damage)
    {
        if (!isinvin)
        {
            rigid.velocity = Vector2.zero;
            Vector2 jumpVelocity = new Vector2(0, -10);


            rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);
            Hp -= damage;
            StartCoroutine("invin");
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxsize);
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 && rigid.velocity.y < 0)
        {
            rigid.velocity = Vector2.zero;  
        }
       



    }
   

    IEnumerator invin()
    {
        curTime = coolTime;
        isinvin = true;
        canmove = false;
        yield return new WaitForSeconds(0.01f);
        isinvin = false;
        yield return new WaitForSeconds(0.3f);
        canmove = true;

    }
    IEnumerator attack()
    {
       
            isat = true;
            yield return new WaitForSeconds(firstdelay);
            Debug.Log("몬스터 공격중");


            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxsize, 0);
            foreach (Collider2D collider in collider2Ds)
            {
                if (collider.gameObject.layer == 10)
                {
                    collider.GetComponent<player_manager>().Damage(atdamage);

                }

            }
            curTime = coolTime;
            canmove = false;
            yield return new WaitForSeconds(atdelay);
            isat = false;
            canmove = true;
        

    }
    private void snje_dong()
    {
        Vector3 moveVelocity = Vector3.right;
        transform.position += moveVelocity * 0.5f * Time.deltaTime;

    }
    IEnumerator cutscene()
    {
        pl.istalking = true;
        start = true;
        yield return new WaitForSeconds(1.0f);
        start = false;
        pl.istalking = false;
        DialogueManager.instance.OnDialogue(senteences);
    }

}
