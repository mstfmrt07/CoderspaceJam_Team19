using UnityEngine;

public class DistanceMeter : Activatable, IResettable
{
    public Transform target;

    private Vector2 initialPos;
    private float distance;

    public float Distance => distance;

    protected override void OnActivate()
    {
        base.OnActivate();
    }

    public void Init()
    {
        initialPos = target.position;
    }

    protected override void Tick()
    {
        distance = (target.position.x - initialPos.x);
    }

    protected override void OnDeactivate()
    {
        base.OnDeactivate();
    }

    public void ApplyReset()
    {
        distance = 0f;
        initialPos = Vector2.zero;
    }
}
