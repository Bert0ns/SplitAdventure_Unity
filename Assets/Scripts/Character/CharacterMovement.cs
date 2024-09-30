using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject leftLeg;
    [SerializeField] private GameObject rightLeg;
    [SerializeField] private GameObject body;
    private Rigidbody2D leftLegRB;
    private Rigidbody2D rightLegRB;
    private Rigidbody2D bodyRB;

    [SerializeField] private float speed = 1500f;
    [SerializeField] private float stepWait = 0.5f;
    [SerializeField] private float jumpForce = 10f;

    private bool isOnGroud;
    [SerializeField] private float positionRadius;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform characterPos;

    void Start()
    {
        leftLegRB = leftLeg.GetComponent<Rigidbody2D>();
        rightLegRB = rightLeg.GetComponent<Rigidbody2D>();
        bodyRB = body.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            if(Input.GetAxisRaw("Horizontal") > 0)
            {
                anim.Play("Character_WalkRight");
                StartCoroutine(MoveRight(stepWait));
            }
            else
            {
                anim.Play("Character_WalkLeft");
                StartCoroutine(MoveLeft(stepWait));
            }
        }
        else
        {
            anim.Play("Character_Idle");
        }

        isOnGroud = Physics2D.OverlapCircle(characterPos.position, positionRadius, ground);
        if (isOnGroud && Input.GetKeyDown(KeyCode.Space))
        {
            bodyRB.AddForce(Vector2.up * jumpForce);
        }
    }

    IEnumerator MoveRight(float seconds)
    {
        leftLegRB.AddForce(Vector2.right * speed * Time.deltaTime);
        yield return new WaitForSeconds(seconds);
        rightLegRB.AddForce(Vector2.right * speed * Time.deltaTime);
    }

    IEnumerator MoveLeft(float seconds)
    {
        rightLegRB.AddForce(Vector2.left * speed * Time.deltaTime);
        yield return new WaitForSeconds(seconds);
        leftLegRB.AddForce(Vector2.left * speed * Time.deltaTime);
    }
}
