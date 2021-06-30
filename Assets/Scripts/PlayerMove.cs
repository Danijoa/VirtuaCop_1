using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMove : MonoBehaviour
{
    //�÷��̾� ������Ʈ�� ������ ���� ���� ����
    public PlayerInformation m_PlayerInfo;

    //Vector3�� ����Ʈ�� ����(����: WayPoint�� ��ǥ���� �ޱ� ����)
    List<Vector3> nextStages = new List<Vector3>();
    //��� �ֳʹ̸� ��� ����Ʈ ����
    public List<GameObject> m_Enemy;
    //private WayPoint m_WayPoint;

    // ī�޶� ������
    public CameraCtrl cameraCtrl;
    
    //nextStages�� �ε����� ���� �������� ���� ����
     public int index = 1;

    //
    public GameObject makeBoss;

    private void Awake()
	{
        //�÷��̾� GameObject�� �����´�
        m_PlayerInfo = gameObject.GetComponent<PlayerInformation>();

        //for������ nextStage ����Ʈ�� ��ǥ�� �߰��� ���ش�
        
        //nextStages.Add(GameManager.instance.m_WayPoint.m_transform[1]);
       	m_PlayerInfo.isMove = false;
        m_PlayerInfo.isRotate = false;
	}

	private void Start()
	{
        //cameraCtrl = get
        for (int i = 0; i < GameManager.instance.m_WayPoint.points.Count; i++)
        {
            //..nextStages[i] = GetComponent<Transform>();
            float x = GameManager.instance.m_WayPoint.points[i].x;
            float y = GameManager.instance.m_WayPoint.points[i].y;
            float z = GameManager.instance.m_WayPoint.points[i].z;
            //Transform pos = GetComponent<Transform>();
            Vector3 pos = new Vector3(x, y, z);
            nextStages.Add(pos);
        }
    }
	private void Update()
	{
        Move();
	}

    public void Move()
	{
        if (m_PlayerInfo.isRotate)
        {
            //�÷��̾ �� ������ ã�� ���� nextStage�� ��ǥ�� �÷��̾� ��ǥ�� �� ���ѵ�
            Vector3 dir = nextStages[index] - m_PlayerInfo.gameObject.transform.position;

            //�ؿ� Quaternion.LookRotation(dir)�� ����ؼ� �÷��̾ �� ������ �����ش�
            //Quaternion.Lerp�Լ��� ����ϸ� �ε巴�� ȸ���� �Ѵ� �׸��� Quaternion.LookRotation(dir)�� dir���࿡ ���� ���ʹϾ� ��ȸ���� �ϰ� ���ش�
            Quaternion q = Quaternion.LookRotation(dir);

            m_PlayerInfo.gameObject.transform.rotation =
                Quaternion.RotateTowards(m_PlayerInfo.gameObject.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 120f);
            //Debug.Log("1 : " + dir);
            //Debug.Log("2 : " + q.eulerAngles);
            //Debug.Log("3 : " + m_PlayerInfo.gameObject.transform.rotation);

            if (m_PlayerInfo.gameObject.transform.rotation == q)
            {
                m_PlayerInfo.isRotate = false;
            }
        }

        //���� isMove�� true�̸� ����
        if(m_PlayerInfo.isRotate == false && m_PlayerInfo.isMove == true)
		{
            //nextStages�� ���� null�� �ƴϸ� ����
            if (nextStages != null)
            {
                //�÷��̾� ���¸� Move�� �����Ͽ� �����δٰ� �˷��ش�
                 m_PlayerInfo.state = PlayerInformation.State.Move;

                //Debug.Log("index" + index);

                //Debug.Log("nextStages[index]: " + nextStages[index]);

                //Vector3�� MoveTowards�� �Լ��� ����� �÷��̾� ��ǥ���� ���� ���������� ��ǥ�� 10���� �ð����� �̵��ϰ� ����� �ش�
                m_PlayerInfo.gameObject.transform.position = Vector3.MoveTowards(m_PlayerInfo.gameObject.transform.position, nextStages[index], Time.deltaTime * 10f);
            }
        }
        
        //���� �÷��̾� ��ǥ�� nextStage�� ��ǥ�� �������� ����
        if(m_PlayerInfo.gameObject.transform.position == nextStages[index])
		{
            //isMove�� false�� ���� �÷��̾��� �������� �ʰ� ������ش�
            m_PlayerInfo.isMove = false;

            m_PlayerInfo.isRotate = true;
            //���� index�� nextStage�� ������ ���Һ��� ������ ����
            if (nextStages.Count - 1 > index)
            {
                //�׷��� index�� ++�� �� 1�� ���� �����ش�.
                index++;
            }
            else
            {
                Debug.Log("index is not right");
            }
        }

        if (index == 13 && m_PlayerInfo.isMove == true)
        {
            makeBoss.SetActive(true);
        }

        //if(Input.GetKeyDown("o"))
        //    makeBoss.SetActive(true);
    }

}
