using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColorTransfer : MonoBehaviour
{
    public GameObject[] selectBack; // �F���ړ�������Cube�̔z��
    public GameObject[] missonPlate; //������\������I�u�W�F�N�g
   

    [SerializeField, Header("��I�����ɉ��Z����}�e���A��")]
    private Material mNoSelectMaterial;

    private int selectNum = 0;

    private void Start()
    {
        // �{�^���������ꂽ�Ƃ��̃C�x���g��ݒ�
        FindButtons();

        Material[] materials = selectBack[0].GetComponent<Renderer>().materials;//materials���擾
        //0�Ԗڂ��~�b�V�������ɐݒ肵���}�e���A���ɕύX
        materials[1] = mNoSelectMaterial;
        //�ύX�����z����ēx�ݒ�
        selectBack[1].GetComponent<Renderer>().materials = materials;
        selectBack[2].GetComponent<Renderer>().materials = materials;
    }

    private void FindButtons()
    {
        Canvas canvas = GameObject.Find("ButtonCanvas").GetComponent<Canvas>();
        Button[] buttons = canvas.GetComponentsInChildren<Button>();

        string[] buttonNames = new string[3] { "ButtonLeft", "BottonRight", "ButtonPush" };

        for(int i = 0; i < buttons.Length; i++)
        {
            if(buttons[i].name == "ButtonLeft") { buttons[i].onClick.AddListener(MoveLeft); }
            else if(buttons[i].name == "ButtonRight") { buttons[i].onClick.AddListener(MoveRight); }
            else if (buttons[i].name == "ButtonPush") { buttons[i].onClick.AddListener(MissionDecision); }
        }
    }

    private void MoveRight()
    {
        // �Ō��Cube�̐F��ۑ�
        Material[] lastColors = selectBack[selectBack.Length - 1].GetComponent<Renderer>().materials;

        // �F��ׂ̃I�u�W�F�N�g�Ɉړ�������
        for (int i = selectBack.Length - 1; i > 0; i--)
        {
            Material[] currentMat = selectBack[i - 1].GetComponent<Renderer>().materials;
            selectBack[i].GetComponent<Renderer>().materials = currentMat;
        }

        // �ŏ���Cube�̐F���Ō��Cube�̐F�ɐݒ�
        selectBack[0].GetComponent<Renderer>().materials= lastColors;

        //�I��ԍ����Z
        selectNum++;
        //selectBack�̃��X�g�͈̔͂𒴂��Ȃ��悤�ɂ���
        if (selectNum >= selectBack.Length) {�@selectNum = 0;�@}
    }

    private void MoveLeft()
    {
        // �Ō��Cube�̐F��ۑ�
        Material[] lastColors = selectBack[0].GetComponent<Renderer>().materials;

        // �F��ׂ̃I�u�W�F�N�g�Ɉړ�������
        for (int i = 0; i < selectBack.Length - 1; i++)
        {
            Material[] currentMat = selectBack[i + 1].GetComponent<Renderer>().materials;
            selectBack[i].GetComponent<Renderer>().materials = currentMat;
        }

        // �ŏ���Cube�̐F���Ō��Cube�̐F�ɐݒ�
        selectBack[selectBack.Length - 1].GetComponent<Renderer>().materials = lastColors;

        selectNum--;

        //selectBack�̃��X�g�͈̔͂𒴂��Ȃ��悤�ɂ���
        if (selectNum < 0) { selectNum = selectBack.Length - 1; }
    }

    private void MissionDecision()
    {
        //�I�΂�ĂȂ��~�b�V�����͏���
        for (int i = 0; i < selectBack.Length; i++)
        {
            if(i != selectNum)
            {
                Destroy(selectBack[i]);
                Destroy(missonPlate[i]);
            }
        }

        //���b��Ƀ~�b�V�����t�F�[�Y��
        StartCoroutine(GoMissionPhase());
    }

    //���b�҂��Ă���~�b�V�����t�F�[�Y�ɍs��
    private IEnumerator GoMissionPhase()
    {
        yield return new WaitForSeconds(2f);

        //�~�b�V�����Z���N�g������
        Destroy(GameObject.Find("MissionSelect"));

        CS_Controller bigctrl = GameObject.Find("BigController").GetComponent<CS_Controller>();//�i�ߓ�����擾

        //������
        bigctrl.ChangePhase(CS_Controller.PACHINKO_PHESE.MISSION);
        
        CS_MissionPhaseData mData = GameObject.Find("BigController").GetComponent<CS_MissionPhaseData>();
        mData.MissionType = (CS_MissionPhaseData.MISSION_TYPE)selectNum;

        bigctrl.CreateController();
        yield return null;
    }
}
