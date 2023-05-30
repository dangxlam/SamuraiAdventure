using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageHit;
    public UnityEvent<int, int> healthChange;
    
    [SerializeField]
    private int _maxHealth = 100;

    Animator animator;

    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }
    [SerializeField]
    private int _health = 100;

    public int HealthLogic
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            healthChange?.Invoke(_health, MaxHealth);

            //If health below 0, character dead
            if(_health <= 0)
            {
                IsAlive = false;
                
            }
        }
    }

    [SerializeField]
    private bool _isAlive = true;
    [SerializeField]
    private bool isInvincible = false;

    public bool IsHit { get
        {
            return animator.GetBool(AnimationStrings.isHit);
        }
        private set 
        { 
            animator.SetBool(AnimationStrings.isHit, value);
        } 
    }

    private float timeSinceHit = 0;
    public float invincibleTime = 0.25f;

    public bool IsAlive { get
        {
            return _isAlive;
        } private set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, IsAlive);
            Debug.Log("Is Alive set" + value);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(isInvincible)
        {
            if(timeSinceHit > invincibleTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }

        //Hit(10);
    }


    public bool Hit(int damage, Vector2 knockBack)
    {
        if(IsAlive && !isInvincible)
        {
            HealthLogic -= damage;
            isInvincible = true;

            //Notify other components
            IsHit = true;
            damageHit?.Invoke(damage, knockBack);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);
            return true;
        }

        //Can not hit
        return false;
    }

    public bool Heal(int healAmount)
    {
        if(IsAlive && HealthLogic < MaxHealth)
        {
            int maxHeal = Mathf.Max(MaxHealth - HealthLogic, 0);
            int actualHeal = Mathf.Min(maxHeal, healAmount);

            HealthLogic += actualHeal;

            CharacterEvents.characterHealed(gameObject, actualHeal);
            return true;
        }

        return false;
    }

    
}
