using NUnit.Framework;
using System;
using System.Collections.Generic;
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

    [Header("Multi Stacking Crystal")]
    [SerializeField] private bool canUseMultiStack;
    [SerializeField] private int amountOfStacks;
    [SerializeField] private float multiStackCooldown;
    [SerializeField] private float useTimeWindow;
    [SerializeField] private List<GameObject> crystalLeft = new List<GameObject>();

    public override void UseSkill()
    {
        base.UseSkill();

        if (CanUseMultiCrystal())
            return;

        if (currentCrystal == null)
        {
            currentCrystal = Instantiate(crystalPrefab, player.transform.position, Quaternion.identity);
            Crystal_Skill_Controller currentCrystalScript = currentCrystal.GetComponent<Crystal_Skill_Controller>();

            currentCrystalScript.SetupCrystal(crystalDuration,moveSpeed, crystalGrowSpeed, FindClosestEnemy(currentCrystal.transform), canMoveToEnemy, canExplode);
        }
        else
        {
            if (canMoveToEnemy)
                return;

            Vector2 playerPos = player.transform.position;

            player.transform.position = currentCrystal.transform.position;

            currentCrystal.transform.position = playerPos;
            currentCrystal.GetComponent<Crystal_Skill_Controller>()?.FinishCrystal();
        }
    }

    private bool CanUseMultiCrystal()
    {
        if (canUseMultiStack)
        {
            // respawn crystal
            if (crystalLeft.Count > 0)
            {
                if (crystalLeft.Count == amountOfStacks)
                    Invoke("ResetAbility", useTimeWindow);

                cooldown = 0;

                GameObject crystalToSpawn = crystalLeft[crystalLeft.Count - 1];
                GameObject newCrystal = Instantiate(crystalToSpawn, player.transform.position, Quaternion.identity);
                crystalLeft.Remove(crystalToSpawn);

                newCrystal.GetComponent<Crystal_Skill_Controller>().
                    SetupCrystal(crystalDuration, moveSpeed, crystalGrowSpeed, FindClosestEnemy(newCrystal.transform), canMoveToEnemy, canExplode);

                if (crystalLeft.Count <= 0)
                {
                    cooldown = multiStackCooldown;
                    RefillCrystal();
                }
                
                return true;
            }
        }

        return false;
    }

    private void RefillCrystal()
    {
        int amountToAdd = amountOfStacks - crystalLeft.Count;

        for (int i = 0; i < amountToAdd; i++)
        {
            crystalLeft.Add(crystalPrefab);
        }
    }

    private void ResetAbility()
    {
        if (cooldownTimer > 0)
            return;

        cooldownTimer = multiStackCooldown;
        RefillCrystal();
    }
}
