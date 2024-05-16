using System;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat damage;
    public Stat maxHp;
    [Space]
    [SerializeField] private int currentHp;

    private void Start()
    {
        currentHp = maxHp.GetValue();
    }

    public virtual void TakeDamage(int _damage)
    {
        currentHp -= _damage;

        if (currentHp <= 0)
            Die();
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
