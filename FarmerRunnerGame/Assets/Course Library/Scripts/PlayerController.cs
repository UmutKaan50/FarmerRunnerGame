using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    private AudioSource playerAudio;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public float jumpForce;
    public float gravityModifier = 10;
    public bool isOnGround = true;
    private int jumpCount = 0;


    private SpawnManager spawnManagerScript;

    // Trying to stop b.g.m. with the var. above
    private AudioSource mainCameraAudio;

    private float Score = 0;
    private float increaseAmount = 1;

    public bool gameOver = false;
    // Instead of making Rigidbody public and taking it from it's component by dragging in unity,
    // we made it private and then used GetComponent method!!!
    // Start is called before the first frame update

    // <, > : left and right carets finds type of something.
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            Score += increaseAmount / 100;
            Debug.Log("Score: " + (int)Score);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !gameOver && jumpCount <= 1 && isOnGround)
        {
            
            if (jumpCount == 1)
            {
                isOnGround = false;
                playerAnim.SetTrigger("Jump_trig");
                playerRb.AddForce(Vector3.up * jumpForce / 2, ForceMode.Impulse);
                dirtParticle.Stop();
                playerAudio.PlayOneShot(jumpSound, 1);
            }
            else
            {
                playerAnim.SetTrigger("Jump_trig");
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                dirtParticle.Stop();
                playerAudio.PlayOneShot(jumpSound, 1);                
            }
            jumpCount++;

        }

        if (Input.GetKey(KeyCode.LeftShift) ||Input.GetKey(KeyCode.RightShift) )
        {
            increaseAmount = 2;
            MoveLeft.speed = 50;
        }
        else
        {
            increaseAmount = 1;
            MoveLeft.speed = 30;
        }

        //else if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 1)
        //{
        //    playerAnim.SetTrigger("Jump_trig");
        //    jumpCount++;
        //    playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);            
        //    dirtParticle.Stop();
        //    playerAudio.PlayOneShot(jumpSound, 1);
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            jumpCount = 0;
            if (!gameOver)
            {
                dirtParticle.Play();
            }

        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over!");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1);

            // Trying to stop background sound when the game ends.

            mainCameraAudio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
            mainCameraAudio.Stop();

            // Alhamdulillah, solved!
        }
    }
}
