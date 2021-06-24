using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // 카메라, 플레이어 위치 받아오기
    Transform cameraPos;
    Transform playerPos;

    float bodyguardSpeed;  
    Vector3 targetPos;

    // 사용할 총
    public EnemyGun gun; 

    private void Awake()
    {
        cameraPos = GameObject.Find("Camera").GetComponent<Transform>();
        playerPos = GameObject.Find("Player").GetComponent<Transform>();
    }

    public void SetUp(float xS, float zS, float xE, float zE)
    {
        // 적 생성 위치
        transform.position = new Vector3(xS, playerPos.position.y, zS);

        // 적 도착 위치
        targetPos = new Vector3(xE, playerPos.position.y, zE);
    }

    private void Start()
    {
        bodyguardSpeed = Random.Range(5f, 8f);
        StartCoroutine(MakeMove());
    }

    private void Update()
    {
        // 카메라를 바라보는 방향으로 
        transform.LookAt(cameraPos.position);
    }

    private IEnumerator MakeMove()
    {
        // 이동
        while (true)
        {
            // 적이 타겟지점에 도착하면 break
            float checkDist = Vector3.Distance(targetPos, transform.position);
            if (checkDist <= 1f)
            {
                break;
            }

            // 생성 위치에서 카메라 안으로 들어와서 정지
            MoveToPosition();

            // 잠깐 멈추자
            yield return new WaitForSeconds(0.05f);
        }

        // (조준) 사격
        //while (isAlive)
        for (int i = 0; i < 10; i++)
        {
            // 한번 발사하고
            gun.Fire();

            // 플레이어 생명 줄이고

            // 잠깐 멈추자
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void MoveToPosition()
    {
        // 이동 방향 벡터 잡기
        Vector3 moveDir = (targetPos - transform.position).normalized;
        
        // 이동
        transform.position += moveDir * bodyguardSpeed * Time.deltaTime;
    }
}
