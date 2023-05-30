using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    Collider2D attackCollider;
    public int attackDamage  = 10;
    public Vector2 knockBack = Vector2.zero;

    private void Awake()
    {
        attackCollider = GetComponent<Collider2D>();
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //See if can be hit
        Health health = collision.GetComponent<Health>();

        if (health != null)
        {
            Vector2 directionKnockback = transform.parent.localScale.x > 0 ? knockBack : new Vector2(-knockBack.x, knockBack.y);
            //Hit target
            bool gotHit = health.Hit(attackDamage, directionKnockback);
            if(gotHit)
                Debug.Log(collision.name + " be hit by "  + attackDamage);
        }
    }
}
