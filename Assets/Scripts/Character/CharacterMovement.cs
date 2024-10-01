using System.Collections;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private GameObject character;
    [SerializeField] private GameObject leftLeg;
    [SerializeField] private GameObject rightLeg;
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject rightHand;
    [SerializeField] private GameObject leftHand;
    private Rigidbody2D leftLegRB;
    private Rigidbody2D rightLegRB;
    private Rigidbody2D bodyRB;
    private Rigidbody2D rightHandRB;
    private Rigidbody2D leftHandRB;
    private CharacterAnimationManager characterAnimationManager;

    [SerializeField] private float speed = 1500f;
    [SerializeField] private float stepWait = 0.5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float handForce = 10f;

    [SerializeField] private float positionRadius;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform characterPos;

    void Start()
    {
        leftLegRB = leftLeg.GetComponent<Rigidbody2D>();
        rightLegRB = rightLeg.GetComponent<Rigidbody2D>();
        bodyRB = body.GetComponent<Rigidbody2D>();
        rightHandRB = rightHand.GetComponent<Rigidbody2D>();
        leftHandRB = leftHand.GetComponent<Rigidbody2D>();
        characterAnimationManager = character.GetComponent<CharacterAnimationManager>();
    }

    private IEnumerator MoveRight(float seconds)
    {
        leftLegRB.AddForce(Vector2.right * speed * Time.deltaTime);
        yield return new WaitForSeconds(seconds);
        rightLegRB.AddForce(Vector2.right * speed * Time.deltaTime);
    }
    private IEnumerator MoveLeft(float seconds)
    {
        rightLegRB.AddForce(Vector2.left * speed * Time.deltaTime);
        yield return new WaitForSeconds(seconds);
        leftLegRB.AddForce(Vector2.left * speed * Time.deltaTime);
    }

    public void CharacterMoveRight()
    {
        characterAnimationManager.PlayAnimationWalkRight();
        StartCoroutine(MoveRight(stepWait));
    }
    public void CharacterMoveLeft()
    {
        characterAnimationManager.PlayAnimationWalkLeft();
        StartCoroutine(MoveLeft(stepWait));
    }
    public void CharacterMoveIdle()
    {
        if(characterAnimationManager != null)
        {
            characterAnimationManager.PlayAnimationIdle();
        } 
    } 
    public void CharacterTryJump()
    {
        bool isOnGroud = Physics2D.OverlapCircle(characterPos.position, positionRadius, ground);
        if (isOnGroud)
        {
            bodyRB.AddForce(Vector2.up * jumpForce);
        }
    }
    public void CharacterMoveHandsUp()
    {
        rightHandRB.AddForce((Vector2.up + (Vector2.right / 2 )) * handForce);
        leftHandRB.AddForce((Vector2.up + (Vector2.left / 2) ) * handForce);
    }

    public void CharacterMoveHandsDown()
    {
        rightHandRB.AddForce((Vector2.down + (Vector2.right / 2)) * handForce);
        leftHandRB.AddForce((Vector2.down + (Vector2.left / 2)) * handForce);
    }

    public void CharacterStopMovingHands()
    {
        rightHandRB.velocity = Vector2.zero;
        leftHandRB.velocity = Vector2.zero;
    }
}
