using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;

public class CS_BPTranstision : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    private CS_Controller bigController;
    // Start is called before the first frame update
    void Start()
    {
        //bigController.ChangePhase(CS_Controller.PACHINKO_PHESE.BOSS);
        bigController = GameObject.Find("BigController").GetComponent<CS_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

//�~�b�V�����t�F�[�Y���t�F�[�h�A�E�g���Ă���{�X�t�F�[�Y�փt�F�[�h�C��
//BPController�Ăяo��(�v���n�u/����)
//�~�b�V�����t�F�[�Y����v���C���[�X�e�[�^�X���Q��
