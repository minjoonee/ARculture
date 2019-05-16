using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_AudioPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void SoundPlay()
    {
        this.audioSource.PlayOneShot(this.clip);
        Debug.Log("음악파일 재생");
    }
}
