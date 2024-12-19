using UnityEngine;

public class RoomSelect : MonoBehaviour
{
    public Animator cameraAnimator; // カメラ用Animator
    public GameObject leftEnemy;   // 左の部屋（敵がる場合）
    public GameObject centerEnemy; // 真ん中の部屋（敵がいる場合）
    public GameObject rightEnemy;  // 右の部屋（敵がいる場合）

    public GameObject leftEmpty;   // 左の部屋（敵がいない場合）
    public GameObject centerEmpty; // 真ん中の部屋（敵がいない場合）
    public GameObject rightEmpty;  // 右の部屋（敵がいない場合）

    private void Start()
    {
        // 敵を有効化
        //leftEnemy.SetActive;
        //centerEnemy.SetActive;
        //rightEnemy.SetActive;

        // 敵がいない場合はEmptyオブジェクトを有効化
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
            Debug.Log("敵が出現");
            // 敵がいる場合の追加演出をここに記述
        }
        else
        {
            Debug.Log("誰もいない");
            // 敵がいない場合の追加演出をここに記述
        }
    }
}
