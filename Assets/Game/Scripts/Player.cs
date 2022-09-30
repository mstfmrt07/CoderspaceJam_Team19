using UnityEngine;

public class Player : MSingleton<Player>
{
    [Header("References")]
    public Animator animator;

    [Header("Values")]
    public int coins;
    public float maxHP;
    public bool isDead;

    private float currentHP;

    public float CurrentHP => currentHP;

    private void Awake()
    {
        currentHP = maxHP;
    }

    public void GetDamage(float damage)
    {
        animator.SetTrigger("Hurt");
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
        animator.SetTrigger("Death");

        Destroy(GetComponent<Rigidbody2D>());
        GetComponent<Collider2D>().enabled = false;
    }
}
