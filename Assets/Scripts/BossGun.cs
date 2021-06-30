using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGun : MonoBehaviour
{
    public ParticleSystem flashEffect; // 총구 화염 효과

    private AudioSource shotAudio; // 총 소리 재생기
    public AudioClip shotClip; // 발사 소리

    //플레이어 정보
    private PlayerHp playerHp;

    private int index = 0;

    private void Awake()
    {
        shotAudio = GetComponent<AudioSource>(); 
        playerHp = GameObject.Find("Player").GetComponent<PlayerHp>();
    }

    public void Fire()
    {
        // 이펙트와 소리
        flashEffect.Play();
        shotAudio.PlayOneShot(shotClip);

        // 플레이어 hp 줄이기
        playerHp.PlayerHit(1);

    }
}
