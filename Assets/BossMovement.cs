using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    // �̵� point
    [Serializable]
    public class Point
    {
        public float x;
        public float y;
        public float z;
    }
    [SerializeField]
    public List<Point> points;
    private int movingIndex { get; set; } 

    // ī�޶� �ٶ󺸱�
    private Transform cameraPos;

    // �� ���
    public GameObject bossGun;

    // �ð�
    private float time = 0f;
    private bool checkTime = false;
    private bool makeFirstMove;
    private bool makeSecondMove;

    // ü��
    public GameObject bossHpBar;

    private void OnEnable()
    {
        bossHpBar.SetActive(true);

        checkTime = false;
        makeFirstMove = true;
        makeSecondMove = false;

        movingIndex = 0;
        transform.position = new Vector3(points[movingIndex].x, points[movingIndex].y, points[movingIndex].z);    // index = 0
        movingIndex++;    // index = 1
    }

    private void OnDisable()
    {
        bossHpBar.SetActive(false);
    }

    private void Start()
    {
        cameraPos = GameObject.Find("Camera").GetComponent<Transform>();
    }

    private void Update()
    {
        // �̵� ����
        for (int i = 0; i < points.Count - 1; i++)
        {
            if (points[i + 1] != null)
            {
                Vector3 startPosition = new Vector3(points[i].x, points[i].y, points[i].z);
                Vector3 endPosition = new Vector3(points[i + 1].x - points[i].x, points[i + 1].y - points[i].y, points[i + 1].z - points[i].z);

                Debug.DrawRay(startPosition, endPosition, Color.blue);
            }
        }

        // ������ �׻� ī�޶� �ٶ󺸱�
        if (checkTime == false)
            transform.LookAt(cameraPos.position);

        // ���� �׻� ��� �ֱ�
        bossGun.GetComponent<BossGun>().Fire();

        // ó�� �����̰�
        if (makeFirstMove)
            MakeFirstMove();

        //��� ����ٰ�
        if (checkTime == true)
        {
            time += Time.deltaTime;

            if (time >= 3f)
            {
                checkTime = false;
                makeSecondMove = true;
            }
        }

        //�ٽ� �����̰� + �������� �����ϸ� ���߱�
        if (makeSecondMove)
            MakeSecondMove();

        // ü�� Ȯ��

        // ��� Ȯ�� �� ����

        // ��� �ý��ھ� ����
    }

    private void MakeFirstMove()
    {
        Vector3 nextPos = new Vector3(points[movingIndex].x, points[movingIndex].y, points[movingIndex].z);
        transform.position = Vector3.MoveTowards(transform.position, nextPos, Time.deltaTime * 13f);

        if (transform.position == nextPos)   // index = 1
        {
            if (movingIndex < points.Count - 1)
            {
                movingIndex++;
                checkTime = true;
                makeFirstMove = false;
            }
        }
    }

    private void MakeSecondMove()
    {
        Vector3 nextPos = new Vector3(points[movingIndex].x, points[movingIndex].y, points[movingIndex].z);
        transform.position = Vector3.MoveTowards(transform.position, nextPos, Time.deltaTime * 15f);
    }
}
