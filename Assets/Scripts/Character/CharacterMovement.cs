using System.Collections;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
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

    [SerializeField] private float speed = 1500f;
    [SerializeField] private float stepWait = 0.5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float handForce = 10f;
    private bool isMovingHandsDown = false;
    private bool isMovingHandsUp = false;

    [SerializeField] private float positionRadius;
    [SerializeField] private LayerMask ground;
    [SerializeField] private LayerMask jumpable;
    [SerializeField] private Transform characterPos;

    CharacterAnimationManager characterAnimationManager;
    private void Awake()
    {
        characterAnimationManager = GetComponent<CharacterAnimationManager>();
    }
    void Start()
    {
        leftLegRB = leftLeg.GetComponent<Rigidbody2D>();
        rightLegRB = rightLeg.GetComponent<Rigidbody2D>();
        bodyRB = body.GetComponent<Rigidbody2D>();
        rightHandRB = rightHand.GetComponent<Rigidbody2D>();
        leftHandRB = leftHand.GetComponent<Rigidbody2D>();
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
        characterAnimationManager.PlayAnimationIdle();
    } 
    public void CharacterTryJump()
    {
        bool isOnGround = Physics2D.OverlapCircle(characterPos.position, positionRadius, ground) || Physics2D.OverlapCircle(characterPos.position, positionRadius, jumpable);

        if (isOnGround)
        {
            float force = 0f;
            if (isMovingHandsUp)
            {
                force -= handForce;
            }
            if (isMovingHandsDown)
            {
                force += handForce;
            }

            bodyRB.AddForce(Vector2.up * (jumpForce + force));
        }
    }
    public void CharacterMoveHandsUp()
    {
        isMovingHandsUp = true;
        rightHandRB.AddForce((Vector2.up + (Vector2.right / 2 )) * handForce);
        leftHandRB.AddForce((Vector2.up + (Vector2.left / 2) ) * handForce);
    }

    public void CharacterMoveHandsDown()
    {
        isMovingHandsDown = true;
        rightHandRB.AddForce((Vector2.down + (Vector2.right / 2)) * handForce);
        leftHandRB.AddForce((Vector2.down + (Vector2.left / 2)) * handForce);
    }

    public void CharacterStopMovingHands()
    {
        isMovingHandsDown = false;
        isMovingHandsUp = false;
    }
}
