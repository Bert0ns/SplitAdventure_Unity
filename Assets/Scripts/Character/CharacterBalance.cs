using UnityEngine;

public class CharacterBalance : MonoBehaviour
{
    [SerializeField] private float targetRotation;
    [SerializeField] private float force;
    private Rigidbody2D rb;
    [SerializeField] private bool isBalanceActive = true;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isBalanceActive)
        {
            rb.MoveRotation(Mathf.LerpAngle(rb.rotation, targetRotation, force * Time.deltaTime));
        }
    }
    public void DisableBalance()
    {
        isBalanceActive = false;
    }
}
