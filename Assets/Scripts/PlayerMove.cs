using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMove : MonoBehaviour
{
    //enum������ ���¸� ��������
    public enum State
	{
        Move,
        Battle
    }

    //�÷��̾� ������Ʈ�� ������ ���� ���� ����
    public GameObject player;

    //Vector3�� ����Ʈ�� ����(����: WayPoint�� ��ǥ���� �ޱ� ����)
    List<Vector3> nextStages = new List<Vector3>();
    //private WayPoint m_WayPoint;
    public State state;

    public bool isMove;

    //nextStages�� �ε����� ���� �������� ���� ����
    private int index = 1;

	private void Awake()
	{
        //�÷��̾� GameObject�� �����´�
        player = GameObject.Find("Player");
        
        //for������ nextStage ����Ʈ�� ��ǥ�� �߰��� ���ش�
        
        //nextStages.Add(GameManager.instance.m_WayPoint.m_transform[1]);
        isMove = false;
	}

	private void Start()
	{
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
        //1���� ������ isMove�� true�� ����� �÷��̾ �����̵��� �Ѵ�
        if(Input.GetKeyDown(KeyCode.Alpha1))
		{
            isMove = true;
		}

        Move();
	}

    public void Move()
	{
        //���� isMove�� true�̸� ����
        if(isMove == true)
		{
            //nextStages�� ���� null�� �ƴϸ� ����
            if (nextStages != null)
            {
                //�÷��̾� ���¸� Move�� �����Ͽ� �����δٰ� �˷��ش�
                this.state = State.Move;
                //Vector3�� MoveTowards�� �Լ��� ����� �÷��̾� ��ǥ���� ���� ���������� ��ǥ�� 10���� �ð����� �̵��ϰ� ����� �ش�
                player.transform.position = Vector3.MoveTowards(player.transform.position, nextStages[index], Time.deltaTime * 10f);

                //�÷��̾ �� ������ ã�� ���� nextStage�� ��ǥ�� �÷��̾� ��ǥ�� �� ���ѵ�
                Vector3 dir = nextStages[index] - player.transform.position;
                
                //�ؿ� Quaternion.LookRotation(dir)�� ����ؼ� �÷��̾ �� ������ �����ش�
                //Quaternion.Lerp�Լ��� ����ϸ� �ε巴�� ȸ���� �Ѵ� �׸��� Quaternion.LookRotation(dir)�� dir���࿡ ���� ���ʹϾ� ��ȸ���� �ϰ� ���ش�
                player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 5f);
            }
        }
        
        //���� �÷��̾� ��ǥ�� nextStage�� ��ǥ�� �������� ����
        if(player.transform.position == nextStages[index])
		{
            //isMove�� false�� ���� �÷��̾��� �������� �ʰ� ������ش�
            isMove = false;
            //���� index�� nextStage�� ������ ���Һ��� ������ ����
            if (nextStages.Count - 1 > index)
            {
                //�׷��� index�� ++�� �� 1�� ���� �����ش�.
                index++;
            }
		}
	}
}
