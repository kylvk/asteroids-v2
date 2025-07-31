using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    public TMP_Text AmmoTextBox;
    private Player player;
    void Start()
    {
        player = FindFirstObjectByType<Player>();
    }
    void Update()
    {
        if (player != null)
        {
            AmmoTextBox.text = "Ammo: " + player.currentAmmo.ToString() + "/" + player.maxAmmo.ToString();
        }
    }
}
