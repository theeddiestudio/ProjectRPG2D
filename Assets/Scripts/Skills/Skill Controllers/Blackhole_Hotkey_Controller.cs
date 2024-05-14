using TMPro;
using UnityEngine;

public class Blackhole_Hotkey_Controller : MonoBehaviour
{
    private KeyCode blackholeHotkey;
    private SpriteRenderer sr;

    private TextMeshProUGUI blackholeHotkeyText;

    private Transform enemiesTransform;
    private Blackhole_Skill_Controller blackhole;

    public void SetupHotkey(KeyCode _blackholeHotkey, Transform _enemy, Blackhole_Skill_Controller _blackhole)
    {
        blackholeHotkeyText = GetComponentInChildren<TextMeshProUGUI>();
        sr = GetComponent<SpriteRenderer>();

        enemiesTransform = _enemy;
        blackhole = _blackhole;

        blackholeHotkey = _blackholeHotkey;
        blackholeHotkeyText.text = blackholeHotkey.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(blackholeHotkey))
        {
            blackhole.AddEnemyToList(enemiesTransform);

            blackholeHotkeyText.color = Color.clear;
            sr.color = Color.clear;
        }
    }
}
