using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_BP_DrawCount : MonoBehaviour
{
    // 数字スプライトリスト (0~9の画像)
    public Sprite[] numberSprites;

    // 数字を配置する親オブジェクト (RectTransform)
    public RectTransform numbersParent;

    // Imageプレハブ (空のImageコンポーネントを持つオブジェクトを作成し登録)
    public GameObject imagePrefab;

    // Imageコンポーネントのリスト
    private List<Image> digitImages = new List<Image>();

    // 表示する間隔
    public float spacing = 50f;

    private void Start()
    {

    }
    /// <summary>
    /// カウントを設定する
    /// </summary>
    /// <param name="count">表示する数値</param>
    public void SetCount(int count)
    {
        if (count < 0)
        {
            // 0未満の場合: numberSpritesの最後のスプライトを3つ表示
            ShowNegativePlaceholder();
        }
        else
        {
            // 通常の数値表示
            ShowPositiveCount(count);
        }
    }

    /// <summary>
    /// 通常の数値表示処理
    /// </summary>
    private void ShowPositiveCount(int count)
    {
        string countStr = count.ToString(); // 数値を文字列に変換

        // 必要な桁数だけImageコンポーネントを確保
        EnsureDigitImages(countStr.Length);

        // 各桁のスプライトを設定
        for (int i = 0; i < countStr.Length; i++)
        {
            int digit = int.Parse(countStr[i].ToString()); // 1の位から順番に取得
            digitImages[i].sprite = numberSprites[digit];
            digitImages[i].gameObject.SetActive(true);
        }

        // 不要なImageを非アクティブ化
        for (int i = countStr.Length; i < digitImages.Count; i++)
        {
            digitImages[i].gameObject.SetActive(false);
        }

        // 配置を調整
        ArrangeDigits(countStr.Length);
    }

    /// <summary>
    /// 0未満の場合のプレースホルダー表示処理
    /// </summary>
    private void ShowNegativePlaceholder()
    {
        const int placeholderCount = 3; // プレースホルダー数
        Sprite placeholderSprite = numberSprites[numberSprites.Length - 1]; // 最後のスプライト

        // 必要な桁数だけImageコンポーネントを確保
        EnsureDigitImages(placeholderCount);

        // プレースホルダーを設定
        for (int i = 0; i < placeholderCount; i++)
        {
            digitImages[i].sprite = placeholderSprite;
            digitImages[i].gameObject.SetActive(true);
        }

        // 不要なImageを非アクティブ化
        for (int i = placeholderCount; i < digitImages.Count; i++)
        {
            digitImages[i].gameObject.SetActive(false);
        }

        // 配置を調整
        ArrangeDigits(placeholderCount);
    }

    /// <summary>
    /// 必要な桁数だけImageコンポーネントを確保する
    /// </summary>
    /// <param name="digitCount">必要な桁数</param>
    private void EnsureDigitImages(int digitCount)
    {
        while (digitImages.Count < digitCount)
        {
            GameObject newImageObj = Instantiate(imagePrefab, numbersParent);
            Image newImage = newImageObj.GetComponent<Image>();
            digitImages.Add(newImage);
        }
    }

    /// <summary>
    /// 桁を等間隔で配置する
    /// </summary>
    /// <param name="digitCount">表示する桁数</param>
    private void ArrangeDigits(int digitCount)
    {
        float totalWidth = (digitCount - 1) * spacing;
        float startX = -totalWidth / 2f;

        for (int i = 0; i < digitCount; i++)
        {
            RectTransform rectTransform = digitImages[i].GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(startX + i * spacing, 0);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // スペースキーでテスト
        {
            int randomCount = Random.Range(0, 100); // 0～99のランダムな値
            SetCount(randomCount);
        }
    }
}
