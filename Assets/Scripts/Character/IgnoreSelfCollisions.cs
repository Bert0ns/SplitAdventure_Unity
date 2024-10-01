using UnityEngine;

public class IgnoreSelfCollisions : MonoBehaviour
{
    void Start()
    {
        var colliders = GetComponentsInChildren<Collider2D>();
        for(int i = 0; i < colliders.Length; i++)
        {
            for (int k = 0; k < colliders.Length; k++)
            {
                Physics2D.IgnoreCollision(colliders[i], colliders[k]);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
        }
    }
}
