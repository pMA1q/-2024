using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_CanvasFadeOut : MonoBehaviour
{
    public Canvas canvas; // �t�F�[�h�A�E�g������Canvas
    public float mTimer = 2.0f; // �t�F�[�h�A�E�g�ɂ����鎞��

    private Graphic[] graphics;

    private void Start()
    {
        if (canvas == null)
        {
            Debug.LogError("Canvas���ݒ肳��Ă��܂���B");
            return;
        }

        // Canvas���̂��ׂĂ�Graphic�R���|�[�l���g���擾
        graphics = canvas.GetComponentsInChildren<Graphic>();

        // �t�F�[�h�A�E�g�J�n
        StartCoroutine(FadeOutAndDisableCanvas());
    }

    private IEnumerator FadeOutAndDisableCanvas()
    {
        yield return new WaitForSeconds(5f);
        float elapsedTime = 0f;

        // �����̃A���t�@�l��ۑ�
        float[] initialAlphas = new float[graphics.Length];
        for (int i = 0; i < graphics.Length; i++)
        {
            initialAlphas[i] = graphics[i].color.a;
        }

        while (elapsedTime < mTimer)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / mTimer);

            foreach (var graphic in graphics)
            {
                if (graphic != null)
                {
                    Color color = graphic.color;
                    color.a = alpha;
                    graphic.color = color;
                }
            }

            yield return null;
        }

        // �ŏI�I�Ɋ��S�ɓ����ɂ���
        foreach (var graphic in graphics)
        {
            if (graphic != null)
            {
                Color color = graphic.color;
                color.a = 0f;
                graphic.color = color;
            }
        }

        // Canvas���A�N�e�B�u��
        Destroy(this.transform.root.gameObject);
    }
}
