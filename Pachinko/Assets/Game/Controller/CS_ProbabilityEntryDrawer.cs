using UnityEngine;
using UnityEditor;

// ProbabilityEntry�̃J�X�^��PropertyDrawer���쐬
[CustomPropertyDrawer(typeof(ProbabilityEntry))]
public class ProbabilityEntryDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // �v���p�e�B�ƑΉ����郉�x����z��ɂ܂Ƃ߂�
        (SerializedProperty prop, string label)[] properties = new (SerializedProperty, string)[]
        {
            (property.FindPropertyRelative("name"), "���o��"),
            (property.FindPropertyRelative("value"), "�m��"),
            (property.FindPropertyRelative("performancePrefab"), "���o�v���n�u")
        };

        // �e�v���p�e�B�̍���
        float singleLineHeight = EditorGUIUtility.singleLineHeight;
        float spacing = EditorGUIUtility.standardVerticalSpacing;

        // �e�v���p�e�B��`��
        for (int i = 0; i < properties.Length; i++)
        {
            Rect fieldRect = new Rect(position.x, position.y + i * (singleLineHeight + spacing), position.width, singleLineHeight);
            EditorGUI.PropertyField(fieldRect, properties[i].prop, new GUIContent(properties[i].label));
        }

        EditorGUI.EndProperty();
    }

    // Height���J�X�^�}�C�Y���邽�߂ɕK�v�Ȋ֐�
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // �e�v���p�e�B���̍������m�� (3�̃v���p�e�B�� + �X�y�[�V���O)
        return (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * 3;
    }
}
