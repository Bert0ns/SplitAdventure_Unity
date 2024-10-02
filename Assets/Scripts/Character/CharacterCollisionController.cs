using UnityEngine;

public class CharacterCollisionController : MonoBehaviour
{
    private void Start()
    {
        var colliders = GetComponentsInChildren<Collider2D>();
        for (int i = 0; i < colliders.Length; i++)
        {
            for (int k = 0; k < colliders.Length; k++)
            {
                Physics2D.IgnoreCollision(colliders[i], colliders[k]);
            }
        }
    }

    public void OnChildCollisionEnter2D(Collision2D collision2D)
    {
        Debug.Log("Collision detected: " +  collision2D.collider.tag + ", " + collision2D.otherCollider.tag);
    
        if(collision2D.collider.tag == "Head")
        {
            Debug.Log("Other Head touched");
        }
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
        }
    }*/
}
