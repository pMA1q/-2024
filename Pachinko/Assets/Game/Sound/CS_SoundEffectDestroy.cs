//---------------------------------
//���ʉ��𗬂��I���������
//�S���ҁF����
//---------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_SoundEffectDestroy : MonoBehaviour
{
    private AudioSource mAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        mAudioSource = GetComponent<AudioSource>();

        if(mAudioSource == null) { Debug.LogError("AudioSource���A�^�b�`���Ă�������"); }
    }

    // Update is called once per frame
    void Update()
    {
        if (!mAudioSource.isPlaying) { Destroy(this.gameObject); }
    }
}
