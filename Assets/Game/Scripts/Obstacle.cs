using UnityEngine;

public class Obstacle : ExecuteOnCollision
{
    public Animator obstacleAnimator;
    public ObstacleType obstacleType;
    public AnimationState playerAnimState;

    protected override void HandleCollisionEnter(Collider2D collider)
    {
        ObstacleAction();
    }

    public void ObstacleAction()
    {
        switch (obstacleType)
        {
            case ObstacleType.Banana:
                Debug.Log("This is Banana");
                break;
            case ObstacleType.FlowerPot:
                Debug.Log("This is Flower Pot");
                break;
            case ObstacleType.Rocket:
                Debug.Log("This is Rocket");
                break;
            case ObstacleType.Barrel:
                Debug.Log("This is Barrel");
                break;
            case ObstacleType.Lightning:
                Debug.Log("This is Lightning");
                break;
            default:
                Debug.Log("This is undefined obstacle");
                break;
        }

        Player.Instance.animator.PlayAnim(playerAnimState);
        obstacleAnimator.SetTrigger("Action");
    }

    protected override void HandleCollisionExit(Collider2D collider)
    {
    }
}

public enum ObstacleType { Banana, FlowerPot, Rocket, Barrel, Lightning}