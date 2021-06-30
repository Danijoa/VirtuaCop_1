using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public ParticleSystem flashEffect; // 총구 화염 효과

    private AudioSource shotAudio; // 총 소리 재생기
    public AudioClip shotClip; // 발사 소리

    //플레이어 정보
    private PlayerHp m_PlayerHp;
    private CameraCtrl m_CamerCtrl;

    private int index = 0;

    private void Awake()
    {
        shotAudio = GetComponent<AudioSource>();
        m_PlayerHp = GameObject.Find("Player").GetComponent<PlayerHp>();
        m_CamerCtrl = GameManager.instance.m_Camera.GetComponent<CameraCtrl>();
    }

    public void Fire()
    {
        // 이펙트와 소리
        flashEffect.Play();
        shotAudio.PlayOneShot(shotClip);

       if (m_CamerCtrl.m_Targets[index].gameObject == this.transform.parent.gameObject) // 타겟 한 대상만 영향 주도록 
       {
           // 플레이어 생명 줄이고
           m_PlayerHp.PlayerHit(1);
       } 
    }
}
