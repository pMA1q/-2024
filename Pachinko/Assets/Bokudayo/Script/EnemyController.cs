using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // �����鑬�x
    public float escapeSpeed = 3f;

    // �����铮����I���E�I�t�ł���t���O
    public bool canEscape = true;

    // ��]�̑���
    public float rotationSpeed = 2f;  // �ǂꂭ�炢�̑����ŉ�]�����邩

    // �����鎞�ԁi2�b��ɊJ�n�j
    private bool isEscaping = false;
    private float escapeStartTime;

    // ��]�̂��߂̕�ԃp�����[�^
    private Quaternion startRotation;
    private Quaternion targetRotation;
    private float rotationTime;

    void Update()
    {
        // �����铮�삪�I���̏ꍇ�ɂ̂ݎ��s
        if (canEscape && isEscaping)
        {
            // ��]�̕�ԁi���X�ɉ�]������j
            if (rotationTime < 1f)
            {
                rotationTime += Time.deltaTime * rotationSpeed;  // ��]���x�ɍ��킹�Đi�s�󋵂𑝉�
                transform.rotation = Quaternion.Lerp(startRotation, targetRotation, rotationTime);  // ���X�ɉ�]
            }

            // ��]������������ɓ����铮����J�n
            if (rotationTime >= 1f)
            {
                // z�������ɓ�����
                transform.position += Vector3.back * escapeSpeed * Time.deltaTime;  // `Vector3.back`��-1�����Ɉړ�
            }
        }
    }

    // �����鏈�����J�n
    public void StartEscape()
    {
        if (canEscape)  // �����铮�삪�I���̏ꍇ�̂ݎ��s
        {
            // ��]���J�n���钼�O�ɉ�]�̏����ݒ���s��
            startRotation = transform.rotation;  // ���݂̉�]��ۑ�
            targetRotation = Quaternion.Euler(0f, 180f, 0f);  // �ڕW��]��ݒ�iy����180�x��]�j

            rotationTime = 0f;  // ��]�J�n���ɐi�s�󋵂����Z�b�g

            // �����铮����J�n����O�ɉ�]���J�n
            isEscaping = true;
            escapeStartTime = Time.time + 2f; // 2�b��ɓ�����J�n
        }
    }

    // �����铮����~���邽�߂̃��\�b�h�i�I�v�V�����j
    public void StopEscape()
    {
        isEscaping = false;
    }
}
