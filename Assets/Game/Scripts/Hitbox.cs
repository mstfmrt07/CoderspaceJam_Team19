using System;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : ExecuteOnCollision
{
    public int initialHealth;
    public List<LayerMaskDamage> damageLayerPairs;

    public Action<int> OnGetDamage;
    public Action OnDestroy;

    private int currentHealth;
    private bool destroyed = false;
    public int CurrentHealth => currentHealth;

    protected override void Awake()
    {
        base.Awake();
        currentHealth = initialHealth;
    }

    protected override void HandleCollisionEnter(Collider2D collider)
    {
        foreach (var pair in damageLayerPairs)
        {
            if (pair.layers.Includes(collider.gameObject.layer))
            {
                GetDamage(pair.damage);
                break;
            }
        }
    }

    protected override void HandleCollisionExit(Collider2D collider)
    {
    }

    private void GetDamage(int damage)
    {
        if (destroyed)
            return;

        currentHealth -= damage;

        OnGetDamage?.Invoke(damage);

        if (currentHealth <= 0)
        {
            destroyed = true;
            OnDestroy?.Invoke();
        }
    }
}

//Defines which layer will give how much damage to the hitbox.
[System.Serializable]
public struct LayerMaskDamage
{
    public LayerMask layers;
    public int damage;
}