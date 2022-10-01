using UnityEngine;

public class Chunk : MonoBehaviour
{
    public Vector2 size;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
