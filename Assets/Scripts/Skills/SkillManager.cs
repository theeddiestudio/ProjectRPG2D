using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager manager;

    public Blackhole_Skill blackhole {  get; private set; }
    public Dash_Skill dash {  get; private set; }
    public Clone_Skill clone { get; private set; }
    public Sword_Skill sword { get; private set; }

    private void Awake()
    {
        if (manager != null)
            Destroy(manager.gameObject);
        else
            manager = this;
    }

    private void Start()
    {
        blackhole = GetComponent<Blackhole_Skill>();
        dash = GetComponent<Dash_Skill>();
        clone = GetComponent<Clone_Skill>();
        sword = GetComponent<Sword_Skill>();
    }
}
