using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // �����鑬�x
    public float escapeSpeed = 3f;

    // �����铮����I���E�I�t�ł���t���O
    public bool canEscape = true;

    // �����鎞�ԁi2�b��ɊJ�n�j
    private bool isEscaping = false;
    private float escapeStartTime;

    void Update()
    {
        // �����铮�삪�I���̏ꍇ�ɂ̂ݎ��s
        if (canEscape && isEscaping)
        {
            float elapsedTime = Time.time - escapeStartTime;
            // 2�b��ɓ����鏈�����J�n
            if (elapsedTime >= 0)
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
