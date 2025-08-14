using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class AmmoUI : MonoBehaviour
{
    [SerializeField] private AudioClip defaultReloadSoundClip;
    public TMP_Text AmmoTextBox;
    public Slider AmmoSlider;
    public Player player;


    void Start()
    {
        AmmoSlider.gameObject.SetActive(false);
        player = FindFirstObjectByType<Player>();
        //showSlider = false;
    }
    void Update()
    {
        if (player != null)
        {
            AmmoTextBox.text = "Ammo: " + player.currentAmmo.ToString() + "/" + player.maxAmmo.ToString();
        }
    }

    public IEnumerator AmmoUIRoutine()
    {
  
        AmmoSlider.gameObject.SetActive(true);
        AmmoSlider.maxValue = defaultReloadSoundClip.length;
        AmmoSlider.value = defaultReloadSoundClip.length;

        AmmoSlider.value = AmmoSlider.maxValue;
        yield return new WaitForSeconds(defaultReloadSoundClip.length);
        AmmoSlider.gameObject.SetActive(false);

    }
}
