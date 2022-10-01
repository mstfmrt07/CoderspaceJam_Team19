using UnityEngine;

public class Chunk : MonoBehaviour, IDisposable
{
    public Vector2 size;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, size);
    }
    public void Dispose()
    {
        Destroy(gameObject);
    }
}
