using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colourChangePortal : MonoBehaviour
{
    
    
    public SpriteRenderer body;
    public GameObject flash;
    public AudioSource audioSource;
    public AudioClip portal;
    public AudioClip end;
    public AudioClip hurt;
    public AudioClip samePlanet;
    public AudioClip f;
    public AudioClip wa;
    public AudioClip w;
     public AudioClip shockk;



    // Start is called before the first frame update
    void Start()
    {
        body  = transform.GetChild(0).GetComponent<SpriteRenderer>();
        flash.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void randomColour(Color color){
        
        body.color=color;
    }
    public void setFlash(){
        flash.SetActive(true);
        Invoke("setNoFlash", 0.5f);
    }
    public void setNoFlash(){
        flash.SetActive(false);
    }
    public void endgame(){
        for(int i=0; i<3; i++)
        Invoke("setFlash", 0.2f);
    }
    public void portalMusic(){
        audioSource.PlayOneShot(portal);
    }
    public void endMusic(){
        audioSource.PlayOneShot(end);
    }
    public void diffPlanetMusic(){
        audioSource.PlayOneShot(hurt);
    }
    public void samePlanetMusic(){
        audioSource.PlayOneShot(samePlanet);
    }
    public void fireball(){
        audioSource.PlayOneShot(f);
    }
     public void water(){
        audioSource.PlayOneShot(wa);
    }
     public void wind(){
        audioSource.PlayOneShot(w);
    }
    public void shock(){
        audioSource.PlayOneShot(shockk);
    }
    // void OnTriggerEnter2D(Collider2D other) {
    //     if(other.tag=="Portal")
    //     {audioSource.PlayOneShot(portal);
    //     Debug.Log("HIT");}
    //     if(other.tag=="Enemy")
    //     audioSource.PlayOneShot(end);
    // }
}
