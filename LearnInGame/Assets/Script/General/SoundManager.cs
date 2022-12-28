using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioClip explosion;
    public AudioSource Audio;
    public AudioSource BGMSource; 
    public void Awake()
    {
        if (SoundManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public void Update()
    {
        if (GameManager.instance.boss.alive==false || GameManager.instance.player.alive == false)
            BGMSource.Stop();
        
    }

}
