using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    //private

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

        #region タグによるボタンタイプの識別

        if (this.gameObject.tag == "StageOne") { thisButtonType = ButtonType.StageOne; }
        else if(this.gameObject.tag == "StageTwo") { thisButtonType = ButtonType.StageTwo; }
        else if (this.gameObject.tag == "StageThree") { thisButtonType = ButtonType.StageThree; }
        else if (this.gameObject.tag == "StageFour") { thisButtonType = ButtonType.StageFour; }
        else if (this.gameObject.tag == "BackTitle") { thisButtonType = ButtonType.BackTitle; }
        #endregion
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
        TouchManager.Instnce.Update();

        //タッチ取得
        TouchManager touchState = TouchManager.Instnce.getTouch();

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

            //タッチしたボタンの種類で行き先を変える
            if (thisButtonType == ButtonType.StageOne) { SceneLoadManager.Instnce.LoadScene("Stage1"); }
            else if (thisButtonType == ButtonType.StageTwo) { SceneLoadManager.Instnce.LoadScene("Stage2"); }
            else if (thisButtonType == ButtonType.StageThree) { SceneLoadManager.Instnce.LoadScene("Stage3"); }
            else if (thisButtonType == ButtonType.StageFour) { SceneLoadManager.Instnce.LoadScene("Stage4"); }
            else if (thisButtonType == ButtonType.BackTitle) { SceneLoadManager.Instnce.LoadScene("Title"); }


        }

        #endregion
    }
}
