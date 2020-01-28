using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    //private

    //プレイヤーパラメーター
    [SerializeField] private GameObject player;     //動かすキャラクター
    [SerializeField] private float speed;           //移動速度
    [SerializeField] private float jumpPower;       //ジャンプ力
    [SerializeField] private float deadCancelTime;  //死亡状態を解除するのに必要な時間

    //操作に必要なボタン
    [SerializeField] private GameObject rightButton;    //右矢印ボタン
    [SerializeField] private GameObject leftButton;     //左矢印ボタン
    [SerializeField] private GameObject BoxCatchButton; //箱を掴むボタン
    [SerializeField] private GameObject BoxLeaveButton; //箱を離すボタン
    [SerializeField] private GameObject jumpButton;     //ジャンプボタン

    
    private TouchManager touchState;    //タッチ状態   
    private Vector2 worldPoint;         //座標系変換
    private RaycastHit2D hit;

    //プレイヤーの状態
    [HideInInspector] public enum PlayerState
    {
        nomal,      //通常状態
        haveBox,    //箱を持っている状態
        dead        //死亡状態
    }

    [HideInInspector] public PlayerState playerState;


    // Start is called before the first frame update
    void Start()
    {
        playerState = PlayerState.nomal;
    }

    // Update is called once per frame
    void Update()
    {
        DeadCheck();

        //タッチ状態更新
        TouchManager.Instnce.Update();

        //タッチ取得
        touchState = TouchManager.Instnce.getTouch();

        //座標系変換
        worldPoint = Camera.main.ScreenToWorldPoint(touchState.touchPosition);

        //死亡状態なら操作不能にする
        if (playerState == PlayerState.dead) return;

        //タッチされていなければこれより先の処理を行わない。
        if (!touchState.touchFlag) return;

        //タッチした瞬間の処理
        hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        PlayerMove();
        PlayerJump();

    }

    #region プレイヤー操作や死亡状態確認など

    /// <summary>
    /// 移動
    /// </summary>
    private void PlayerMove()
    {
        if (touchState.touchPhase == TouchPhase.Began)
        {
            //タッチした場所にオブジェクトがなければこの先の処理を行わない
            if (!hit.collider.gameObject) return;
        }
    }

    /// <summary>
    /// ジャンプ
    /// </summary>
    private void PlayerJump()
    {
        if (touchState.touchPhase == TouchPhase.Began)
        {
            //タッチした場所にオブジェクトがなければこの先の処理を行わない
            if (!hit.collider.gameObject) return;

        }
    }

    /// <summary>
    /// 箱をつかむ
    /// </summary>
    private void BoxCatch()
    {
        

        BoxCatchButton.SetActive(false);
        BoxLeaveButton.SetActive(true);
    }

    /// <summary>
    /// 箱を離す
    /// </summary>
    private void BoxLeave()
    {
        BoxLeaveButton.SetActive(true);
        BoxCatchButton.SetActive(true);
    }

    /// <summary>
    /// 死亡してるかのチェック
    /// </summary>
    private void DeadCheck()
    {
        //死亡していなければこの先の処理は行わない
        if (playerState != PlayerState.dead) return;

        StartCoroutine("DeadCancel");

    }

    /// <summary>
    /// 死亡状態解除
    /// </summary>
    private IEnumerator DeadCancel()
    {
        yield return new WaitForSeconds(deadCancelTime);

        playerState = PlayerState.nomal;
    }
    #endregion

}
