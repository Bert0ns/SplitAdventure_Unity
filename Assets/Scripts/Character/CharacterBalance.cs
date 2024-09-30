using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBalance : MonoBehaviour
{
    [SerializeField] private float targetRotation;
    [SerializeField] private float force;
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.MoveRotation(Mathf.LerpAngle(rb.rotation, targetRotation, force * Time.deltaTime));
    }
}
