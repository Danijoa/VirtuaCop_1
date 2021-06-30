using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public bool isDie = false;
    public int state = 0;
    public bool shoot = false;
    private Animator enemyAnimator;

    private void Awake()
    {
        state = 1; // 0~1
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator = gameObject.GetComponent<Animator>();
        enemyAnimator.SetInteger("State", state);
        enemyAnimator.SetBool("Shoot", shoot);
    }

    // Update is called once per frame
    void Update()
    {
        if (shoot == true)
        {
            enemyAnimator.SetBool("Shoot", shoot);
        }    

        if (Input.GetKeyDown("d") || isDie == true)
        {
            isDie = true;
            enemyAnimator.SetBool("Die", isDie);
        }
    }
}
