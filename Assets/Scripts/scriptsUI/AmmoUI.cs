using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class AmmoUI : MonoBehaviour
{
    public TMP_Text AmmoTextBox;
    public Slider AmmoSlider;
    private Player player;

    private bool showSlider = true;

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
    private void sliderVisiblity()
    {
    
    }

    public IEnumerator AmmoUIRoutine()
    {
        showSlider = true;
        AmmoSlider.gameObject.SetActive(true);
        

        AmmoSlider.maxValue = soundDuration;
        AmmoSlider.value = soundDuration;

        //set max value to sound duration
        //set slider value
        //move down to zero with clip length `
        //show slider = false;

    }
}
