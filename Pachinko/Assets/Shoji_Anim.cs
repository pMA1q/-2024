using UnityEngine;

public class ShojiAnimController : MonoBehaviour
{
    public Animator shojiAnimator; // ��q��Animator
    public Animator cameraAnimator; // �J������Animator
    public Animator brightenAnimator; // ���]�p��Animator
    public GameObject enemy; // �G�I�u�W�F�N�g
    public Material enemyShadowMaterial; // �e��Ԃ�Material
    public Material enemyVisibleMaterial; // �\����Ԃ�Material

    void Start()
    {
        // �������F�G���e��Ԃɐݒ�
        enemy.GetComponent<Renderer>().material = enemyShadowMaterial;
    }

    public void StartScene()
    {
        StartCoroutine(PlayScene());
    }

    private IEnumerator PlayScene()
    {
        // ��q�����J��
        shojiAnimator.SetTrigger("OpenSlightly");
        cameraAnimator.SetTrigger("ZoomIn");
        yield return new WaitForSeconds(1.5f); // �����J���A�j���[�V�����̒���

        // ��q�S�J
        shojiAnimator.SetTrigger("OpenFully");
        yield return new WaitForSeconds(1.5f); // �S�J�A�j���[�V�����̒���

        // ���]
        brightenAnimator.SetTrigger("Brighten");
        yield return new WaitForSeconds(2f);

        // �G�̕\����؂�ւ�
        enemy.GetComponent<Renderer>().material = enemyVisibleMaterial;
    }
}
