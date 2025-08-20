using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/MagBuff")]

public class MagBuff : PowerupEffect
{
    public int amount;
    public override void Apply(GameObject target)
    {
        target.GetComponent<Player>().maxAmmo += amount;
        target.GetComponent<Player>().currentAmmo += amount;
        //target.GetComponent<SpriteRenderer>().color = Color.orange;
    }
}
