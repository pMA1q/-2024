using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeText : MonoBehaviour
{
    //"sampletext"��GameObject���擾
    GameObject sampletext;

    // Start is called before the first frame update
    void Start()
    {

        //"sampletext"��text���e���u�����������v�ɍX�V
        sampletext.GetComponent<TextMesh>().text = "����������";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
