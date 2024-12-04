using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill",menuName = "Skill")]
public class Skill : ScriptableObject
{
    public string skillname; 
    public string description; //�X�L���̐���
}

public class CS_Skill : MonoBehaviour
{
    public List<Skill> skillList = new List<Skill>();

    public void AddSkill(Skill skill)
    {
        skillList.Add(skill);
        Debug.Log($"{skill.skillname}���擾���܂����I");
    }
}
