using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(LineRenderer))]

public class WayPoint : MonoBehaviour
{
    //원하는 좌표를 담기 위해 class로 구조체를 만들었다
    [Serializable]
    public class Point
	{
        public float x;
        public float y;
        public float z;
	}

    //리스트로 쉽게 여러게 좌표를 담기위해 리스트를 썼다
    [SerializeField]
    public List<Point> points;

    //public Transform[] m_transform { get; private set; }

	private void Awake()
	{
        //m_transform = new Transform[points.Count];
        //for(int i = 0; i < points.Count; i++)
		//{
        //    //m_transform[i].position = new Vector3(points[i].x, points[i].y, points[i].z);
        //}
	}

	// Update is called once per frame
	void Update()
    {
		//m_Transform.position = new Vector3(points[index + 1].x, points[index + 1].y, points[index + 1].z);
        //DrawRay로 이동결로를 보여주기 위해 해 놓았다
        for(int i = 0; i < points.Count - 1; i++)
		{
            if(points[i + 1] != null)
			{
                Vector3 startPosition = new Vector3(points[i].x, points[i].y, points[i].z);
                Vector3 endPosition = new Vector3(points[i + 1].x - points[i].x, points[i + 1].y - points[i].y, points[i + 1].z - points[i].z);

                Debug.DrawRay(startPosition, endPosition, Color.green);
            }
        }
    }
}
