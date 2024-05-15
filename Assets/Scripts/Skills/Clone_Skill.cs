using System.Collections;
using UnityEngine;

public class Clone_Skill : Skill
{
    [Header("Clone Info")]
    [SerializeField] private GameObject clonePrefab;
    [SerializeField] private float cloneDuration;
    [Space]
    [SerializeField] private bool canAttack;

    [Header("Skills using Clones")]
    [SerializeField] private bool createCloneOnDashStart;
    [SerializeField] private bool createCloneOnDashOver;
    [SerializeField] private bool createCloneOnCounterAttack;
    [SerializeField] private bool canDuplicateClone;
    [SerializeField] private float chanceOfDuplication;

    [Header("Crystal Instead of Clone")]
    [SerializeField] private bool crystalInstead;

    public void CreateClone(Transform _clonePosition, Vector3 _offset)
    {
        if (crystalInstead)
        {
            SkillManager.manager.crystal.CreateCrystal();
            return;
        }

        GameObject newClone = Instantiate(clonePrefab, player.transform.position, Quaternion.identity); // this chance is made by me, cause before it would instantiate in 0,0 position and closest enemy would be different and so flip would not work as well.

        newClone.GetComponent<Clone_Skill_Controller>().SetupClone(_clonePosition, _offset, FindClosestEnemy(newClone.transform), cloneDuration, canAttack, canDuplicateClone, chanceOfDuplication);
    }

    public void CreateCloneOnDashStart()
    {
        if (createCloneOnDashStart)
            CreateClone(player.transform, Vector3.zero);
    }

    public void CreateCloneOnDashOver()
    {
        if (createCloneOnDashOver)
            CreateClone(player.transform, Vector3.zero);
    }

    public void CreateCloneOnCounterAttack(Transform _enemyTransform)
    {
        if (createCloneOnCounterAttack)
            StartCoroutine(CreateCloneWithDelay(_enemyTransform, new Vector3(2 * player.facingDir, 0)));
    }

    private IEnumerator CreateCloneWithDelay(Transform _enemyTransform, Vector3 _offset)
    {
        yield return new WaitForSeconds(0.4f);
        CreateClone(_enemyTransform, _offset);
    }
}
