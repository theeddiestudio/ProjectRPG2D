using UnityEngine;

public class Crystal_Skill : Skill
{
    [SerializeField] private GameObject crystalPrefab;
    private GameObject currentCrystal;

    [SerializeField] private float crystalDuration;
    [SerializeField] private float crystalGrowSpeed;

    [Header("Moving Crystal")]
    [SerializeField] private bool canMoveToEnemy;
    [SerializeField] private float moveSpeed;

    [Header("Explosive Crystal")]
    [SerializeField] private bool canExplode;

    public override void UseSkill()
    {
        base.UseSkill();

        if (currentCrystal == null)
        {
            currentCrystal = Instantiate(crystalPrefab, player.transform.position, Quaternion.identity);
            Crystal_Skill_Controller currentCrystalScript = currentCrystal.GetComponent<Crystal_Skill_Controller>();

            currentCrystalScript.SetupCrystal(crystalDuration,moveSpeed, crystalGrowSpeed, canMoveToEnemy, canExplode);
        }
        else
        {
            Vector2 playerPos = player.transform.position;

            player.transform.position = currentCrystal.transform.position;

            currentCrystal.transform.position = playerPos;
            currentCrystal.GetComponent<Crystal_Skill_Controller>()?.FinishCrystal();
        }
    }
}
