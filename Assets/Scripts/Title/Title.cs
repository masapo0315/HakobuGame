using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    [SerializeField] private Text text;     //点滅させるテキスト
    private float speed = 1.0f;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetAlphaColor(text.color);
    }

    #region テキストの点滅

    /// <summary>
    /// Alhpa値を更新してColorを返す
    /// </summary><param name= "color"></param>>
    private void GetAlphaColor(Color color)
    {
        time += Time.deltaTime * 5.0f * speed;
        color.a = Mathf.Sin(time) * 0.5f + 0.5f;

        return;
    }

    #endregion

}
