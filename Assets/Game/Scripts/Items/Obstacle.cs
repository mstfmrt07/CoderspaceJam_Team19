using UnityEngine;

public class Obstacle : ExecuteOnCollision, IDisposable
{
    public Animator obstacleAnimator;
    public ObstacleType obstacleType;
    public AnimationState playerAnimState;

    [Header("Trigger")]
    public CollisionDetector triggerDetector;
    public GameObject warningMark;
    public float triggerWaitTime;

    public float SpeedFactor => GameManager.Instance.GameFlowSpeed;

    protected override void Awake()
    {
        base.Awake();

        InitObstacle();
    }

    private void InitObstacle()
    {
        if (triggerDetector != null)
            triggerDetector.OnEnter += TriggerAction;

        if (warningMark != null)
            warningMark.SetActive(false);

        switch (obstacleType)
        {
            case ObstacleType.Banana:
                break;
            case ObstacleType.FlowerPot:
                var rb = GetComponent<Rigidbody2D>();
                rb.bodyType = RigidbodyType2D.Kinematic;
                break;
            case ObstacleType.Rocket:
                break;
            case ObstacleType.Barrel:
                break;
            case ObstacleType.Lightning:
                detector.GetComponent<Collider2D>().enabled = false;
                detector.IsActive = false;
                break;
            default:
                break;
        }
    }

    protected override void HandleCollisionEnter(Collider2D collider)
    {
        OnObstacleHit();
    }

    private void OnObstacleHit()
    {
        if (warningMark != null)
            warningMark.SetActive(false);

        Player.Instance.animator.PlayAnim(playerAnimState);
    }

    public void ObstacleAction()
    {
        switch (obstacleType)
        {
            case ObstacleType.Banana:
                Debug.Log("This is Banana");
                SoundManager.Instance.PlaySound(SoundManager.Instance.fallClip);
                break;
            case ObstacleType.FlowerPot:
                Debug.Log("This is Flower Pot");
                SoundManager.Instance.PlaySound(SoundManager.Instance.flowerPotClip, lifeTime: 1f);

                var rb = GetComponent<Rigidbody2D>();
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.gravityScale *= SpeedFactor;
                break;
            case ObstacleType.Rocket:
                Debug.Log("This is Rocket");
                break;
            case ObstacleType.Barrel:
                SoundManager.Instance.PlaySound(SoundManager.Instance.explosionClip);
                Debug.Log("This is Barrel");
                break;
            case ObstacleType.Lightning:
                SoundManager.Instance.PlaySound(SoundManager.Instance.lightningClip);
                Debug.Log("This is Lightning");
                detector.GetComponent<Collider2D>().enabled = true;
                detector.IsActive = true;
                break;
            default:
                Debug.Log("This is undefined obstacle");
                break;
        }

        obstacleAnimator.SetTrigger("Action");

    }

    public virtual void TriggerAction(Collider2D collider)
    {
        this.Wait(triggerWaitTime / SpeedFactor, () => ObstacleAction());
        if (warningMark != null)
            warningMark.SetActive(true);
    }

    protected override void HandleCollisionExit(Collider2D collider)
    {
    }

    public void Dispose()
    {
        Destroy(gameObject);
    }
}

public enum ObstacleType { Banana, FlowerPot, Rocket, Barrel, Lightning}