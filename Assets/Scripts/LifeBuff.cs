using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/LifeBuff")]
public class LifeBuff : PowerupEffect
{
    public int amount;

    public override void Apply(GameObject target)
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.lives += amount;
        }
    }
}