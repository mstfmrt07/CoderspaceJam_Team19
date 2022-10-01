using UnityEngine;

public class DistanceMeter : Activatable
{
    public Transform target;

    private Vector2 initialPos;
    private float distance;

    public float Distance => distance;

    protected override void OnActivate()
    {
        base.OnActivate();
        initialPos = target.position;
        distance = 0f;
    }
    protected override void Tick()
    {
        distance = (target.position.x - initialPos.x);
    }

    protected override void OnDeactivate()
    {
        base.OnDeactivate();
        initialPos = Vector2.zero;
    }
}
