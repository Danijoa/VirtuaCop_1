using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(LineRenderer))]

public class WayPoint : MonoBehaviour
{
    //���ϴ� ��ǥ�� ��� ���� class�� ����ü�� �������
    [Serializable]
    public class Point
	{
        public float x;
        public float y;
        public float z;
	}

    //����Ʈ�� ���� ������ ��ǥ�� ������� ����Ʈ�� ���
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
        //DrawRay�� �̵���θ� �����ֱ� ���� �� ���Ҵ�
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
