using System;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat strength;
    public Stat damage;
    public Stat maxHp;
    [Space]
    [SerializeField] private int currentHp;

    protected virtual void Start()
    {
        currentHp = maxHp.GetValue();
    }

    public virtual void DoDamage(CharacterStats _targetStats)
    {

        int totalDamage = damage.GetValue() + strength.GetValue();
        _targetStats.TakeDamage(totalDamage);
    }

    public virtual void TakeDamage(int _damage)
    {
        currentHp -= _damage;

        Debug.Log(_damage);

        if (currentHp <= 0)
            Die();
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
