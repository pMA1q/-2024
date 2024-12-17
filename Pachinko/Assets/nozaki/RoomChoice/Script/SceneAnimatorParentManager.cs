using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAnimatorParentManager : MonoBehaviour
{

    [SerializeField, Header("アニメーター")]
    Animator sceneAnimator;    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            sceneAnimator.SetBool("bRight", true);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            sceneAnimator.SetBool("bLeft", true);
        }
    }
}
