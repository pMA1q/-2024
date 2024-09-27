using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SetPhaseStatus", menuName = "PhaseStatus/SetPhaseStatus", order = 1)]
public class CSO_SetPhaseStatus : ScriptableObject
{
    [SerializeField, Header("���o���Əo���m�������")]
    public List<ProbabilityEntry> performances;
}


[System.Serializable]
public class ProbabilityEntry
{
    public string name;  // �J�X�^������ێ�����
    public float value=1;   // �m���l�ifloat�j
    public GameObject performancePrefab;
}