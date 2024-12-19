using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAnimatorParentManager : MonoBehaviour
{

    [SerializeField, Header("�A�j���[�^�[")]
    Animator sceneAnimator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // �E���L�[��bRight��true�ɐݒ�
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            sceneAnimator.SetBool("bRight", true);
        }

        // �����L�[��bLeft��true�ɐݒ�
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            sceneAnimator.SetBool("bLeft", true);
        }
    }
}