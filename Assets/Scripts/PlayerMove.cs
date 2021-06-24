using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMove : MonoBehaviour
{
    //enum문으로 상태를 나누었다
    public enum State
	{
        Move,
        Battle
    }

    //플레이어 오브젝트를 가지고 오기 위해 선언
    public GameObject player;

    //Vector3를 리스트로 선언(이유: WayPoint의 좌표값을 받기 위해)
    List<Vector3> nextStages = new List<Vector3>();
    //private WayPoint m_WayPoint;
    public State state;

    public bool isMove;

    //nextStages의 인덱스를 각각 가져오기 위해 선언
    private int index = 1;

	private void Awake()
	{
        //플레이어 GameObject를 가져온다
        player = GameObject.Find("Player");
        
        //for문으로 nextStage 리스트에 좌표를 추가를 해준다
        
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
        //1번을 누르면 isMove를 true를 만들어 플레이어가 움직이도록 한다
        if(Input.GetKeyDown(KeyCode.Alpha1))
		{
            isMove = true;
		}

        Move();
	}

    public void Move()
	{
        //만약 isMove가 true이면 실행
        if(isMove == true)
		{
            //nextStages의 값이 null이 아니면 실행
            if (nextStages != null)
            {
                //플레이어 상태를 Move로 변경하여 움직인다고 알려준다
                this.state = State.Move;
                //Vector3의 MoveTowards의 함수를 사용해 플레이어 좌표에서 다음 스테이지의 좌표로 10초의 시간으로 이동하게 만들어 준다
                player.transform.position = Vector3.MoveTowards(player.transform.position, nextStages[index], Time.deltaTime * 10f);

                //플레이어가 갈 방향을 찾기 위해 nextStage의 좌표와 플레이어 좌표를 빼 구한뒤
                Vector3 dir = nextStages[index] - player.transform.position;
                
                //밑에 Quaternion.LookRotation(dir)를 사용해서 플레이어가 볼 방향을 구해준다
                //Quaternion.Lerp함수를 사용하면 부드럽게 회전을 한다 그리고 Quaternion.LookRotation(dir)은 dir방행에 따른 쿼터니언 축회전을 하게 해준다
                player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 5f);
            }
        }
        
        //만약 플레이어 좌표가 nextStage의 좌표랑 같아지면 실행
        if(player.transform.position == nextStages[index])
		{
            //isMove를 false로 변경 플레이어의 움직이지 않게 만들어준다
            isMove = false;
            //만약 index가 nextStage의 마지막 원소보다 작으면 실행
            if (nextStages.Count - 1 > index)
            {
                //그래서 index를 ++로 값 1을 증가 시켜준다.
                index++;
            }
		}
	}
}
