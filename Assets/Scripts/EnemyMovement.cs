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

    // ī�޶�, �÷��̾�, �� ��ġ �޾ƿ���
    Transform playerPos;
    Transform cameraPos;
    public Transform gunPos;



    // ī�޶� ����
    private CameraCtrl cameraCtrl;
    
	// ����� ��
    public GameObject gun;

    public ItemDrop m_ItemDrop;

    //�� ����
    public EnemyState enemyState;
    public State state { get; set; }

    // �� 
    float bodyguardSpeed;
    Vector3 targetPos;

    bool isItemDrop = false;

    private void Awake()
    {
        cameraPos = GameObject.Find("Camera").GetComponent<Transform>();
        
        enemyState = GetComponent<EnemyState>();

        m_ItemDrop = GetComponent<ItemDrop>();

        state = State.Move;
    }

    public void SetUp(float xS, float zS, float xE, float zE, float yP)
    {
        playerPos = GameObject.Find("Player").GetComponent<Transform>();
        cameraCtrl = GameManager.instance.m_Camera.GetComponent<CameraCtrl>();

        if (yP != 0)
        {
            // �� ���� ��ġ
            transform.position = new Vector3(xS, playerPos.position.y + yP, zS);

            // �� ���� ��ġ
            targetPos = new Vector3(xE, playerPos.position.y + yP, zE);
        }
        else
        {
            transform.position = new Vector3(xS, playerPos.position.y, zS);

            targetPos = new Vector3(xE, playerPos.position.y, zE);
        }
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
            Die();

            if(isItemDrop == false)
            {
                m_ItemDrop.Drop(this.transform);
                isItemDrop = true;  
            }
        }

    }
    private IEnumerator MakeMove()
    {
        while(true)
        {
            // ���� Ÿ�������� �����ϸ� break
            float checkDist = Vector3.Distance(targetPos, transform.position);
            if (checkDist <= 1.5f)
            {

                // ��Ʋ ���·� ���� ��
                state = State.Battle;
                enemyState.shoot = true;
                // ī�޶����� �ڽ��� target�� �־��ֱ�
                if(state == State.Battle)
				{
                    cameraCtrl.AddTarget(gameObject);
                    //cameraCtrl.m_EnemyMovemrnts.Add(gameObject.GetComponent<EnemyMovement>());
                }

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

    public void Die()
	{
       

        enemyState.shoot = false;
        state = State.Die;
        enemyState.isDie = true;
       
        //cameraCtrl.m_Targets.Remove(gameObject);
        //gameObject.SetActive(false);
        StartCoroutine(MakeSetFalse());
    }

    private IEnumerator MakeSetFalse()
    {
        yield return new WaitForSeconds(10f);
        gameObject.SetActive(false);        
    }
}
