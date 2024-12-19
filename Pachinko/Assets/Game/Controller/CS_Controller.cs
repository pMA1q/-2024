//---------------------------------
//�i�ߓ��i��j
//�S���ҁF����
//---------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CS_Controller : MonoBehaviour
{
    //�p�`���R�̏��
    public enum PACHINKO_PHESE
    {
        SET,    //�����t�F�[�Y
        MISSION,//�~�b�V�����t�F�[�Y
        BOSS,   //�{�X�t�F�[�Y       
        BOUNUS,
        RUSH    //���b�V���t�F�[�Y
    }

    
    [SerializeField, Header("�i�ߓ��R���g���[���[")]
    List<GameObject> mCtrls = new List<GameObject>();
    [SerializeField, Header("�e�t�F�[�Y��BGM")]
    List<GameObject> mBGM = new List<GameObject>();


    [SerializeField, Header("�w�\")]
    private CS_Stock mHeso;

    [SerializeField, Header("�}���\��")]
    private CS_DrawPattern mDrawNum;

    [SerializeField, Header("�}���\��")]
    private CS_NumberRailController mDrawNum2;

    private Vector3 mDefaultNumberScale;


    private Vector3 mDefaultNumberPosition;

    [SerializeField]
    private Vector3 mBigNumberScale;

    [SerializeField]
    private Vector3 mBigNumberPosition;

    public GameObject NumberRail { get{ return mDrawNum2.gameObject; } }
    public CS_NumberRailController NumberRailController { get{ return mDrawNum2; } }

    [SerializeField]
    private PACHINKO_PHESE mNowPhese = PACHINKO_PHESE.SET;//���݂̃t�F�[�Y
    private PACHINKO_PHESE mPrevPhese = PACHINKO_PHESE.SET;//�O�̂̃t�F�[�Y

    private GameObject mNowBGM;

    private int mStock = 0;//�ۗ��ʐ�
    
    private bool mPatternVariationFinish = true;//�}���ϓ��I���t���O
    private bool mPerformanceFinish = true;//���o�I���t���O
    private bool mPerformanceSemiFinish = true;//���o�I�����t���O(���o���I����Ă��������܂ł̃t���O)
    private bool mWaitChoice = false;//�I��҂��t���O

    public bool PerformanceSemiFinish
    {
        set { mPerformanceSemiFinish = value; }
        get { return mPerformanceSemiFinish; }
    }

    private bool IsJackPotCutIn = false;
    public bool JackPotPerf
    {
        set { IsJackPotCutIn = value; }
        get { return IsJackPotCutIn; }
    }

    private int[] mPattern = new int[3];//�}��

    private float mVariationTimer;

    //�ϓ����Ԃ̐ݒ�A�擾
    public float VariationTimer
    {
        set { mVariationTimer = value; }
        get { return mVariationTimer; }
    }

    public bool WaitChoice
    {
        set { mWaitChoice = value; }
        get { return mWaitChoice; }
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateController();

        RectTransform NumberTrans = mDrawNum2.GetComponent<RectTransform>();
        mDefaultNumberScale = NumberTrans.localScale;
        mDefaultNumberPosition = NumberTrans.localPosition ;
    }

    // Update is called once per frame
    void Update()
    {
        //���݂̃t�F�[�Y�̑O�̃t�F�[�Y���Ⴄ�Ȃ玟�̃t�F�[�Y�ɍs��
       // if(mNowPhese != mPrevPhese) { GoNextPhese(); }
    }

    //���݂̃t�F�[�Y�擾
    public PACHINKO_PHESE GetPhese()
    {
        return mNowPhese;
    }

    //�t�F�[�Y�؂�ւ�
    public void ChangePhase(PACHINKO_PHESE _nextPhese)
    {
        mPrevPhese = mNowPhese;
        mNowPhese = _nextPhese;
       // GoNextPhese();
    }

    //���̃t�F�[�Y�֍s��
    private void GoNextPhese()
    {
        mPrevPhese = mNowPhese;
       
    }

    public void CreateController()
    {
        if(mNowBGM != null)
        {
            AudioSource audio = mNowBGM.GetComponent<AudioSource>();
            if(audio.isPlaying)
            {
                audio.Stop();
            }
            Destroy(mNowBGM);
        }
        mPerformanceFinish = true;
        mPerformanceSemiFinish = true;
        mPatternVariationFinish = true;
        GameObject ctrl = Instantiate(mCtrls[(int)mNowPhese], transform.position, transform.rotation);
        ctrl.name = mCtrls[(int)mNowPhese].name;
        mNowBGM = Instantiate(mBGM[(int)mNowPhese], Vector3.zero, Quaternion.identity);
    }

    //�ۗ��ʂ𑝂₷
    public void AddStock()
    {
        mStock++;
    }

    public void UseStock(WIN_LOST _win_lost)
    {
        mStock--;
        mPatternVariationFinish = false;//�}���ϓ��I���t���O��false
        mPerformanceFinish = false;//���o�I���t���O��false
        mPerformanceSemiFinish = false;//���o�I��(��)�t���O��false

        mPattern = CS_LotteryFunction.PatternLottery2(_win_lost);
        //Debug.Log("�}��:" + mHeso.stock[0][0] + "," + mHeso.stock[0][1] + "," + mHeso.stock[0][2] + ",");
        if (!mDrawNum2.gameObject.activeSelf) { mDrawNum2.gameObject.SetActive(true); }
        if(mDrawNum != null) {  mDrawNum.StartPatternVariation();}
        if(mDrawNum2 != null) {  mDrawNum2.StartPattenRail();}

    }

    public void Set777()
    {
        mDrawNum2.Rail777();
    }

    public void NumberRailBigger()
    {
        RectTransform NumberTrans = mDrawNum2.GetComponent<RectTransform>();
        NumberTrans.localScale = mBigNumberScale;
        NumberTrans.anchoredPosition = mBigNumberPosition;
    }

    public void NumberRailResetTrans()
    {
        RectTransform NumberTrans = mDrawNum2.GetComponent<RectTransform>();
        NumberTrans.localScale = mDefaultNumberScale;
        NumberTrans.localPosition = mDefaultNumberPosition;
    }

    public int[] GetPatterns()
    {
        return mPattern;
    }

    public bool GetJuckpot()
    {
        int[] nowstock = mPattern;
        return nowstock.Length == 3 && nowstock[0] == nowstock[1] && nowstock[1] == nowstock[2];
    }

    //�ۗ��ʂ��擾����
    public int GetStock()
    {
        mStock = mHeso.Count;
        return mStock;
    }

    //���o�I��
    public void PerformanceFinish()
    {
        JackPotPerf = false;
        mPerformanceFinish = true;
    }

    public bool GetPerformanceFinish()
    {
        return mPerformanceFinish;
    }

    //�}���ϓ��I��
    public void PatternVariationFinish()
    {
        if (GetJuckpot()) 
        { 
            if(mNowPhese == PACHINKO_PHESE.MISSION)
            {
               
            }
            mPerformanceFinish = false;
            mPerformanceSemiFinish = false;
            JackPotPerf = true;
            GetComponent<CS_CommonData>().LeftAttakerStart(3);
        }
        Debug.Log("�}���I��");
        mPatternVariationFinish = true;
        mHeso.DisableStock();//�X�g�b�N���폜
    }

    //�}���ϓ����I�����Ă��邩
    public bool GetPatternVariationFinish() { return mPatternVariationFinish; }

    //�ϓ����J�n�ł��邩
    public bool CanVariationStart()
    {
        if(GetStock() <= 0) { return false; }
        return mPatternVariationFinish && mPerformanceFinish;
    }

    public bool GetVariationFinish() 
    {
        return mPatternVariationFinish && mPerformanceFinish;
    }
}
