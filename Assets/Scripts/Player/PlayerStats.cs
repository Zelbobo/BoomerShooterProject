using TMPro;
using UnityEngine;

public class PlayerStats : CreatureStats
{
    [SerializeField] private float armor;
    [SerializeField] private float armorTakeDamage;//Сколько урона поглощает защита за 1 ед.

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI textHealth;
    [SerializeField] private TextMeshProUGUI textArmor;

    #region [PublicVars]

    public float Health
    {
        get => health;
        private set
        {
            health = Mathf.Clamp(value, 0, 100);

            if (textHealth != null)
            {
                textHealth.text = health.ToString();
            }
        }
    }

    public float Armor
    {
        get => armor;
        private set
        {
            armor = Mathf.Clamp(value, 0, 100);

            if (textArmor != null)
            {
                textArmor.text = armor.ToString();
            }
        }
    }

    #endregion

    public override void TakeDamage(float _ammount)
    {
        float damageAmmount = _ammount;
        
        if (Armor > 0)
        {
            int armorNeed = Mathf.RoundToInt(Mathf.Clamp((_ammount / armorTakeDamage) - 1, 1, int.MaxValue));

            float armorResist = armorNeed * armorTakeDamage;
            damageAmmount = Mathf.Clamp(damageAmmount - armorResist, 0, float.MaxValue);
            Armor -= armorNeed;

            Debug.Log($"Start damage : {_ammount}\n" +
                $"Armor need : {armorNeed}\n" +
                $"Armor resist : {armorResist}\n" +
                $"Damage ammount : {damageAmmount}");
        }

        Health -= damageAmmount;

        if (Health > 0)
        {
            return;
        }

        Death();
    }

    public bool AddHealth(float _ammount)
    {
        if (Health >= 100)
        {
            return false;
        }

        Health += _ammount;

        return true;
    }

    public bool AddArmor(float _ammount)
    {
        if (Armor >= 100)
        {
            return false;
        }

        Armor += _ammount;

        return true;
    }
}
