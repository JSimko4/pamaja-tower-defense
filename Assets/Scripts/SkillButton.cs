using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SkillButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject skillPrefab;
    [SerializeField]
    private TextMeshProUGUI hoverText;
    public static GameObject ClickedSkillPrefab { get; set; }
    [SerializeField]
    private TextMeshProUGUI hoverTextInfo;
    [SerializeField]
    private GameObject skillsInfo;
    public GameObject SkillPrefab
    {
        get
        {
            return skillPrefab;
        }
    }
    private GameObject prefabInstance;

    void Start()
    {
        prefabInstance = Instantiate(skillPrefab);
        prefabInstance.SetActive(false);
        Skill skill = prefabInstance.GetComponentInChildren<Skill>();
        hoverText.text = $"{skill.ManaCost} m.";
        hoverTextInfo.text = $"{skill.SkillName}\n \nManacost: {skill.ManaCost}\nRadius: {skill.Radius}";
        if (hoverText != null)
            hoverText.enabled = false;
        if (hoverTextInfo != null)
        {
            hoverTextInfo.enabled = false;
            skillsInfo.SetActive(false);
        }
    }
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
        if (GameManager.Instance.GameLost) return;

        SelectSpell(skillPrefab);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GameManager.Instance.GameLost) return;

        if (hoverText != null)
            hoverText.enabled = true; // Show the hover text
        if (hoverTextInfo != null)
        {
            hoverTextInfo.enabled = true;
            skillsInfo.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoverText != null)
            hoverText.enabled = false; // Hide the hover text
        if (hoverTextInfo != null)
        {
            hoverTextInfo.enabled = false;
            skillsInfo.SetActive(false);
        }
    }
}
