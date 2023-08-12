using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CreatureStats
{
    [SerializeField] private float armor;
    [SerializeField] private float armorTakeDamage;//Сколько урона поглощает защита за 1 ед.

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI textHealth;
    [SerializeField] private TextMeshProUGUI textArmor;
    [Space, SerializeField] private Slider sliderHealth;
    [SerializeField] private Slider sliderArmor;
    [SerializeField] private GameOverLogic gameOverLogic;

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

            if (sliderHealth != null)
            {
                sliderHealth.value = health / 100;
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

            if (sliderArmor != null)
            {
                sliderArmor.value = armor / 100;
            }
        }
    }

    #endregion

    private void Start()
    {
        Health = health;
        Armor = armor;
    }

    public override void TakeDamage(float _ammount)
    {
        float damageAmmount = _ammount;
        
        if (Armor > 0)
        {
            int armorNeed = Mathf.RoundToInt(Mathf.Clamp((_ammount / armorTakeDamage) - 1, 1, int.MaxValue));

            float armorResist = armorNeed * armorTakeDamage;
            damageAmmount = Mathf.Clamp(damageAmmount - armorResist, 0, float.MaxValue);
            Armor -= armorNeed;

            //Debug.Log($"Start damage : {_ammount}\n" +
            //    $"Armor need : {armorNeed}\n" +
            //    $"Armor resist : {armorResist}\n" +
            //    $"Damage ammount : {damageAmmount}");
        }

        if (Health > 0)
        {
            Health -= damageAmmount;
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

    protected override void Death()
    {
        gameOverLogic.GameOver();
        Destroy(gameObject);
    }
}
