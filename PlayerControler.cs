using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour 
{
    private GameController  _GameController;

    private Rigidbody2D     playerRb;
    private Animator        playerAnimator;

    public float            speed;
    public float            jumpforce;

    public bool             islookleft;

    public Transform        groundCheck;
    private bool            isGrounded;
    private bool            isAtack;

    public Transform        mao;
   
    public GameObject       HitBoxPrefab;

        
    void Start()
    {
            playerRb = GetComponent<Rigidbody2D>();
            playerAnimator = GetComponent<Animator>();
            
            _GameController = FindObjectOfType(typeof(GameController)) as GameController;
            
            _GameController.playerTransform = this.transform;        
    }

        
        void Update()
        {
            float h = Input.GetAxisRaw("Horizontal");

            if(isAtack == true && isGrounded == true) 
            {
                h = 0;
            }

            if (h > 0 && islookleft)
            {
                Flip();
            }
            else if (h < 0 && !islookleft)
            {
                Flip();
            }        

            float speedY = playerRb.velocity.y;

            if (Input.GetButtonDown("Jump") && isGrounded == true)
            {
                _GameController.playSFX(_GameController.sfxJump, 0.1f);
                playerRb.AddForce(new Vector2(0, jumpforce));
            }

            if(Input.GetButtonDown("Fire1") && isAtack == false)
            {
                isAtack = true;
                _GameController.playSFX(_GameController.sfxAtack, 0.1f);            
                playerAnimator.SetTrigger("atack");
            }

            playerRb.velocity = new Vector2(h * speed, speedY);
            playerAnimator.SetInteger("h", (int) h);
            playerAnimator.SetBool("isGrounded", isGrounded);
            playerAnimator.SetFloat("speedY", speedY);
            playerAnimator.SetBool("isAtack", isAtack);
        }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f);    
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Coletavel")
        {
            _GameController.playSFX(_GameController.sfxCoin, 0.2f);
            Destroy(col.gameObject);
        } 
        
        else if (col.gameObject.tag == "damage") 
        {
            print("DANO!");
        }


    }
    void Flip()
    {
        islookleft = !islookleft;
 
        float x = transform.localScale.x * -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }
    void OnEndAtack() 
    {
        isAtack = false;    
    }

    void HitBoxAtack() 
    {
        GameObject HitBoxTemp = Instantiate(HitBoxPrefab, mao.position, transform.localRotation);
        Destroy(HitBoxTemp, 0.2f);
    }

    public void footStep()
    {
        _GameController.playSFX(_GameController.sfxStep[Random.Range(0, _GameController.sfxStep.Length)], 0.1f);
    }
}