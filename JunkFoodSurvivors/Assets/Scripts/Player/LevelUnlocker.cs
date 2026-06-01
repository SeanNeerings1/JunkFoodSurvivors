using UnityEngine;
using System;

public class LevelUnlocker : MonoBehaviour
{
    [System.Serializable]
    public class LevelReward
    {
        public int unlockLevel;
        public string scriptName;
    }

    [SerializeField] private LevelReward[] upgrades;
    private PlayerXp _playerXp;
    private int _currentUpgradeIndex = 0;

    void Start()
    {
        _playerXp = GetComponent<PlayerXp>();
    }

    void Update()
    {
        if (_playerXp == null || upgrades == null || _currentUpgradeIndex >= upgrades.Length) return;

        LevelReward nextReward = upgrades[_currentUpgradeIndex];

        if (_playerXp.CurrentLevel >= nextReward.unlockLevel)
        {
            PlayerShoot oldShoot = GetComponent<PlayerShoot>();

            if (oldShoot != null)
            {
                Type scriptType = null;
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    scriptType = assembly.GetType(nextReward.scriptName);
                    if (scriptType != null) break;
                }

                if (scriptType != null)
                {
                    GameObject prefab = oldShoot.bulletPrefab;
                    Transform point = oldShoot.firePoint;
                    float speed = oldShoot.bulletSpeed;
                    float rate = oldShoot.fireRate;

                    DestroyImmediate(oldShoot);

                    Component newComponent = gameObject.AddComponent(scriptType);
                    PlayerShoot newShoot = newComponent as PlayerShoot;

                    if (newShoot != null)
                    {
                        newShoot.bulletPrefab = prefab;
                        newShoot.firePoint = point;
                        newShoot.bulletSpeed = speed;
                        newShoot.fireRate = rate;
                    }

                    Debug.Log(nextReward.scriptName + " succesvol toegevoegd via LevelUnlocker!");
                    _currentUpgradeIndex++;
                }
                else
                {
                    Debug.LogError("KAN SCRIPT NIET VINDEN: '" + nextReward.scriptName + "'. Check de spelling in de Inspector!");
                    _currentUpgradeIndex++;
                }
            }
        }
    }
}