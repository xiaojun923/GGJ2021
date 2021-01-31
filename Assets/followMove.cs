using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followMove : MonoBehaviour
{
    public Transform m_playerTransform;

    //设定一个角色能看到的最远值
    public float Ahead;

    //设置一个摄像机要移动到的点
    private Vector3 prePos;

    //设置一个缓动速度插值
    private float smooth;


    void Start () 
    {
        prePos = m_playerTransform.position;
    }
    
    // Update is called once per frame
    void Update ()
    {

        Vector3 tar = m_playerTransform.position - prePos;

        gameObject.transform.Translate(tar.x,tar.y,0);
        prePos = m_playerTransform.position;

    }
}
