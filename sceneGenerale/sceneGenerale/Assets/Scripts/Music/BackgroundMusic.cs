using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{

    public AudioClip[] musics;
    private AudioSource source;
    private float timebreak = 5.0f;
    private float volume = 4;
    private float volumeBeach = 1;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        StartCoroutine(PlayRandomMusic());
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PlayRandomMusic ()
    {
        while (true)
        {
            
            yield return (new WaitForSeconds(timebreak));
            source.clip = musics[Random.Range(0, musics.Length)];
            if (source.clip.name == "Beach")
            {
                source.PlayOneShot(source.clip, volumeBeach);
            }
            else source.PlayOneShot(source.clip,volume);
            yield return (new WaitForSeconds(source.clip.length + timebreak * 2));
            
        }
    }
}
