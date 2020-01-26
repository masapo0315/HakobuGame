using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    #region シングルトン
    private static TouchManager m_instance = null;

    public static TouchManager Instnce
    {
        get
        {
            if (m_instance == null)
            {
                Debug.LogError("アタッチしているオブジェクトがありません");
            }
            return m_instance;
        }
    }

    /// <summary>
    /// シングルトン作成
    /// </summary>
    private void CreateInstnce()
    {
        if (m_instance == null)
        {
            m_instance = this;
            DontDestroyOnLoad(this.transform.gameObject);
        }
        else
        {
            Destroy(this.transform.gameObject);
        }
    }
    #endregion

    [HideInInspector] public bool touchFlag;      // タッチ有無
    [HideInInspector] public Vector2 touchPosition;   // タッチ座標
    [HideInInspector] public TouchPhase touchPhase;   // タッチ状態

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="flag">タッチしているかどうか</param>
    /// <param name="position">タッチしている座標</param>
    /// <param name="phase">タッチの状態</param>
    public TouchManager(bool flag = false, Vector2? position = null, TouchPhase phase = TouchPhase.Began)
    {
        if(position == null)
        {
            this.touchPosition = new Vector2(0, 0);
        } else {
            this.touchPosition = (Vector2)position;
        }
        this.touchPhase = phase;
    }


    // Update is called once per frame
    public void Update()
    {
        TouchEditor();
    }

    /// <summary>
    /// エディタ
    /// </summary>
    private void TouchEditor()
    {
        this.touchFlag = false;

        if (!Application.isEditor) return;

        // 押した瞬間
        if (Input.GetMouseButtonDown(0))
        {
            this.touchFlag = true;
            this.touchPhase = TouchPhase.Began;
        }

        // 離した瞬間
        if (Input.GetMouseButtonUp(0))
        {
            this.touchFlag = true;
            this.touchPhase = TouchPhase.Ended;
        }

        // 押しっぱなし
        if (Input.GetMouseButton(0))
        {
            this.touchFlag = true;
            this.touchPhase = TouchPhase.Moved;
        }

        //座標取得
        if (this.touchFlag) this.touchPosition = Input.mousePosition;

        //端末の場合
        else
        {
            if (Input.touchCount == 0) return;

            Touch touch = Input.GetTouch(0);
            this.touchPosition = touch.position;
            this.touchPhase = touch.phase;
            this.touchFlag = true;
        }
    }

    /// <summary>
    /// タッチ状態取得
    /// </summary>
    public TouchManager getTouch()
    {
        return new TouchManager(this.touchFlag, this.touchPosition, this.touchPhase);
    }
}
