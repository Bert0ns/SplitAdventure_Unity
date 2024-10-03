using UnityEngine;

[RequireComponent (typeof(CharacterLife))]
public class CharacterCollisionController : MonoBehaviour
{
    private bool isCollisionEnabled = true;
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
        if (!isCollisionEnabled)
        {
            return;
        }

        if(collision2D.collider.tag == "Head")
        {
            collision2D.gameObject.GetComponentInParent<CharacterLife>().ChangeLifePoints(-1);
        }
    }

    public void DisableCollisionTriggerWithPlayers()
    {
        isCollisionEnabled = false;
    }
}
