using UnityEngine;

public class ApplyMaterialToAll : MonoBehaviour
{
    public Material newMaterial;  // �V�����}�e���A�����C���X�y�N�^�[����ݒ�

    void Start()
    {
        // ���̃I�u�W�F�N�g�̂��ׂĂ�MeshRenderer�R���|�[�l���g���擾
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();

        // �eMeshRenderer�ɐV�����}�e���A����K�p
        foreach (MeshRenderer renderer in renderers)
        {
            renderer.material = newMaterial;
        }
    }
}
