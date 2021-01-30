using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    public enum BodyState{HEAD, HEADBODY, HEADBODYLEG, HEADBODYLEGHAND1, HEADBODYLEGHAND2};
    public BodyState state;
    public float speed;//控制移动速度
    public float wspeed;//控制旋转速度
    public Transform m_transform;

	// Use this for initialization
	void Start () 
    {
        m_transform = this.transform;
	}
	
	// Update is called once per frame
	void Update () 
    {
        //向左
        if (Input.GetKey(KeyCode.A))
        {

        }

        //向右
        if (Input.GetKey(KeyCode.D))
        {


        }

        m_transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
        m_transform.Rotate(Vector3.forward * wspeed * Time.deltaTime, Space.Self);

    }

}
