using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    //private

    //マネージャー
    [SerializeField] TouchManager _touchManager;            //TouchManager
    [SerializeField] SceneLoadManager _sceneLoadManager;    //SceneLoadManager

    //ボタンのタイプ
    private enum ButtonType
    {
        StageOne,       //ステージ１へ
        StageTwo,       //ステージ２へ
        StageThree,     //ステージ３へ
        StageFour,      //ステージ４へ
        BackTitle       //タイトルへ
    };

    private ButtonType thisButtonType;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<GameObject>();

        if (this.gameObject.tag == "StageOne") { }
    }

    // Update is called once per frame
    void Update()
    {
        StageDecision();
    }

    /// <summary>
    /// ステージ決定
    /// </summary>
    private void StageDecision()
    {
        //タッチ状態更新
        _touchManager.Update();

        //タッチ取得
        TouchManager touchState = _touchManager.getTouch();

        //タッチされていなければこれより先の処理を行わない。
        if (!touchState.touchFlag) return;

        #region タッチされた時の処理

        //座標系変換
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(touchState.touchPosition);

        if (touchState.touchPhase == TouchPhase.Began)
        {
            //タッチした瞬間の処理
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            //タッチした場所にオブジェクトがなければこの先の処理を行わない
            if (!hit.collider.gameObject) return;


        }

        #endregion
    }
}
