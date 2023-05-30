using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealFood : MonoBehaviour
{
    public int healAmount = 20;
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);

    AudioSource healSound;

    private void Awake()
    {
        healSound = GetComponent<AudioSource>();
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health health = collision.GetComponent<Health>();

        if(health)
        {
            bool wasHealed = health.Heal(healAmount);
            if(wasHealed)
            {
                if (healSound != null)
                    AudioSource.PlayClipAtPoint(healSound.clip, gameObject.transform.position, healSound.volume);
                Destroy(gameObject);
            }
            
        }
    }
}
