using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject skillPrefab;

    public static GameObject ClickedSkillPrefab { get; set; }

    public static void SelectSpell(GameObject prefab)
    {
        Skill skill = prefab.GetComponent<Skill>();
        var skillPrefabInstance = Instantiate(skill.PrecastPrefab);
        skillPrefabInstance.transform.localScale = new Vector3(skill.Radius, skill.Radius);
        ClickedSkillPrefab = prefab;

        Hover.Instance.SetHoverPrefabInstance(skillPrefabInstance);
    }

    public static void ResetHoverPrefab()
    {
        Hover.Instance.SetHoverPrefabInstance(null);
        ClickedSkillPrefab = null;
    }

    public static void CastSkill()
    {
        var requiredMana = ClickedSkillPrefab.GetComponent<Skill>().ManaCost;

        if (GameManager.Instance.Mana >= requiredMana)
        {
            GameManager.Instance.Mana -= requiredMana;
            var skill = Instantiate(ClickedSkillPrefab, Hover.Instance.HoverPrefabInstance.transform.position, Quaternion.identity).GetComponent<Skill>();
        }

        ResetHoverPrefab();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SelectSpell(skillPrefab);
    }
}
