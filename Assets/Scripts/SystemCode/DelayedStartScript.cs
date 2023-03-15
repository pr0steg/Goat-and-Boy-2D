using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DelayedStartScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public TextMeshProUGUI tapToStartText;

    public GameObject goat;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("StartDelay");
        rb = GetComponent<Rigidbody2D>();
        
    }

    IEnumerator StartDelay()
    {
        Time.timeScale = 0.0f;
        GameObject adsButton = GameObject.FindGameObjectWithTag("AdsButton");
        adsButton.SetActive(false);
        int kingStatus = PlayerPrefs.GetInt("kingofgame");
        GameObject trueKingLogoObject = GameObject.Find("TrueKingLogo");
        Image trueKingLogoImage = trueKingLogoObject.GetComponent<Image>();
        GameObject lightDefaultLogoObject = GameObject.Find("LightDefaultLogo");
        Image lightDefaultLogoImage = lightDefaultLogoObject.GetComponent<Image>();
        if (kingStatus == 1)
        {
            trueKingLogoImage.enabled = true;
            lightDefaultLogoImage.enabled = false;
            tapToStartText.text = "LONG LIVE THE KING!";
            PlayerPrefs.SetInt("kingofgame", 0);
        }
        else
        {
            lightDefaultLogoImage.enabled = true;
        }
        tapToStartText.enabled = true;
        while (rb.velocity.x == 0 && rb.velocity.y == 0)
        {
            yield return 0;
        }
        Time.timeScale = 1.0f;
        adsButton.SetActive(true);
        tapToStartText.enabled = false;
        lightDefaultLogoImage.enabled = false;
        trueKingLogoImage.enabled = false;
        AudioSource[] audioSources = goat.GetComponents<AudioSource>();
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.enabled = true;
        }
    }
}
