using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColorTransfer : MonoBehaviour
{
    public GameObject[] selectBack; // 色を移動させるCubeの配列
    public GameObject[] missonPlate; //文字を表示するオブジェクト
   

    [SerializeField, Header("非選択時に加算するマテリアル")]
    private Material mNoSelectMaterial;

    private int selectNum = 0;

    private void Start()
    {
        // ボタンが押されたときのイベントを設定
        FindButtons();

        Material[] materials = selectBack[0].GetComponent<Renderer>().materials;//materialsを取得
        //0番目をミッション情報に設定したマテリアルに変更
        materials[1] = mNoSelectMaterial;
        //変更した配列を再度設定
        selectBack[1].GetComponent<Renderer>().materials = materials;
        selectBack[2].GetComponent<Renderer>().materials = materials;
    }

    private void FindButtons()
    {
        Canvas canvas = GameObject.Find("ButtonCanvas").GetComponent<Canvas>();
        Button[] buttons = canvas.GetComponentsInChildren<Button>();

        string[] buttonNames = new string[3] { "ButtonLeft", "BottonRight", "ButtonPush" };

        for(int i = 0; i < buttons.Length; i++)
        {
            if(buttons[i].name == "ButtonLeft") { buttons[i].onClick.AddListener(MoveLeft); }
            else if(buttons[i].name == "ButtonRight") { buttons[i].onClick.AddListener(MoveRight); }
            else if (buttons[i].name == "ButtonPush") { buttons[i].onClick.AddListener(MissionDecision); }
        }
    }

    private void MoveRight()
    {
        // 最後のCubeの色を保存
        Material[] lastColors = selectBack[selectBack.Length - 1].GetComponent<Renderer>().materials;

        // 色を隣のオブジェクトに移動させる
        for (int i = selectBack.Length - 1; i > 0; i--)
        {
            Material[] currentMat = selectBack[i - 1].GetComponent<Renderer>().materials;
            selectBack[i].GetComponent<Renderer>().materials = currentMat;
        }

        // 最初のCubeの色を最後のCubeの色に設定
        selectBack[0].GetComponent<Renderer>().materials= lastColors;

        //選択番号加算
        selectNum++;
        //selectBackのリストの範囲を超えないようにする
        if (selectNum >= selectBack.Length) {　selectNum = 0;　}
    }

    private void MoveLeft()
    {
        // 最後のCubeの色を保存
        Material[] lastColors = selectBack[0].GetComponent<Renderer>().materials;

        // 色を隣のオブジェクトに移動させる
        for (int i = 0; i < selectBack.Length - 1; i++)
        {
            Material[] currentMat = selectBack[i + 1].GetComponent<Renderer>().materials;
            selectBack[i].GetComponent<Renderer>().materials = currentMat;
        }

        // 最初のCubeの色を最後のCubeの色に設定
        selectBack[selectBack.Length - 1].GetComponent<Renderer>().materials = lastColors;

        selectNum--;

        //selectBackのリストの範囲を超えないようにする
        if (selectNum < 0) { selectNum = selectBack.Length - 1; }
    }

    private void MissionDecision()
    {
        //選ばれてないミッションは消す
        for (int i = 0; i < selectBack.Length; i++)
        {
            if(i != selectNum)
            {
                Destroy(selectBack[i]);
                Destroy(missonPlate[i]);
            }
        }

        //数秒後にミッションフェーズへ
        StartCoroutine(GoMissionPhase());
    }

    //数秒待ってからミッションフェーズに行く
    private IEnumerator GoMissionPhase()
    {
        yield return new WaitForSeconds(2f);

        //ミッションセレクトを消す
        Destroy(GameObject.Find("MissionSelect"));

        CS_Controller bigctrl = GameObject.Find("BigController").GetComponent<CS_Controller>();//司令塔大を取得

        //仮処理
        bigctrl.ChangePhase(CS_Controller.PACHINKO_PHESE.MISSION);
        
        CS_MissionPhaseData mData = GameObject.Find("BigController").GetComponent<CS_MissionPhaseData>();
        mData.MissionType = (CS_MissionPhaseData.MISSION_TYPE)selectNum;

        bigctrl.CreateController();
        yield return null;
    }
}
