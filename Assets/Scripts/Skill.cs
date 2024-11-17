using TMPro;
using UnityEngine.UI;
using UnityEngine;
using static SkillTree;

public class Skill : MonoBehaviour
{
    public int id;
    public TMP_Text TitleText;
    public TMP_Text DescriptionText;

    public int[] ConnectedSkills;

    public void UpdateUI()
    {
        TitleText.text = $"{skillTree.SkillLevels[id]}/{skillTree.SkillCaps[id]}\n{SkillTree.skillTree.SkillNames[id]}";
        DescriptionText.text = $"{skillTree.SkillDescriptions[id]}\nCost: {skillTree.SkillPoint}/1 SP";

        GetComponent<Image>().color = skillTree.SkillLevels[id] >= skillTree.SkillCaps[id] ? Color.yellow :
        skillTree.SkillPoint > 0 ? Color.green : Color.white;

        foreach (int connectedSkill in ConnectedSkills)
        {
            skillTree.SkillList[connectedSkill].gameObject.SetActive(skillTree.SkillLevels[id] > 0);
            skillTree.ConnectorList[connectedSkill].SetActive(skillTree.SkillLevels[id] > 0);
        }
    }

    public void Buy()
    {
        if (skillTree.SkillPoint < 1 || skillTree.SkillLevels[id] >= skillTree.SkillCaps[id]) return;

        skillTree.SkillPoint -= 1;
        skillTree.SkillLevels[id]++;
        skillTree.UpdateAllSkillUI();
    }
}
