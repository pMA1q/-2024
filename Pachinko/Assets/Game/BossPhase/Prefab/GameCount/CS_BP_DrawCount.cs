using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_BP_DrawCount : MonoBehaviour
{
    // �����X�v���C�g���X�g (0~9�̉摜)
    public Sprite[] numberSprites;

    // ������z�u����e�I�u�W�F�N�g (RectTransform)
    public RectTransform numbersParent;

    // Image�v���n�u (���Image�R���|�[�l���g�����I�u�W�F�N�g���쐬���o�^)
    public GameObject imagePrefab;

    // Image�R���|�[�l���g�̃��X�g
    private List<Image> digitImages = new List<Image>();

    // �\������Ԋu
    public float spacing = 50f;

    private void Start()
    {

    }
    /// <summary>
    /// �J�E���g��ݒ肷��
    /// </summary>
    /// <param name="count">�\�����鐔�l</param>
    public void SetCount(int count)
    {
        if (count < 0)
        {
            // 0�����̏ꍇ: numberSprites�̍Ō�̃X�v���C�g��3�\��
            ShowNegativePlaceholder();
        }
        else
        {
            // �ʏ�̐��l�\��
            ShowPositiveCount(count);
        }
    }

    /// <summary>
    /// �ʏ�̐��l�\������
    /// </summary>
    private void ShowPositiveCount(int count)
    {
        string countStr = count.ToString(); // ���l�𕶎���ɕϊ�

        // �K�v�Ȍ�������Image�R���|�[�l���g���m��
        EnsureDigitImages(countStr.Length);

        // �e���̃X�v���C�g��ݒ�
        for (int i = 0; i < countStr.Length; i++)
        {
            int digit = int.Parse(countStr[i].ToString()); // 1�̈ʂ��珇�ԂɎ擾
            digitImages[i].sprite = numberSprites[digit];
            digitImages[i].gameObject.SetActive(true);
        }

        // �s�v��Image���A�N�e�B�u��
        for (int i = countStr.Length; i < digitImages.Count; i++)
        {
            digitImages[i].gameObject.SetActive(false);
        }

        // �z�u�𒲐�
        ArrangeDigits(countStr.Length);
    }

    /// <summary>
    /// 0�����̏ꍇ�̃v���[�X�z���_�[�\������
    /// </summary>
    private void ShowNegativePlaceholder()
    {
        const int placeholderCount = 3; // �v���[�X�z���_�[��
        Sprite placeholderSprite = numberSprites[numberSprites.Length - 1]; // �Ō�̃X�v���C�g

        // �K�v�Ȍ�������Image�R���|�[�l���g���m��
        EnsureDigitImages(placeholderCount);

        // �v���[�X�z���_�[��ݒ�
        for (int i = 0; i < placeholderCount; i++)
        {
            digitImages[i].sprite = placeholderSprite;
            digitImages[i].gameObject.SetActive(true);
        }

        // �s�v��Image���A�N�e�B�u��
        for (int i = placeholderCount; i < digitImages.Count; i++)
        {
            digitImages[i].gameObject.SetActive(false);
        }

        // �z�u�𒲐�
        ArrangeDigits(placeholderCount);
    }

    /// <summary>
    /// �K�v�Ȍ�������Image�R���|�[�l���g���m�ۂ���
    /// </summary>
    /// <param name="digitCount">�K�v�Ȍ���</param>
    private void EnsureDigitImages(int digitCount)
    {
        while (digitImages.Count < digitCount)
        {
            GameObject newImageObj = Instantiate(imagePrefab, numbersParent);
            Image newImage = newImageObj.GetComponent<Image>();
            digitImages.Add(newImage);
        }
    }

    /// <summary>
    /// ���𓙊Ԋu�Ŕz�u����
    /// </summary>
    /// <param name="digitCount">�\�����錅��</param>
    private void ArrangeDigits(int digitCount)
    {
        float totalWidth = (digitCount - 1) * spacing;
        float startX = -totalWidth / 2f;

        for (int i = 0; i < digitCount; i++)
        {
            RectTransform rectTransform = digitImages[i].GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(startX + i * spacing, 0);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // �X�y�[�X�L�[�Ńe�X�g
        {
            int randomCount = Random.Range(0, 100); // 0�`99�̃����_���Ȓl
            SetCount(randomCount);
        }
    }
}
