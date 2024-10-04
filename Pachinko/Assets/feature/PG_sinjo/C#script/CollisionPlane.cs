using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollisionPlane : MonoBehaviour
{
    [SerializeField]
    Heso heso = null;  // �Q�[���̏�Ԃ�f�[�^���Ǘ�����J�X�^���N���X

    [SerializeField]
    TextMeshProUGUI[] texts = null;  // UI�e�L�X�g�I�u�W�F�N�g�̔z��

    Coroutine coroutine = null;  // �R���[�`���̎��s���Ǘ�����ϐ�

    // �����l�ݒ�
    int[] value = new int[3] { 1, 1, 1 };

    void Start()
    {

    }

    void Update() 
    {
        if (coroutine == null && heso.stock.Count != 0) 
        {
            coroutine = StartCoroutine(RealTex());
        }
    }


    // ���I���J�n���郁�\�b�h
    IEnumerator RealTex()
    {
        // stock�̗v�f��������Ă��邩�m�F
        if (heso.stock.Count == 0 || heso.stock[0].Length < 3)
        {
            Debug.LogError("stock�ɏ\���ȗv�f������܂���");
            yield break;
        }

        float time = 0.0f;
        value = heso.stock[0];
        heso.DisableStock();

        while (time <= 2.0f)
        {
            value[0]++;
            value[1]++;
            value[2]++;

            value[0] %= 10;
            value[1] %= 10;
            value[2] %= 10;

            texts[0].text = value[0].ToString();
            texts[1].text = value[1].ToString();
            texts[2].text = value[2].ToString();

            time += Time.deltaTime;
            yield return null;
        }

        texts[0].text = "";
        texts[1].text = "";
        texts[2].text = "";

        yield return new WaitForSeconds(1.0f);

        // stock�̃T�C�Y���ēx�m�F���Ă���l��\��
        if (heso.stock.Count > 0 && heso.stock[0].Length >= 3)
        {
            texts[0].text = heso.stock[0][0].ToString();
            yield return new WaitForSeconds(0.5f);
            texts[1].text = heso.stock[0][1].ToString();
            yield return new WaitForSeconds(0.5f);
            texts[2].text = heso.stock[0][2].ToString();
        }
        else
        {
            Debug.LogError("stock�ɏ\���ȃf�[�^������܂���");
        }

        yield return new WaitForSeconds(2.0f);

        coroutine = null;
        heso.stock.RemoveAt(0);
        yield return null;
    }


}

