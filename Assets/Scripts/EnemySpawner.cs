using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // �� ������ ����
    public GameObject enemyPrefab;

    // �� ��� ����Ʈ
    public List<GameObject> enemies = new List<GameObject>();

    // ����� �� ��ġ �޾ƿ���
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
        // �� ������ ����
        GameObject enemy = Instantiate(enemyPrefab,
            new Vector3(enemyPositions[index].xStart, 0f, enemyPositions[index].zStart),
            Quaternion.identity);

        // ��ġ ����ֱ�
        enemy.GetComponent<EnemyMovement>().SetUp(enemyPositions[index].xStart, enemyPositions[index].zStart, enemyPositions[index].xEnd, enemyPositions[index].zEnd);
    }
}
