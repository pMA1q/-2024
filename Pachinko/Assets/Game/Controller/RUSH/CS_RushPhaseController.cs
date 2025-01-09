using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_RushPhaseController : MonoBehaviour
{
  
    [SerializeField, Header("�v���n�u")]
    private GameObject mJackPotPerf;

    private int mGameCount = 100;//���ܐ�

    private CS_Controller mBigController;//�i�ߓ�(��)
    private CS_RushPhaseData mData;

    // �哖����̊m��
    private const float mBigWinProbability = 1.0f / 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        mBigController = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_Controller>();//�i�ߓ�����擾
        mBigController.NumberRailBigger();
        mData = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_RushPhaseData>();
    }

    // Update is called once per frame
    void Update()
    { 

        //���ܐ���3�H
        if (mGameCount <= 0 && mBigController.GetVariationFinish())
        {
            Destroy(this.gameObject);
            return;
        }

        
        //�ϓ��ł��邩���擾
        bool variationStart = mBigController.CanVariationStart();
        if (!variationStart) { return; }//false�Ȃ�I��

        //�ۗ��ʂ������Ȃ�I��
        if (mBigController.GetStock() == 0) { return; }

        //���o���I
        bool jackPot = CS_LotteryFunction.LotJackpotFloat(mBigWinProbability);
        WIN_LOST winlos = WIN_LOST.LOST;
        if (jackPot) 
        { 
            winlos = WIN_LOST.BIG_WIN;
            mData.JackPot = true;
        }
        

        //�ۗ��ʎg�p�i�ϓ��J�n�j
        mBigController.UseStock(winlos);

        PlayPerf(jackPot);
        mGameCount--;//���ܐ����Z
    }
    private void PlayPerf(bool _jackpot)
    {
        if(_jackpot)
        {
            if(mJackPotPerf != null)
            {
                StartCoroutine(WaitStartPerf());
            }
           
        }
        else
        {
            mBigController.PerformanceSemiFinish = true;
            mBigController.PerformanceFinish();//���o�͍s��Ȃ��̂ŏI���t���O�𗧂Ă�
        }
    }
    private IEnumerator WaitStartPerf()
    {
        yield return new WaitForSeconds(0.2f);
        Instantiate(mJackPotPerf, Vector3.zero, mJackPotPerf.transform.rotation);
    }
}
