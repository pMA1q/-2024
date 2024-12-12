//---------------------------------
//�}���ϓ�
//�S���ҁF����
//---------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CS_DrawPattern : MonoBehaviour
{
    [SerializeField]
    Heso mHeso = null;  // �Q�[���̏�Ԃ�f�[�^���Ǘ�����J�X�^���N���X

    Coroutine mCoroutine = null;  // �R���[�`���̎��s���Ǘ�����ϐ�

    private CS_NumberSprite mNumberSprite;  

    // �����l�ݒ�
    int[] mValue = new int[3] { 2, 6, 4 };



    private void Start()
    {
        mNumberSprite = this.GetComponent<CS_NumberSprite>();
        if (mNumberSprite == null)
        {
            Debug.LogError("CS_NumberSprite���A�^�b�`���Ă�������");
        }

    }

    public void StartPatternVariation()
    {
        //Debug.Log("�}���ϓ�");
       
        if (mCoroutine == null && mHeso.stock.Count != 0)
        {
            mCoroutine = StartCoroutine(RealTex());
        }
    }

    
    // ���I���J�n���郁�\�b�h
    private IEnumerator RealTex()
    {
        // stock�̗v�f��������Ă��邩�m�F
        if (mHeso.stock.Count == 0 || mHeso.stock[0].Length < 3)
        {
            Debug.LogError("stock�ɏ\���ȗv�f������܂���");
            yield break;
        }


        float time = 0.0f;
        //mHeso.stock[0].CopyTo(mValue, 0); //�l���R�s�[(���ڑ�������mValue���ς�����Ƃ���stock�̒��g���ς��̂ŃR�s�[)

        
      �@//�Q�b�ԕϓ�
        while (time <= 2.0f)
        {
            //�R�̐}�����C���N�������g�ŉ�
            for (int i = 0; i < 3; i++) { IncNumber(i); }
            Debug.Log("�}��:" + mHeso.stock[0][0] + "," + mHeso.stock[0][1] + "," + mHeso.stock[0][2] + ",");
            time += Time.deltaTime;
            yield return null;
        }

        // stock�̃T�C�Y���ēx�m�F���Ă���l��\��
        if (mHeso.stock.Count > 0 && mHeso.stock[0].Length >= 3)
        {
            //0.2�b���Ƃɍ��A�E�A���̏��ԂŎ~�߂�
            //DecisionNumber(mTexts[0], mHeso.stock[0][0]);
            mNumberSprite.SetNumber(mHeso.stock[0][0],CS_NumberSprite.Pattern.LEFT);
            yield return StartCoroutine(UpdateWithIncNumber(0.2f, 1,3)); // 0.2�b�ԁAIncNumber����
            //DecisionNumber(mTexts[1], mHeso.stock[0][2]);
            mNumberSprite.SetNumber(mHeso.stock[0][2], CS_NumberSprite.Pattern.RIGHT);
            yield return StartCoroutine(UpdateWithIncNumber(0.2f, 2, 3));
            //DecisionNumber(mTexts[2], mHeso.stock[0][1]);
            mNumberSprite.SetNumber(mHeso.stock[0][1], CS_NumberSprite.Pattern.CENTER);
        }
        else
        {
            Debug.LogError("stock�ɏ\���ȃf�[�^������܂���");
        }


        mCoroutine = null;
        yield return new WaitForSeconds(0.5f);
        //mHeso.DisableStock();//�X�g�b�N���폜
        //mHeso.stock.RemoveAt(0);//�X�g�b�N���X�g�̂O�Ԗڂ��폜

        //�i�ߓ��ɐ}���ϓ��I����`����
        //CS_Controller ctrl = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_Controller>();
        //ctrl.PatternVariationFinish();

        yield return null;
    }

    //�}����~�����A�F�Ɛ}�������߂�
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

    //�}���̐������C���N�������g
    private void IncNumber(int _val)
    {
        mValue[_val]++;

        mValue[_val] = (mValue[_val] - 1) % 9 + 1; // 1�`9�͈͓̔��ɒ���


        //mTexts[_val].text = mValue[_val].ToString();

        //�摜�̕ύX
        mNumberSprite.SetNumber(mValue[_val], (CS_NumberSprite.Pattern)_val);

    }

    //�}�������ԂɎ~�܂�Ƃ��̎~�܂����}���ȊO�͂܂��C���N�������g��������
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
