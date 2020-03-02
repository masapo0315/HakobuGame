using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeControler : MonoBehaviour
{
    [SerializeField] private Image fadePanel;   //透明度を変えるimage
    [SerializeField] private float fadespeed;   //不透明度を変える速度
    private float　red, green, blue, alpha;     //パネルの色、透明度

    [HideInInspector] public bool fadeOut;
    [HideInInspector] public bool fadeIn;

    // Start is called before the first frame update
    void Start()
    {
        red = fadePanel.color.r;
        green = fadePanel.color.g;
        blue = fadePanel.color.b;
        alpha = fadePanel.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn) { StartFadeIn(); }
        else if (fadeOut) { StartFadeOut(); }
    }

    //フェードイン
    private void StartFadeIn()
    {
        alpha -= fadespeed;
        SetAlpha();
        if (alpha <= 0)
        {
            fadeIn = false;
            fadePanel.enabled = false;
        }
    }

    //フェードアウト
    private void StartFadeOut()
    {
        fadePanel.enabled = true;
        alpha += fadespeed;
        SetAlpha();
        if (alpha >= 1) { fadeOut = false; }
    }

    private void SetAlpha()
    {
        fadePanel.color = new Color(red, green, blue, alpha);
    }
}
