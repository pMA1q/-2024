using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_EffectAnimation : MonoBehaviour
{
    [SerializeField, Header("アニメーションさせるImage")]
    private Image mImage;
    [SerializeField, Header("画像データ")]
    private List<Sprite> mAnimationSprite;

    [SerializeField, Header("アニメーションスピード")]
    private float mAnimationSpeed = 0.1f;

    private float mTime = 0;

    private int mCurrent = 0;

    // Start is called before the first frame update
    void Start()
    {
        mImage.sprite = mAnimationSprite[mCurrent];
    }

    // Update is called once per frame
    void Update()
    {
        // ChangeSprite();

        mTime += mAnimationSpeed * Time.deltaTime;
        if (mTime >= mAnimationSpeed)
        {
            mTime = 0;
            ChangeSprite();
        }
        Debug.Log("アニメーションナンバー" + mCurrent);
    }

    void ChangeSprite()
    {
        mCurrent++;
        if(mCurrent >= mAnimationSprite.Count)
        {
            mCurrent = 0;
        }
        mImage.sprite = mAnimationSprite[mCurrent];
    }
}
