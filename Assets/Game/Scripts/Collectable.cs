using UnityEngine;

public class Collectable : ExecuteOnCollision, IDisposable
{
    public CurrencyData data;
    public Animator animator;
    public float collectAnimDuration;

    protected override void HandleCollisionEnter(Collider2D collider)
    {
        CurrencyController.Instance.AddToCurrency(data, 1);
        animator.SetTrigger("Action");
        Dispose();
    }

    protected override void HandleCollisionExit(Collider2D collider)
    {
    }
    public void Dispose()
    {
        Destroy(gameObject, collectAnimDuration);
    }

}
