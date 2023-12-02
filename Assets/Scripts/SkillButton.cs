using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillButton : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler
{
    [SerializeField]
    private GameObject skillPrefab;

    private Skill skill { get=>skillPrefab.GetComponent<Skill>(); }

    public static GameObject ClickedSkillPrefab { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void CastSkill()
    {
        var requiredMana = ClickedSkillPrefab.GetComponent<Skill>().ManaCost;

        if(GameManager.Instance.Mana >= requiredMana)
        {
            GameManager.Instance.Mana -= requiredMana;
            var skill = Instantiate(ClickedSkillPrefab, Hover.Instance.HoverPrefabInstance.transform.position, Quaternion.identity).GetComponent<Skill>();
        }
       
        //reset hover prefab
        Hover.Instance.SetHoverPrefabInstance(null);
        ClickedSkillPrefab = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var skillPrefabInstance = Instantiate(skill.PrecastPrefab);
        skillPrefabInstance.transform.localScale = new Vector3(skill.Radius, skill.Radius);
        ClickedSkillPrefab = skillPrefab;

        Hover.Instance.SetHoverPrefabInstance(skillPrefabInstance);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
