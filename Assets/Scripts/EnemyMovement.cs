using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public enum State
	{
        Move,
        Battle,
        Die
    }

    // Component
    Rigidbody myRigid;

    // ī�޶�, �÷��̾� ��ġ �޾ƿ���
    Transform playerPos;
    Transform cameraPos;

    // ī�޶� ����
    private CameraCtrl cameraCtrl;
    
	// ����� ��
    public GameObject gun;

    //�� ����
    public EnemyState enemyState;
    public State state { get; set; }

    // �� 
    float bodyguardSpeed;
    Vector3 targetPos;

    private void Awake()
    {
        cameraPos = GameObject.Find("Camera").GetComponent<Transform>();
        enemyState = GetComponent<EnemyState>();
        state = State.Move;
    }

    public void SetUp(float xS, float zS, float xE, float zE)
    {
        playerPos = GameObject.Find("Player").GetComponent<Transform>();
        cameraCtrl = GameManager.instance.m_Camera.GetComponent<CameraCtrl>();

        // �� ���� ��ġ
        transform.position = new Vector3(xS, playerPos.position.y, zS);

        // �� ���� ��ġ
        targetPos = new Vector3(xE, playerPos.position.y, zE);
    }

    private void Start()
    {
        myRigid = GetComponent<Rigidbody>();
        bodyguardSpeed = Random.Range(5f, 8f);
        StartCoroutine(MakeMove());

        transform.LookAt(cameraPos.position);
    }

	private void Update()
    {
        // ���� ������
        if (enemyState.isDie == true)
        {
            enemyState.shoot = false;
            state = State.Die;
        }

    }
    private IEnumerator MakeMove()
    {
        while(true)
        {
            // ���� Ÿ�������� �����ϸ� break
            float checkDist = Vector3.Distance(targetPos, transform.position);
            if (checkDist <= 2f)
            {
                // ��Ʋ ���·� ���� ��
                state = State.Battle;
                enemyState.shoot = true;
                // ī�޶����� �ڽ��� target�� �־��ֱ�
                cameraCtrl.AddTarget(gameObject);
                break;
            }

            // ���� ��ġ���� ī�޶� ������ ���ͼ� ����
            MoveToPosition();

            // ��� ������
            yield return new WaitForSeconds(0.05f);
        }

        // ī�޶� �ٶ󺸴� �������� 
        transform.LookAt(cameraPos.position);
        // �ѹ� ���߱�..
        myRigid.Sleep();

        // (����) ���
        while (enemyState.shoot == true)
        {
            // �ѹ� �߻��ϰ�
            gun.GetComponent<EnemyGun>().Fire();

            // �÷��̾� ���� ���̰�

            // ��� ������
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void MoveToPosition()
    {
    	//���� ����
        state = State.Move;
        
        // �̵� ���� ���� ���
        var moveDir = (targetPos - transform.position).normalized;
        moveDir.y = 0f;

        // �̵�
        transform.position += moveDir * bodyguardSpeed * Time.deltaTime;
    }
}
