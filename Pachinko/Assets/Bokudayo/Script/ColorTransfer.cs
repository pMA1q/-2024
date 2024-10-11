using UnityEngine;
using UnityEngine.UI;

public class ColorTransfer : MonoBehaviour
{
    public GameObject[] cubes; // 色を移動させるCubeの配列
    public Button transferButton; // ボタン

    private void Start()
    {
        // ボタンが押されたときのイベントを設定
        transferButton.onClick.AddListener(MoveColor);
    }

    private void MoveColor()
    {
        // 最後のCubeの色を保存
        Color lastColor = cubes[0].GetComponent<Renderer>().material.color;

        // 色を隣のオブジェクトに移動させる
        for (int i = 0; i < cubes.Length - 1; i++)
        {
            Color currentColor = cubes[i + 1].GetComponent<Renderer>().material.color;
            cubes[i].GetComponent<Renderer>().material.color = currentColor;
        }

        // 最初のCubeの色を最後のCubeの色に設定
        cubes[cubes.Length - 1].GetComponent<Renderer>().material.color = lastColor;
    }
}
