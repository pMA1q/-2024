//---------------------------------
//�����t�F�[�Y�̃~�b�V�����𒊑I���ĕ\��
//�S���ҁF����
//---------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_LotMission: MonoBehaviour
{
    [SerializeField, Header("�~�b�V�����e�[�u��")]
    private CSO_SetPhaseTable mMissionStatus;

    [SerializeField, Header("�X�e�[�^�X�~�b�V�����\���I�u�W�F�N�g")]
    private MeshRenderer mStatusMission;

    [SerializeField, Header("�C�x���g�~�b�V�����\���I�u�W�F�N�g")]
    private MeshRenderer mEventMission;

    [SerializeField, Header("�A�C�e���~�b�V�����\���I�u�W�F�N�g")]
    private MeshRenderer mItemMission;

    private List<MeshRenderer> mTextureMaterials = new List<MeshRenderer>();

    private int mNowMissionSelect = 0;//���I���肷��~�b�V�����ԍ�(0~2)

    Coroutine coroutine = null;  // �R���[�`���̎��s���Ǘ�����ϐ�
    // Start is called before the first frame update
    void Start()
    {
        mTextureMaterials.Add(mStatusMission);
        mTextureMaterials.Add(mEventMission);
        mTextureMaterials.Add(mItemMission);

        CS_SetPheseController.OnPlayPerformance += DecisionMission;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�~�b�V��������
    private void DecisionMission(int _num)
    {
        if(coroutine == null) { coroutine = StartCoroutine(ChangeMaterial(_num)); }
    }

    //�}�e���A����ύX
    private IEnumerator ChangeMaterial(int _num)
    {
        yield return new WaitForSeconds(2f);

        Material[] materials = mTextureMaterials[mNowMissionSelect].materials;//materials���擾
        //0�Ԗڂ��~�b�V�������ɐݒ肵���}�e���A���ɕύX
        materials[0] = mMissionStatus.infomation[mNowMissionSelect].mission[_num].missionTextureMaterial;
        //�ύX�����z����ēx�ݒ�
        mTextureMaterials[mNowMissionSelect].materials = materials;

        mNowMissionSelect++;//�~�b�V�����ԍ����C���N�������g

        //�e�X�g�p
        if (mNowMissionSelect == 3)
        {
            mNowMissionSelect = 0;
        }

        //���o�I���p�X�N���v�g�𐶐�
        CS_SetPerformanceFinish spcFinish = this.gameObject.AddComponent<CS_SetPerformanceFinish>();
        spcFinish.Timer = 2f;//�I���܂ł̎��Ԃ�1�b�ɕύX

        coroutine = null;
        yield return null;
    }
}
