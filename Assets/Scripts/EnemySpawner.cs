using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // 적 프리팹 연결
    public EnemyMovement enemyPrefab;

    // 적 담는 리스트
    public List<EnemyMovement> enemies = new List<EnemyMovement>();

    // 저장된 적 위치 받아오기
    EnemyPositionCtrl enemyPosCtrl;
    public EnemyPositionCtrl enemyPositionCtrl { get => enemyPosCtrl; }
    List<PositionData> enemyPositions;

    int index = 0;

    private void Awake()
    {
        enemyPosCtrl = GetComponent<EnemyPositionCtrl>();
    }

    void Start()
    {
        enemyPosCtrl.LoadData();
        enemyPositions = enemyPosCtrl.enemyPositionDatas;
    }

    void Update()
    {
        if (index < 3)
        {
            CreateEnemy(index);
            index++;
        }
    }

    private void CreateEnemy(int index)
    {
        // 적 프리팹 생성
        EnemyMovement enemy = Instantiate(enemyPrefab,
            new Vector3(enemyPositions[index].xStart, 0f, enemyPositions[index].zStart),
            Quaternion.identity);

        // 위치 잡아주기
        enemy.SetUp(enemyPositions[index].xStart, enemyPositions[index].zStart, enemyPositions[index].xEnd, enemyPositions[index].zEnd);
    }
}
