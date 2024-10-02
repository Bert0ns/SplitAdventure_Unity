using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartCollisionController : MonoBehaviour
{
    [SerializeField] private GameObject character;
    private CharacterCollisionController charCollController;
    [SerializeField] private bool isBodyPartHead = false;
    void Start()
    {
        charCollController = character.GetComponent<CharacterCollisionController>();
        if(this.gameObject.tag == "Head")
        {
            isBodyPartHead = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isBodyPartHead)
        {
            charCollController.OnChildCollisionEnter2D(collision);
        }
    }
}
