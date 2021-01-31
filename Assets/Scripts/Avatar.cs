using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Avatar : MonoBehaviour
{
    public Sprite [] sprites;
    public GameObject ground;
    public GameObject door;
    public GameObject dog;
    public GameObject bed;
    public GameObject body;
    public GameObject legs;
    public GameObject lefthand;
    public GameObject righthand;
    public enum BodyState{HEAD=0, HEADBODY=1, HEADBODYLEG=2, HEADBODYLEGHAND1=3, HEADBODYLEGHAND2=4};
    public BodyState m_state;
    public float dev;//加速度
    public float fdev;//摩擦力减速
    public int interTime;
    private Rigidbody2D rig;   //刚体
    public float jumpForce ;  //跳跃的力
    public float slowspeed;//蠕动速度
    public float fastspeed;//行走速度
    private Transform m_transform;
    private Vector3 m_prepos;//上一帧位置
    private float m_speed;//控制移动速度
    private int lastTime;
    private int coll_door_cnt;
	void Start ()
    {
        rig = GetComponent<Rigidbody2D>(); 
        m_transform = this.transform;
        m_speed = 0.0f;
        m_prepos = m_transform.position;
        switchToState(BodyState.HEAD);
        lastTime = Time.frameCount;
        coll_door_cnt = 0;
	}
	void Update ()
    {
        float pre_speed = m_speed;
        if(m_state == BodyState.HEAD)
        {
            if(Input.GetKey(KeyCode.A))
            {     
                m_speed -= dev;
            }
            if(Input.GetKey(KeyCode.D))
            {
                m_speed += dev;
            }
            if(Math.Abs(m_speed) > 0)
            {
                float x = Math.Max(0.0f, Math.Abs(m_speed) - fdev);
                m_speed = Math.Sign(m_speed) * x;
            }
        }
        if(m_state == BodyState.HEADBODY)
        {
            m_speed = 0.0f;
            if (Input.GetKey(KeyCode.A))
            {     
                m_speed = -slowspeed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                m_speed = slowspeed;
            }
        }
        if(m_state == BodyState.HEADBODYLEG || m_state==BodyState.HEADBODYLEGHAND1 || m_state==BodyState.HEADBODYLEGHAND2)
        {
            m_speed = 0.0f;
            if (Input.GetKey(KeyCode.A))
            {     
                m_speed = -fastspeed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                m_speed = fastspeed;
            }
            if(Input.GetKeyDown(KeyCode.J)) rig.AddForce(new Vector2(0,jumpForce));   //给刚体一个向上的力
        }
        if(m_speed < 0)
        {
            SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
            sr.flipX = true;
        }
        if(m_speed > 0)
        {
            SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
            sr.flipX = false;
        }

    }
    void LateUpdate()
    {
        m_transform.Translate(Vector3.right * m_speed * Time.deltaTime, Space.World);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if(m_state == BodyState.HEAD &&  collision.gameObject == body)
        {
            switchToState(BodyState.HEADBODY);
            DestroyObject(collision.gameObject);
        }
        if(m_state ==BodyState.HEADBODY && collision.gameObject == legs)
        {
            switchToState(BodyState.HEADBODYLEG);
            DestroyObject(collision.gameObject);
        }
        if(m_state == BodyState.HEADBODYLEG && collision.gameObject == righthand)
        {
            switchToState(BodyState.HEADBODYLEGHAND1);
            DestroyObject(collision.gameObject);
        }
        if(m_state == BodyState.HEADBODYLEGHAND1 && collision.gameObject == lefthand)
        {
            switchToState(BodyState.HEADBODYLEGHAND2);
            DestroyObject(collision.gameObject);
        }
        if(m_state == BodyState.HEADBODY && collision.gameObject == door)
        {
            Debug.Log("Enter!!!!");
            Debug.Log(Time.frameCount);
            int crtTime = Time.frameCount;
            if(coll_door_cnt > 0)
            {   
                int devTime = crtTime-lastTime;
                if(devTime < interTime)
                {
                    coll_door_cnt++;
                    if(coll_door_cnt >= 3)
                    {
                        DestroyObject(collision.gameObject);
                    }
                    else
                    {
                        lastTime = crtTime;
                    }
                }
                else
                {
                    coll_door_cnt = 0;
                }
            }
            else
            {
                coll_door_cnt++;
                lastTime = crtTime;
            }
        }
        if(m_state ==BodyState.HEADBODYLEGHAND1&&collision.gameObject ==dog)
        {
            DestroyObject(collision.gameObject);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {

    }

    void OnCollisionExit2D(Collision2D collision)
    {

    }

    void switchToState(BodyState st)
    {
        m_state = st;
        SpriteRenderer sr = gameObject.transform.GetComponent<SpriteRenderer>();
        sr.sprite = sprites[(int)st];
        Debug.Log("switdh to" + sr);
    }



}
