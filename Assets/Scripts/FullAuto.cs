using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/FullAuto")]

public class FullAuto : PowerupEffect

{
    [SerializeField] Sprite newSprite;
    public int amount;
    public override void Apply(GameObject target)
    {
        target.GetComponent<Player>().maxAmmo += amount;
        target.GetComponent<Player>().currentAmmo += amount;
        target.GetComponent<SpriteRenderer>().sprite = newSprite;
        target.GetComponent<Player>().CurrentUpgrade = Upgrade.FullAuto;
        Debug.Log("fullauto acquired");
    }
}
