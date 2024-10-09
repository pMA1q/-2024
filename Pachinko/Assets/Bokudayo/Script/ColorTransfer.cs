using UnityEngine;
using UnityEngine.UI;

public class ColorTransfer : MonoBehaviour
{
    public GameObject[] cubes; // �F���ړ�������Cube�̔z��
    public Button transferButton; // �{�^��

    private void Start()
    {
        // �{�^���������ꂽ�Ƃ��̃C�x���g��ݒ�
        transferButton.onClick.AddListener(MoveColor);
    }

    private void MoveColor()
    {
        // �Ō��Cube�̐F��ۑ�
        Color lastColor = cubes[0].GetComponent<Renderer>().material.color;

        // �F��ׂ̃I�u�W�F�N�g�Ɉړ�������
        for (int i = 0; i < cubes.Length - 1; i++)
        {
            Color currentColor = cubes[i + 1].GetComponent<Renderer>().material.color;
            cubes[i].GetComponent<Renderer>().material.color = currentColor;
        }

        // �ŏ���Cube�̐F���Ō��Cube�̐F�ɐݒ�
        cubes[cubes.Length - 1].GetComponent<Renderer>().material.color = lastColor;
    }
}
