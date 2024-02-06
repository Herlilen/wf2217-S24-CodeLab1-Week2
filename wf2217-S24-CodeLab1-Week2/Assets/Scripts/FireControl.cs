using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireControl : MonoBehaviour
{
    [Header("Fire Particle")] 
    public ParticleSystem fire;
    public ParticleSystem fire2;
    public ParticleSystem fire3;
    public ParticleSystem light;
    //set particle fade in with alpha value;
    [Range(0.0f, 1.0f)]
    public float particleAlpha;
    public AudioSource _audio;

    [Header("Fire Control")] 
    public float heat;
    public float heatDecay;
    public float heatIncrease;
    
    // Start is called before the first frame update
    void Start()
    {
        particleAlpha = 1;
        _audio = GetComponent<AudioSource>();
        heat = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //set color 
        var color = fire.startColor;
        color.a = particleAlpha;
        fire.startColor = color;
        fire2.startColor = color;
        fire3.startColor = color;
        light.startColor = color;
        
        //set volume
        _audio.volume = particleAlpha;
        
        //heat decay;
        if (heat > 0)
        {
            heat -= heatDecay * Time.deltaTime;
        }
        if (heat <= 0)
        {
            heat = 0;
        }
        //increase heat by space
        if (Input.GetKeyDown(KeyCode.Space) && heat < 1)
        {
            heat += heatIncrease;
        }
        if (heat >= 1)  //cap heat max to be 1
        {
            heat = 1;
        }
        
        //set alpha equals to heat
        particleAlpha = heat;
    }
}
