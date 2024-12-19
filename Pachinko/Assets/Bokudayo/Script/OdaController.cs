using UnityEngine;

public class OdaController : MonoBehaviour
{
    // Animator �R���|�[�l���g
    private Animator animator;

    // �ŏI�I��z���W
    public float targetZ = -10f;
    // �ړ����x�i�ǂꂭ�炢�̑����ňړ����邩�j
    public float moveSpeed = 1f;

    // �ŏ��̈ʒu��ۑ�
    private float startZ;
    // �ړ��J�n����
    private float startTime;

    // �ړ������ǂ����̃t���O
    private bool isMoving = true;

    // �����̓G�L�����N�^�[�̃X�N���v�g�iEnemyController�̔z��j
    public EnemyController[] enemyControllers;

    void Start()
    {
        // Animator �R���|�[�l���g�̎擾
        animator = GetComponent<Animator>();

        // ������z���W��ۑ�
        startZ = transform.position.z;
        // �Q�[���J�n���Ɉړ����J�n
        startTime = Time.time;
    }

    void Update()
    {
        // �ړ����̏ꍇ�̂�z���W����
        if (isMoving)
        {
            // �Q�[�����n�܂��Ă���̌o�ߎ��Ԃ��v�Z
            float elapsedTime = Time.time - startTime;

            // �o�ߎ��ԂɊ�Â���z���W����
            float newZ = Mathf.Lerp(startZ, targetZ, elapsedTime * moveSpeed);

            // �V����z���W��ݒ�
            transform.position = new Vector3(transform.position.x, transform.position.y, newZ);

            // z���W���ڕW�ɒB������A�ړ����~
            if (Mathf.Approximately(newZ, targetZ))
            {
                isMoving = false; // �ړ��I��

                // �����̓G�L�����N�^�[�����Ԃɓ����铮����J�n
                foreach (var enemyController in enemyControllers)
                {
                    if (enemyController != null)
                    {
                        enemyController.StartEscape();  // EnemyController��StartEscape���\�b�h���Ăяo��
                    }
                }
            }
        }

        // �X�y�[�X�L�[�������ꂽ���m�F
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // SpacePressed �g���K�[���Z�b�g���đJ�ڂ𔭐�������
            animator.SetTrigger("SpacePressed");
        }
    }
}
