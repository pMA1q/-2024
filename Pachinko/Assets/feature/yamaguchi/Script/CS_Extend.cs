using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_Extend : MonoBehaviour
{
    private int minGame = 1;
    private int maxGame = 3;

    private int remainGame = 10; //���݂̃Q�[����

    //UI�̐ݒ�(�Q�[�����\��)
    public Text remainGameText;

    // Start is called before the first frame update
    void Start()
    {
        UpdateRemaiGameUI();
    }

    //�����@�\���Ăяo��
    public void TriggerExtraGames()
    {
        int extraGame = Random.Range(minGame,maxGame + 1);
        remainGame += extraGame;
        Debug.Log("����:" + remainGameText + "G�ǉ�����܂����I");

        UpdateRemaiGameUI();
    }

    //�c��Q�[������1���炵,0�ɂȂ����ꍇ�͉��������s
    public void PlayGameRound()
    {
        if (remainGame > 0)
        {
            remainGame--;
            UpdateRemaiGameUI();

            if(remainGame  == 0)
            {
                //�Q�[���I�����ɉ��������s
                TriggerExtraGames();
            }
        }
        else
        {
            Debug.Log("�Q�[���I�[�o�[�ł�");
        }
    }

    // �c��Q�[����UI�̍X�V
    void UpdateRemaiGameUI()
    {
        remainGameText.text = "�c��G��: " + remainGame;
    }
}
