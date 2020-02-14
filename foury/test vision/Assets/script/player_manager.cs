using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_manager : MonoBehaviour
{
    
    public int havehp = 100;
    public int hp = 100;
    public int movepower = 3;
    public float jumppower = 6;
    public Slider hpslier;
    public Rigidbody2D rigid;
    SpriteRenderer render;
    public Animator ani;
    private bool isDead = false;
    public BoxCollider2D triggerbody;
    public CapsuleCollider2D body;
    public int maxjumpcnt = 1;
    private int havejcnt = 0;
    private float curtime = 0.5f;
    private float coolTime =0.2f;
    private float combotime =0;
    private float korocurTime;
    private float korocoolTime = 0.6f;
    public Transform pos1;
    public Vector2 boxsize1;
    public Transform pos2;
    public Vector2 boxsize2;
    public Transform pos3;
    public Vector2 boxsize3;
    public int atdamage = 25;
    private bool canat = true;
    public int atcnt=0;
    private bool ismov=false;
    private bool cansick = true;
    public bool canmove = true;
    private bool isground = true;
    private bool koro;
    private int pt = 1;
    public bool istalking = false;
  

    // Start is called before the first frame update
    void Start()
    {
        render = gameObject.GetComponentInChildren<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        havehp = hp;
        ani = gameObject.GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {
        //rigid.velocity = new Vector3(0, -1, 0);
        hpslier.maxValue = hp;
        hpslier.value = havehp;
        if (havehp > hp)
            havehp = hp;

        if (!isDead&&cansick)
        {

            if (canat&&!istalking)
            {
                //if (curTime <= 0 && atcnt == 0)
                //{

                //    if (Input.GetKeyDown(KeyCode.Z) && !ismov && isground)
                //    {
                //        atcnt++;

                //        StartCoroutine("k1_at1");

                //    }


                //}

                //else if (combotime <= 0.5 && atcnt == 1 || combotime >= 0.1 && atcnt == 1 && !ismov && isground)
                //{
                //    if (Input.GetKeyDown(KeyCode.Z))
                //    {
                //        atcnt++;

                //        StartCoroutine("k1_at2");

                //    }

                //}
                //else if (combotime <= 0.5 && atcnt == 2 || combotime >= 0.1 && atcnt == 2 && !ismov && isground)
                //{
                //    if (Input.GetKeyDown(KeyCode.Z))
                //    {

                //        StartCoroutine("k1_at3");
                //        atcnt = 0;
                //    }

                //}

                //else
                //{
                //    curTime -= Time.deltaTime;
                //}
                switch (pt) {

                    case 1:
                        if (combotime <= 0.2 && !ismov && isground || combotime >= 0.1 && !ismov && isground)
                        {
                            if (Input.GetKeyDown(KeyCode.Z) && !ismov)
                            {
                                StartCoroutine("p1_at1");
                                //switch (atcnt)
                                //{
                                //    case 0:
                                //        atcnt++;
                                //        StartCoroutine("k1_at1");
                                //        break;
                                //    case 1:
                                //        atcnt++;
                                //        StartCoroutine("k1_at2");
                                //        break;
                                //    case 2:
                                //        atcnt++;
                                //        StartCoroutine("k1_at3");
                                //        break;

                                //    default: return;
                                //}

                            }

                        }
                        break;
                    case 2:
                        if (combotime <= 0.2 && !ismov && isground || combotime >= 0.1 && !ismov && isground)
                        {
                            if (Input.GetKeyDown(KeyCode.Z) && !ismov)
                            {

                                switch (atcnt)
                                {
                                    case 0:
                                        atcnt++;
                                        StartCoroutine("p1_at1");
                                        break;
                                    case 1:
                                        atcnt++;
                                        StartCoroutine("p2_at1");
                                        break;
                                    case 2:
                                        atcnt++;
                                        StartCoroutine("p2_at2");
                                        break;

                                    default: return;
                                }

                            }

                        }
                        break;


                    default: Debug.Log("오류");break;


                }


                if (Input.GetKeyDown(KeyCode.R) && !ismov)
                {
                    pt++;
                    if (pt >= 3) pt = 1;
                }

                    if (Input.GetKeyDown(KeyCode.X) && isground && korocurTime <= 0)
                {
                    canmove = false;
                    if (transform.rotation.y == 0)
                    {
                        korocurTime = korocoolTime;
                        rigid.velocity = Vector2.zero;
                        Vector2 jumpVelocity = new Vector2(-jumppower, 0);
                        koro = true;

                        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);
                        StartCoroutine("koroin");
                    }
                    else if (!(transform.rotation.y == 0))
                    {
                        korocurTime = korocoolTime;
                        rigid.velocity = Vector2.zero;
                        Vector2 jumpVelocity = new Vector2(jumppower, 0);
                        koro = true;

                        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);
                        StartCoroutine("koroin");
                    }
                }
               
                else korocurTime -= Time.deltaTime; ;

            }



            
            if (curtime >= 0) curtime -= Time.deltaTime;
            if (combotime >= 0) combotime -= Time.deltaTime;
            

            if (havehp <= 0)
                {
                    isDead = true;

                }


                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Application.Quit();
                }
                if (Input.GetKeyDown(KeyCode.Space) && !(maxjumpcnt == havejcnt)&&cansick && rigid.velocity.y >= 0)
                {
                    havejcnt++;

                    jump();
                }




            

        }
    }
        void FixedUpdate()
        {
            if (!isDead&&canat&&cansick&&canmove&&!istalking)
            {

                move();

            }

        }

        void move()
        {
            if (!isDead&&canmove)
            {





                Vector3 moveVelocity = Vector3.zero;
                if (Input.GetAxisRaw("Horizontal") == 0)
                {
                    ani.SetBool("run", false);
                ismov = false;

                }
                else if (Input.GetAxisRaw("Horizontal") < 0)
                {
                ismov = true;
                moveVelocity = Vector3.left;

                ani.SetBool("run", true);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                //rigid.velocity = Vector2.zero;
                //Vector2 jumpVelocity = new Vector2(-movepower, 0);
                //rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);


                //render.flipX = false;
            }
                else if (Input.GetAxisRaw("Horizontal") > 0)
                {
                ismov = true;
                moveVelocity = Vector3.right;
                ani.SetBool("run", true);
                transform.rotation = Quaternion.Euler(0, 180, 0);
                //rigid.velocity = Vector2.zero;
                //Vector2 jumpVelocity = new Vector2(movepower, 0);
                //rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);
            }
            transform.position += moveVelocity * movepower * Time.deltaTime;




        }
        }
        void jump()
        {
            if (!isDead&&!istalking)
            {





                rigid.velocity = Vector2.zero;
                Vector2 jumpVelocity = new Vector2(0, jumppower);


                rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);
            isground = false;
            }




        }
   

    private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 8 && rigid.velocity.y < 0)
            {
            rigid.velocity = Vector2.zero;


            havejcnt = 0;
            isground = true;
            }
       
        //if (collision.gameObject.layer == 9)
        //{
        //    if (cansick)
        //    {
        //        havehp -= 10;
        //        StartCoroutine("invin");
        //    }
        //}
        }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 && rigid.velocity.y < 0)
        {
            rigid.velocity = Vector2.zero;


            havejcnt = 0;
            isground = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                collision.GetComponent<NPCSentence>().talk();
            }
        }
    }

    private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(pos1.position, boxsize1);
            
            Gizmos.DrawWireCube(pos2.position, boxsize2);
            
            Gizmos.DrawWireCube(pos3.position, boxsize3);
    }
        public void Damage(int damage/*,bool isl*/)
        {
        if (cansick==true&&koro==false)
        {
            havehp -= damage;
            StartCoroutine("invin");
           
            

            //if(isl)
            //{

            //}

        }
        }
   
    IEnumerator invin()
    {
        
        atcnt = 0;
        cansick = false;
        canmove = false;
        yield return new WaitForSeconds(0.3f);
        canmove = true;
        cansick = true;
       

    }
    IEnumerator koroin()
    {
        //rigid.gravityScale = 0;
        //body.enabled = false;
        canmove = false;
        yield return new WaitForSeconds(0.3f);
        rigid.velocity = Vector2.zero;
        koro = false;
        yield return new WaitForSeconds(0.06f);
        canmove = true;
        //body.enabled = true ;
        //rigid.gravityScale = 1;



    }
    IEnumerator p1_at1()
        {
            atdamage = 25;
       
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos1.position, boxsize1, 0);
                foreach (Collider2D collider in collider2Ds)
                {
                    if (collider.gameObject.layer == 9)
                    {
                        collider.GetComponent<monster_manager>().Damage(atdamage);
                if (collider.GetComponent<monster_manager>().Hp <= 0)
                    havehp += 6;
                else
                havehp += 2;
                    }

                }


                ani.SetTrigger("k1_at1");





        combotime = coolTime;
       
        canat = false;
        yield return new WaitForSeconds(0.3f);
        curtime = 0.5f;
        canat = true;
        yield return new WaitForSeconds(0.6f);
        atcnt = 0;


    }
        IEnumerator p2_at1()
        {
        havehp -= 5;
     
            atdamage = 35;
      
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos2.position, boxsize2, 0);
                foreach (Collider2D collider in collider2Ds)
                {
                    if (collider.gameObject.layer == 9)
                    {
                        collider.GetComponent<monster_manager>().UpDamage(atdamage);
                if (collider.GetComponent<monster_manager>().Hp <= 0) { 
                    havehp += 6;}
                else
                    havehp += 2;
            }

                }


                ani.SetTrigger("k1_at2");



        combotime = coolTime;
       
        canat = false;
        yield return new WaitForSeconds(0.6f);
        curtime = 0.5f;
        canat = true;
        yield return new WaitForSeconds(0.6f);
        atcnt = 0;
       
        
      


    }
    IEnumerator p2_at2()
    {
        havehp -= 5;

        atdamage = 60;
      
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos3.position, boxsize3, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.gameObject.layer == 9)
            {
                collider.GetComponent<monster_manager>().DownDamage(atdamage);
                if (collider.GetComponent<monster_manager>().Hp <= 0)
                    havehp += 6;
                else
                    havehp += 2;
            }

        }


        ani.SetTrigger("k1_at3");



        combotime = coolTime;
       
        canat = false;
        yield return new WaitForSeconds(1f);
       curtime = 0.2f;
        canat = true;
        atcnt = 0;

        

    }
}
