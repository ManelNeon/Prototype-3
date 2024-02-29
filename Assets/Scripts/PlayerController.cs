using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Jump")]

    [SerializeField] float jumpForce;

    [SerializeField] float gravityModifier;

    bool grounded;

    [HideInInspector] public bool gameOver;

    Rigidbody rb;

    Animator playerAnim;

    void Start()
    {
        playerAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        gameOver = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded && !gameOver)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            grounded = false;
            playerAnim.SetTrigger("Jump_trig");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over!");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
        }
    }
}
