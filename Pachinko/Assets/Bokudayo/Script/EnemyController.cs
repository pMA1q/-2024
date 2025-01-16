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

    // �|��邽�߂̉�]�p�����[�^
    private bool isKnockedDown = false;
    private float knockdownRotationTime = 0f;
    public float knockdownDuration = 3f;  // �|��铮��̎��Ԃ𒲐��i�傫������قǒx���|���j
    public float knockdownDelay = 1f;    // �|���܂ł̒x�����ԁi�Ⴆ��1�b�x�点��j

    // �|��铮��J�n�̎���
    private float knockdownStartTime;

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

        // �|���A�j���[�V�����i��]��K�p�j
        if (isKnockedDown)
        {
            // �|���܂ł̒x�����o�߂�����A�|��n�߂�
            if (Time.time - knockdownStartTime >= knockdownDelay)
            {
                // �|��铮��̐i�s
                knockdownRotationTime += Time.deltaTime / knockdownDuration;  // knockdownDuration�œ|��鎞�Ԃ𒲐�
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-90f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z), knockdownRotationTime);

                // ��]������������|�ꂽ��Ԃɕێ�
                if (knockdownRotationTime >= 1f)
                {
                    isKnockedDown = false; // ����œ|��铮�삪�I��
                }
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
            targetRotation = Quaternion.Euler(startRotation.x, startRotation.y + 180.0f, startRotation.z);  // �ڕW��]��ݒ�iy����180�x��]�j

            rotationTime = 0f;  // ��]�J�n���ɐi�s�󋵂����Z�b�g

            // �����铮����J�n����O�ɉ�]���J�n
            isEscaping = true;
            escapeStartTime = Time.time + 2f; // 2�b��ɓ�����J�n
        }
    }

    // �U�����󂯂ē|��鏈��
    public void KnockDown()
    {
        isKnockedDown = true;  // �|��鏈�����J�n
        knockdownRotationTime = 0f; // ������
        knockdownStartTime = Time.time;  // �|��铮����J�n�������Ԃ��L�^
        Debug.Log("�m�b�N�_�E���J�n");
    }

    // �����铮����~���邽�߂̃��\�b�h�i�I�v�V�����j
    public void StopEscape()
    {
        isEscaping = false;
    }
}
