using System.Collections.Generic;
using UnityEngine;

public class Blackhole_Skill_Controller : MonoBehaviour
{
    [SerializeField] private GameObject hotKeyPrefab;
    [SerializeField] private List<KeyCode> hotkeyList;

    public float maxSize;
    public float growSpeed;
    public float shrinkSpeed;
    public bool canGrow;
    public bool canShrink;

    private bool canCreateHotkeys = true;

    public int amountOfAttacks = 4;
    public float cloneAttackCooldown = .3f;
    private float cloneAttackTimer;
    private bool canAttack;

    private List<Transform> targets = new List<Transform>();
    private List<GameObject> createdHotkeys = new List<GameObject>();

    private void Update()
    {
        cloneAttackTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.E))
        {
            DestroyHotKeys();
            canAttack = true;
            canCreateHotkeys = false;
        }

        if (cloneAttackTimer < 0 && canAttack)
        {
            cloneAttackTimer = cloneAttackCooldown;

            int randomIndex = Random.Range(0, targets.Count);

            float xOffset;

            if (Random.Range(0, 10) > 5)
                xOffset = 2;
            else
                xOffset = -2;

            Debug.Log(targets.Count);

            SkillManager.manager.clone.CreateClone(targets[randomIndex], new Vector3(xOffset, 0));

            amountOfAttacks -= 1;

            if (amountOfAttacks <= 0)
            {
                canShrink = true;
                canAttack = false;
            }
        }

        if (canGrow && !canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);
        }

        if (canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(-1, -1), shrinkSpeed * Time.deltaTime);

            if (transform.localScale.x <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void DestroyHotKeys()
    {
        if (createdHotkeys.Count <= 0)
            return;

        for (int i = 0; i < createdHotkeys.Count; i++)
        {
            Destroy(createdHotkeys[i]);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().FreezeTime(true);
            CreateHotkey(collision);
        }
    }

    private void CreateHotkey(Collider2D collision)
    {
        if (hotkeyList.Count <= 0)
        {
            Debug.LogWarning("Not enough hot keys in a key code list");
            return; 
        }

        if (!canCreateHotkeys)
            return;

        GameObject newHotkey = Instantiate(hotKeyPrefab, collision.transform.position + new Vector3(0, 2), Quaternion.identity);
        createdHotkeys.Add(newHotkey);

        KeyCode choosenKey = hotkeyList[Random.Range(0, hotkeyList.Count)];
        hotkeyList.Remove(choosenKey);

        Blackhole_Hotkey_Controller newHotkeyScript = newHotkey.GetComponent<Blackhole_Hotkey_Controller>();

        newHotkeyScript.SetupHotkey(choosenKey, collision.transform, this);
    }

    public void AddEnemyToList(Transform _enemyTransform) => targets.Add(_enemyTransform);
}
