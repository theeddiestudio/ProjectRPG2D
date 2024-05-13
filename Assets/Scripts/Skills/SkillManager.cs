using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager manager;

    public Dash_Skill dash {  get; private set; }
    public Clone_Skill clone { get; private set; }

    private void Awake()
    {
        if (manager != null)
            Destroy(manager.gameObject);
        else
            manager = this;
    }

    private void Start()
    {
        dash = GetComponent<Dash_Skill>();
        clone = GetComponent<Clone_Skill>();
    }
}
