using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    // 이동 point
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

    // 카메라 바라보기
    private Transform cameraPos;

    // 총 쏘기
    public GameObject bossGun;

    // 시간
    private float time = 0f;
    private bool checkTime = false;
    private bool makeFirstMove;
    private bool makeSecondMove;

    // 체력
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
        // 이동 라인
        for (int i = 0; i < points.Count - 1; i++)
        {
            if (points[i + 1] != null)
            {
                Vector3 startPosition = new Vector3(points[i].x, points[i].y, points[i].z);
                Vector3 endPosition = new Vector3(points[i + 1].x - points[i].x, points[i + 1].y - points[i].y, points[i + 1].z - points[i].z);

                Debug.DrawRay(startPosition, endPosition, Color.blue);
            }
        }

        // 시점은 항상 카메라 바라보기
        if (checkTime == false)
            transform.LookAt(cameraPos.position);

        // 총을 항상 쏘고 있기
        bossGun.GetComponent<BossGun>().Fire();

        // 처음 움직이고
        if (makeFirstMove)
            MakeFirstMove();

        //잠시 멈췄다가
        if (checkTime == true)
        {
            time += Time.deltaTime;

            if (time >= 3f)
            {
                checkTime = false;
                makeSecondMove = true;
            }
        }

        //다시 움직이고 + 마지막에 도달하면 멈추기
        if (makeSecondMove)
            MakeSecondMove();

        // 체력 확인

        // 사망 확인 및 설정

        // 사망 시스코어 띄우기
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
