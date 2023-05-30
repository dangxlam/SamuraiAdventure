using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource loopSource;
    // Start is called before the first frame update
    void Start()
    {
        loopSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
