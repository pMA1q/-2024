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

    // �����铮����J�n���邽�߂̃t���O
    private bool isStopped = false;
    private float stopTime;

    // �����~�܂��Ă���A�j���[�V�����J�ڂ��J�n���邽�߂̃t���O
    private bool animationTriggered = false;
    private float animationDelay = 2f;  // 2�b��ɃA�j���[�V�����J��
    private float animationStartTime;

    // �����̓G���i�[����z��
    public EnemyController[] enemyControllers;

    // �U���\���ǂ����̃t���O�i�O�����琧��\�j
    public bool canAttack = true;

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
        // �Q�[�����n�܂��Ă���̌o�ߎ��Ԃ��v�Z
        float elapsedTime = Time.time - startTime;

        // �o�ߎ��ԂɊ�Â���z���W����
        float newZ = Mathf.Lerp(startZ, targetZ, elapsedTime * moveSpeed);

        // �V����z���W��ݒ�
        transform.position = new Vector3(transform.position.x, transform.position.y, newZ);

        // z���W���ڕW�ɒB������A�ړ����~
        if (Mathf.Approximately(newZ, targetZ) && !isStopped)
        {
            // ��~�t���O��ݒ肵�āA��~���2�b�ҋ@���J�n
            isStopped = true;
            stopTime = Time.time;
            animationStartTime = Time.time;  // �A�j���[�V�����̃J�E���g�J�n
        }

        // ��~��2�b�ҋ@������A�U���A�j���[�V�����J�ڂ��J�n
        if (isStopped && !animationTriggered && Time.time - animationStartTime >= animationDelay && canAttack)
        {
            // �A�j���[�V������J�ڂ�����
            animator.SetTrigger("SpacePressed");

            // �A�j���[�V�����J�ڂ������������Ƃ��L�^
            animationTriggered = true;

            // �U�������������̂ŁA�G���|���悤�ɂ���
            foreach (var enemyController in enemyControllers)
            {
                if (enemyController != null)
                {
                    enemyController.KnockDown(); // �G��|��
                }
            }
        }

        // ��~��2�b�ҋ@������A�G�̓����铮����J�n
        if (isStopped && Time.time - stopTime >= 2f)
        {
            // 2�b��ɑS�Ă̓G�𓦂�������
            foreach (var enemyController in enemyControllers)
            {
                if (enemyController != null)
                {
                    enemyController.StartEscape();
                }
            }
            isStopped = false; // �����鏈�����J�n���ꂽ��A��~�t���O�����Z�b�g
        }

        // �X�y�[�X�L�[�������ꂽ���m�F
        if (Input.GetKeyDown(KeyCode.Space) && canAttack)
        {
            // SpacePressed �g���K�[���Z�b�g���đJ�ڂ𔭐�������
            animator.SetTrigger("SpacePressed");
        }
    }

    // �O������U���̃I���I�t�𐧌䂷�郁�\�b�h
    public void ToggleAttack(bool isEnabled)
    {
        canAttack = isEnabled;
    }
}
