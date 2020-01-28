using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxControler : MonoBehaviour
{
    //private

    private Quaternion gyro;
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        Input.compass.enabled = true;
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 加速度センサーを利用して箱を移動させる
    /// </summary>
    private void BoxMove()
    {
        var dir = Vector2.zero;
        dir.x = Input.acceleration.x;

        if (dir.sqrMagnitude > 1) { dir.Normalize(); }

        #region 傾き具合で加速度を変える 

        //右に傾けた時
        if (dir.x > 0 && dir.x < 0.3) { transform.Translate(dir * speed); }
        else if(dir.x > 0.3 && dir.x < 0.6){ transform.Translate(dir * speed * 1.2f); }
        else if(dir.x > 0.6 && dir.x < 1) { transform.Translate(dir * speed * 1.7f); }

        //左に傾けた時
        if (dir.x < 0 && dir.x > -0.3) { transform.Translate(dir * speed); }
        else if (dir.x < -0.3 && dir.x > -0.6) { transform.Translate(dir * speed * 1.2f); }
        else if (dir.x < -0.6 && dir.x > -1) { transform.Translate(dir * speed * 1.7f); }

        #endregion
    }

    /// <summary>
    /// ジャイロセンサーを取得し、Unity内でのカメラと同期させる
    /// </summary>
    private void GetGyro()
    {
        this.transform.localPosition = Quaternion.Euler(90, 0, 0) * (new Quaternion(-gyro.x, -gyro.y, -gyro.z, -gyro.w));
    }
}
