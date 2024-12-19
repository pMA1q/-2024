using UnityEngine;

public class RoomSelect : MonoBehaviour
{
    public Animator cameraAnimator; // �J�����pAnimator
    public GameObject leftEnemy;   // ���̕����i�G����ꍇ�j
    public GameObject centerEnemy; // �^�񒆂̕����i�G������ꍇ�j
    public GameObject rightEnemy;  // �E�̕����i�G������ꍇ�j

    public GameObject leftEmpty;   // ���̕����i�G�����Ȃ��ꍇ�j
    public GameObject centerEmpty; // �^�񒆂̕����i�G�����Ȃ��ꍇ�j
    public GameObject rightEmpty;  // �E�̕����i�G�����Ȃ��ꍇ�j

    private void Start()
    {
        // �G��L����
        //leftEnemy.SetActive;
        //centerEnemy.SetActive;
        //rightEnemy.SetActive;

        // �G�����Ȃ��ꍇ��Empty�I�u�W�F�N�g��L����
        leftEmpty.SetActive(!leftEnemy.activeSelf);
        centerEmpty.SetActive(!centerEnemy.activeSelf);
        rightEmpty.SetActive(!rightEnemy.activeSelf);
    }

    public void SelectLeftRoom()
    {
        cameraAnimator.SetTrigger("Left");
        ShowEnemy(leftEnemy, leftEmpty);
    }

    public void SelectCenterRoom()
    {
        cameraAnimator.SetTrigger("Center");
        ShowEnemy(centerEnemy, centerEmpty);
    }

    public void SelectRightRoom()
    {
        cameraAnimator.SetTrigger("Right");
        ShowEnemy(rightEnemy, rightEmpty);
    }

    private void ShowEnemy(GameObject enemy, GameObject empty)
    {
        if (enemy.activeSelf)
        {
            Debug.Log("�G���o��");
            // �G������ꍇ�̒ǉ����o�������ɋL�q
        }
        else
        {
            Debug.Log("�N�����Ȃ�");
            // �G�����Ȃ��ꍇ�̒ǉ����o�������ɋL�q
        }
    }
}
