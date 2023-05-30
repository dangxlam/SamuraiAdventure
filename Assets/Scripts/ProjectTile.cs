using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTile : MonoBehaviour
{
    public int attackDamage = 10;
    public Vector2 moveSpeed = new Vector2(3f, 0);
    public Vector2 knockBack = new Vector2 (0, 0);


    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health health = collision.GetComponent<Health>();

        if (health != null )
        {
            Vector2 directionKnockback = transform.localScale.x > 0 ? knockBack : new Vector2(-knockBack.x, knockBack.y);
            //Hit target
            bool gotHit = health.Hit(attackDamage, directionKnockback);
            if (gotHit)
                Debug.Log(collision.name + " be hit by " + attackDamage);
            Destroy(gameObject);
        }
    }
}
