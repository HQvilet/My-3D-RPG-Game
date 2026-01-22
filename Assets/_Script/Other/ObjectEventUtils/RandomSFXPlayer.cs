using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSFXPlayer : MonoBehaviour
{
    [SerializeField] List<AudioClip> audios;
    [SerializeField] float randomTime = 10f;

    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        time = randomTime + Random.Range(-10f, 8f);
    }

    float time;
    void Update()
    {
        time -= Time.deltaTime;
        if(time <= 0)
        {
            time = randomTime + Random.Range(-10f, 8f);
            if(audios.Count > 0)
            {
                audioSource.clip = audios[Random.Range(0, audios.Count - 1)];
                audioSource.Play();
            }
            
        }
    }

}
