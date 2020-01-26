using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    //private

    [SerializeField] private float speed;
    [SerializeField] private Text text;     //点滅させるテキスト
    private float time;

    //マネージャー

    [SerializeField] SceneLoadManager _sceneLoadManager;    //SceneLoadManager
    [SerializeField] TouchManager _touchManager;            //TouchManager

    // Update is called once per frame
    void Update()
    {
        text.color = GetAlphaColor(text.color);

        //タッチ取得
        TouchManager touchState = _touchManager.getTouch();

        //タッチされたらステージセレクトへ
        if (!touchState.touchFlag) return;
        if (touchState.touchPhase == TouchPhase.Began)
        {
            _sceneLoadManager.LoadScene("StageSelect");
        }

    }

    /// <summary>
    /// Alhpa値を更新してColorを返す
    /// </summary>
    /// <param name= "color"></param>>
    private Color GetAlphaColor(Color color)
    {
        time += Time.deltaTime * 5.0f * speed;
        color.a = Mathf.Sin(time) * 0.5f + 0.5f;

        return color;
    }
}
