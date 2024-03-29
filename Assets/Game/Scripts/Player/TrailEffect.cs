using UnityEngine;

public class TrailEffect : MonoBehaviour
{
    public float activeTime = 0.1f;
    public float startAlpha = 0.8f;
    public float alphaMultiplier = 0.85f;

    private float timeActivated;
    private float currentAlpha;
    
    private Player player;
    private SpriteRenderer playerSR;

    private SpriteRenderer SR;

    private Color color;

    private void OnEnable()
    {
        SR = GetComponent<SpriteRenderer>();
        player = Player.Instance;
        playerSR = player.animator.GetComponent<SpriteRenderer>();

        currentAlpha = startAlpha;
        color = playerSR.color;
        SR.sprite = playerSR.sprite;
        transform.position = playerSR.transform.position;
        transform.rotation = playerSR.transform.rotation;
        timeActivated = Time.time;
    }

    private void Update()
    {
        currentAlpha *= alphaMultiplier;
        color.a = currentAlpha;
        SR.color = color;

        if (Time.time >= (timeActivated + activeTime))
        {
            ObjectPool.Instance.AddToPool(gameObject);
        }

    }
}
