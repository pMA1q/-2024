using UnityEngine;

public class ApplyMaterialToAll : MonoBehaviour
{
    public Material newMaterial;  // 新しいマテリアルをインスペクターから設定

    void Start()
    {
        // このオブジェクトのすべてのMeshRendererコンポーネントを取得
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();

        // 各MeshRendererに新しいマテリアルを適用
        foreach (MeshRenderer renderer in renderers)
        {
            renderer.material = newMaterial;
        }
    }
}
