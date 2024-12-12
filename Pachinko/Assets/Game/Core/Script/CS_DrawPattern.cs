//---------------------------------
//図柄変動
//担当者：中島
//---------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CS_DrawPattern : MonoBehaviour
{
    [SerializeField]
    Heso mHeso = null;  // ゲームの状態やデータを管理するカスタムクラス

    Coroutine mCoroutine = null;  // コルーチンの実行を管理する変数

    private CS_NumberSprite mNumberSprite;  

    // 初期値設定
    int[] mValue = new int[3] { 2, 6, 4 };



    private void Start()
    {
        mNumberSprite = this.GetComponent<CS_NumberSprite>();
        if (mNumberSprite == null)
        {
            Debug.LogError("CS_NumberSpriteをアタッチしてください");
        }

    }

    public void StartPatternVariation()
    {
        //Debug.Log("図柄変動");
       
        if (mCoroutine == null && mHeso.stock.Count != 0)
        {
            mCoroutine = StartCoroutine(RealTex());
        }
    }

    
    // 抽選を開始するメソッド
    private IEnumerator RealTex()
    {
        // stockの要素数が足りているか確認
        if (mHeso.stock.Count == 0 || mHeso.stock[0].Length < 3)
        {
            Debug.LogError("stockに十分な要素がありません");
            yield break;
        }


        float time = 0.0f;
        //mHeso.stock[0].CopyTo(mValue, 0); //値をコピー(直接代入するとmValueが変わったときにstockの中身も変わるのでコピー)

        
      　//２秒間変動
        while (time <= 2.0f)
        {
            //３つの図柄をインクリメントで回す
            for (int i = 0; i < 3; i++) { IncNumber(i); }
            Debug.Log("図柄:" + mHeso.stock[0][0] + "," + mHeso.stock[0][1] + "," + mHeso.stock[0][2] + ",");
            time += Time.deltaTime;
            yield return null;
        }

        // stockのサイズを再度確認してから値を表示
        if (mHeso.stock.Count > 0 && mHeso.stock[0].Length >= 3)
        {
            //0.2秒ごとに左、右、中の順番で止める
            //DecisionNumber(mTexts[0], mHeso.stock[0][0]);
            mNumberSprite.SetNumber(mHeso.stock[0][0],CS_NumberSprite.Pattern.LEFT);
            yield return StartCoroutine(UpdateWithIncNumber(0.2f, 1,3)); // 0.2秒間、IncNumberを回す
            //DecisionNumber(mTexts[1], mHeso.stock[0][2]);
            mNumberSprite.SetNumber(mHeso.stock[0][2], CS_NumberSprite.Pattern.RIGHT);
            yield return StartCoroutine(UpdateWithIncNumber(0.2f, 2, 3));
            //DecisionNumber(mTexts[2], mHeso.stock[0][1]);
            mNumberSprite.SetNumber(mHeso.stock[0][1], CS_NumberSprite.Pattern.CENTER);
        }
        else
        {
            Debug.LogError("stockに十分なデータがありません");
        }


        mCoroutine = null;
        yield return new WaitForSeconds(0.5f);
        //mHeso.DisableStock();//ストックを削除
        //mHeso.stock.RemoveAt(0);//ストックリストの０番目を削除

        //司令塔に図柄変動終了を伝える
        //CS_Controller ctrl = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_Controller>();
        //ctrl.PatternVariationFinish();

        yield return null;
    }

    //図柄停止させ、色と図柄を決める
    private void DecisionNumber(TextMeshProUGUI _textGUI, int _stock)
    {
        _textGUI.text = _stock.ToString();
        if (_stock % 2 == 0)
        {
            _textGUI.color = Color.blue;
        }
        else
        {
            _textGUI.color = Color.red;
        }
    }

    //図柄の数字をインクリメント
    private void IncNumber(int _val)
    {
        mValue[_val]++;

        mValue[_val] = (mValue[_val] - 1) % 9 + 1; // 1～9の範囲内に調整


        //mTexts[_val].text = mValue[_val].ToString();

        //画像の変更
        mNumberSprite.SetNumber(mValue[_val], (CS_NumberSprite.Pattern)_val);

    }

    //図柄が順番に止まるときの止まった図柄以外はまだインクリメントし続ける
    private IEnumerator UpdateWithIncNumber(float _duration, int _startindex, int _finishIndex)
    {
        float time = 0.0f;

        while (time <= _duration)
        {
            for(int i = _startindex; i < _finishIndex; i++)
            {
                IncNumber(i);
            }
           
            time += Time.deltaTime;
            yield return null;
        }
    }
}
