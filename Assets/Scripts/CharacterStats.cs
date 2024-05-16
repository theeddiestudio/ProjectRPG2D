using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int damage;
    public int maxHp;

    private int currentHp;

    private void Start()
    {
        currentHp = maxHp;
    }

    public void TakeDamage(int _damage)
    {
        currentHp -= _damage;
    }
}
