using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSO_MissionData : MonoBehaviour
{
    public enum MISSION_TYPE
    {
        COLLECT = 0,    //���W
        SUBJUGATION,//����
        TRAINING,   //�g���[�j���O
    }
    private MISSION_TYPE mMisisonNumber = MISSION_TYPE.COLLECT;
    
    //�~�b�V�����̎�ނ�ݒ�A�擾
    public MISSION_TYPE MissionNumber
    {
        set
        {
            mMisisonNumber = value;
        }
        get
        {
            return mMisisonNumber;
        }
    }
     
}
