using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    private AudioSource _explodeSound;
    // Start is called before the first frame update
    void Start()
    {
        _explodeSound = GameObject.Find("ExplodeSound").GetComponent<AudioSource>();
        _explodeSound.Play();
        Destroy(this.gameObject,2.8f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
