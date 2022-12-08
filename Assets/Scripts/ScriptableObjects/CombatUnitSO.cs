using UnityEngine;

[CreateAssetMenu(fileName = "NewUnit", menuName = "Scriptable Objects/Combat/Unit")]
public class CombatUnitSO : ScriptableObject
{
    public string unitName;

    public float baseHP;
    public float maxHP;
    public float currentHP;

    public GameObject unitPrefab;

    public InventorySO inventory;

    public void AttackUnit(CombatUnitSO other, ItemWeaponSO weapon)
    {
        other.TakeDamage(weapon.attackPower);
    }

    public void TakeDamage(float damage)
    {
        this.currentHP -= damage;

        if(this.currentHP < 0)
        {
            this.currentHP = 0f;
        }
    }

    public void Heal(float heal)
    {
        this.currentHP += heal;

        if(this.currentHP > this.maxHP)
        {
            this.currentHP = this.maxHP;
        }
    }

    public void ResetHP()
    {
        this.currentHP = this.maxHP;
    }

    private void OnDisable()
    {
        this.maxHP = this.baseHP;
    }
}
