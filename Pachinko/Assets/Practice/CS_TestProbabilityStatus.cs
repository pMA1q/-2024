using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CS_TestProbabilityStatus", menuName = "Test/CS_TestProbabilityStatus", order = 1)]

public class CS_TestProbabilityStatus : ScriptableObject
{

    [SerializeField,Header("���o���Əo���m�������")]
    public List<ProbabilityEntry> performances; 
}

[System.Serializable]
public class ProbabilityEntry
{
    public string name;  // �J�X�^������ێ�����
    public float value;   // �m���l�ifloat�j
    public GameObject performancePrefab;
}