using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillTree : MonoBehaviour
{
    public static SkillTree skillTree;
    private void Awake() => skillTree = this;

    public int[] SkillLevels;
    public int[] SkillCaps;
    public string[] SkillNames;
    public string[] SkillDescriptions;

    public List<Skill> SkillList;
    public GameObject SkillHolder;

    public List<GameObject> ConnectorList;
    public GameObject ConnectorHolder;
    public int SkillPoint;

    private void Start()
    {
        SkillPoint = 30;
        SkillLevels = new int[7];
        SkillCaps = new[] { 5, 5, 5, 5, 1, 1, 1 };
        SkillNames = new[] { "Upgrade 1", "Upgrade 2", "Upgrade 3", "Upgrade 4", "Booster 5", "Booster 6", "Booster 7" };
        SkillDescriptions = new[]
        {
            "HP Limit",
            "Speed Limit",
            "Battery Limit",
            "Speed Limit",
            "Shrink Light",
            "Freeze Light",
            "Spawn Light"
        };

        foreach (var skill in SkillHolder.GetComponentsInChildren<Skill>()) SkillList.Add(skill);
        foreach (RectTransform connector in ConnectorHolder.GetComponentsInChildren<RectTransform>()) ConnectorList.Add(connector.gameObject);

        for (var i = 0; i < SkillList.Count; i++) SkillList[i].id = i;

        SkillList[0].ConnectedSkills = new[] { 1, 2, 3 };
        SkillList[2].ConnectedSkills = new[] { 4, 5, 6 };

        UpdateAllSkillUI();
    }

    public void UpdateAllSkillUI()
    {
        foreach (var skill in SkillList) skill.UpdateUI();
    }

}
