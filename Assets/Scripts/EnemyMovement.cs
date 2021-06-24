using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // ī�޶�, �÷��̾� ��ġ �޾ƿ���
    Transform cameraPos;
    Transform playerPos;

    float bodyguardSpeed;  
    Vector3 targetPos;

    // ����� ��
    public EnemyGun gun; 

    private void Awake()
    {
        cameraPos = GameObject.Find("Camera").GetComponent<Transform>();
        playerPos = GameObject.Find("Player").GetComponent<Transform>();
    }

    public void SetUp(float xS, float zS, float xE, float zE)
    {
        // �� ���� ��ġ
        transform.position = new Vector3(xS, playerPos.position.y, zS);

        // �� ���� ��ġ
        targetPos = new Vector3(xE, playerPos.position.y, zE);
    }

    private void Start()
    {
        bodyguardSpeed = Random.Range(5f, 8f);
        StartCoroutine(MakeMove());
    }

    private void Update()
    {
        // ī�޶� �ٶ󺸴� �������� 
        transform.LookAt(cameraPos.position);
    }

    private IEnumerator MakeMove()
    {
        // �̵�
        while (true)
        {
            // ���� Ÿ�������� �����ϸ� break
            float checkDist = Vector3.Distance(targetPos, transform.position);
            if (checkDist <= 1f)
            {
                break;
            }

            // ���� ��ġ���� ī�޶� ������ ���ͼ� ����
            MoveToPosition();

            // ��� ������
            yield return new WaitForSeconds(0.05f);
        }

        // (����) ���
        //while (isAlive)
        for (int i = 0; i < 10; i++)
        {
            // �ѹ� �߻��ϰ�
            gun.Fire();

            // �÷��̾� ���� ���̰�

            // ��� ������
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void MoveToPosition()
    {
        // �̵� ���� ���� ���
        Vector3 moveDir = (targetPos - transform.position).normalized;
        
        // �̵�
        transform.position += moveDir * bodyguardSpeed * Time.deltaTime;
    }
}
