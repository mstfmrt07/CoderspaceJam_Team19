using UnityEngine;

public class Player : MSingleton<Player>
{
    [Header("References")]
    public CharacterAnimator animator;
    public Rigidbody2D rigidBody2D;

    [Header("Values")]
    public float maxHP;

    private bool isDead;
    private float currentHP;

    public float CurrentHP => currentHP;
    public bool IsDead => isDead;

    private void Awake()
    {
        currentHP = maxHP;
    }

    public void GetDamage(float damage)
    {
        //Animate
        currentHP -= damage;

        Debug.Log(gameObject.name + ", HP: " + currentHP);

        if (currentHP <= 0)
        {
            Die();
            Debug.Log(gameObject.name + " died.");
        }
    }

    public void Die()
    {
        isDead = true;
        animator.PlayAnim(AnimationState.ANGEL);

        Destroy(GetComponent<Rigidbody2D>());
        GetComponent<Collider2D>().enabled = false;
    }
}
