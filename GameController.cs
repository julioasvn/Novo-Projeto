using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Camera      cam;
    public Transform    playerTransform;

    public Transform    LimCamDir, LimCamEsq, LimCamSup, LimCamInf;
    public float        speedCam;

    //[Header("Audio")]
    public AudioSource  sfxSource;
    public AudioSource  musicSource;
    public AudioClip    sfxJump;
    public AudioClip    sfxAtack;
    public AudioClip    sfxCoin;
    public AudioClip[]  sfxStep;
    
    void Start()
    {
        cam = Camera.main;
    }
    
    void Update()
    {
        
    }

    void LateUpdate()
    {
        CamController();
    }

    void CamController()
    { 
        float posCamX = playerTransform.position.x;
        float posCamY = playerTransform.position.y + 1.0f;

        if(cam.transform.position.x < LimCamEsq.position.x && playerTransform.position.x < LimCamEsq.position.x) 
        {
           posCamX = LimCamEsq.position.x; 
        }
        
        if (cam.transform.position.x > LimCamDir.position.x && playerTransform.position.x > LimCamDir.position.x)
        {
            posCamX = LimCamDir.position.x;
        }

        if (cam.transform.position.y < LimCamInf.position.x && playerTransform.position.y < LimCamInf.position.y)
        {
            posCamY = LimCamInf.position.y;
        }

        if (cam.transform.position.y > LimCamSup.position.y && playerTransform.position.y > LimCamSup.position.y)
        {
            posCamY = LimCamSup.position.y;
        }

        Vector3 posCam = new Vector3(posCamX, posCamY, cam.transform.position.z);

        cam.transform.position = Vector3.Lerp(cam.transform.position, posCam, speedCam * Time.deltaTime);
    }

    public void playSFX(AudioClip sfxClip, float volume)
    {
        sfxSource.PlayOneShot(sfxClip, volume); 
    }    
}
