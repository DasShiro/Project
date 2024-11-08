using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int maxHp; // Maximale Gesundheit
    public int currentHp; // Aktuelle Gesundheit
    public float healthRegenAmount; // Gesundheitsregenerationsrate
    public float healthRegenDelay; // Verzögerung für die Gesundheitsregeneration
    public float lifeSteal; // Lebensraub
    public int physicalDamage; // Physischer Schaden
    public float magicalDamage; // Magischer Schaden
    public int rangedDamage; // Fernkampfschaden
    public float attackSpeed; // Angriffsgeschwindigkeit
    public float criticalChance; // Kritische Trefferchance
    public float criticalDamage; // Kritischer Schaden
    public int maxShield; // Maximales Schild
    public int currentShield; // Aktuelles Schild
    public float shieldRechargeRate; // Schildaufladegeschwindigkeit
    public float shieldRechargeDelay; // Verzögerung für die Schildaufladung
    public float moveSpeed; // Bewegungsgeschwindigkeit

    public PlayerData(int maxHp, float healthRegenAmount, float healthRegenDelay, float lifeSteal, int physicalDamage, float magicalDamage, int rangedDamage, float attackSpeed, float criticalChance, float criticalDamage, int maxShield, float shieldRechargeRate, float shieldRechargeDelay, float moveSpeed)
    {
        this.maxHp = maxHp;
        this.currentHp = maxHp; // Setze die aktuelle Gesundheit auf das Maximum
        this.healthRegenAmount = healthRegenAmount;
        this.healthRegenDelay = healthRegenDelay;
        this.lifeSteal = lifeSteal;
        this.physicalDamage = physicalDamage;
        this.magicalDamage = magicalDamage;
        this.rangedDamage = rangedDamage; // Setze den Fernkampfschaden
        this.attackSpeed = attackSpeed;
        this.criticalChance = criticalChance;
        this.criticalDamage = criticalDamage;
        this.maxShield = maxShield;
        this.currentShield = maxShield; // Setze das aktuelle Schild auf das Maximum
        this.shieldRechargeRate = shieldRechargeRate;
        this.shieldRechargeDelay = shieldRechargeDelay;
        this.moveSpeed = moveSpeed; // Setze die Bewegungsgeschwindigkeit
    }

    // Methode zur Regeneration des Schildes

    public void RegenerateShield(float deltaTime)
    {
        currentShield += (int)(shieldRechargeRate * deltaTime);
        if (currentShield > maxShield)
        {
            currentShield = maxShield; // Verhindert, dass das Schild über das Maximum hinausgeht
        }
    }
    // Methode zur Regeneration der Gesundheit
    public void RegenerateHealth(float deltaTime)
    {
        currentHp += (int)(healthRegenAmount * deltaTime);
        if (currentHp > maxHp)
        {
            currentHp = maxHp; // Verhindert, dass die Gesundheit über das Maximum hinausgeht
        }
    }

    // Methode zum Anwenden von Lebensraub
    public void ApplyLifeSteal(float damageDealt)
    {
        currentHp += (int)(damageDealt * lifeSteal);
        if (currentHp > maxHp)
        {
            currentHp = maxHp; // Verhindert, dass die Gesundheit über das Maximum hinausgeht
        }
    }

    // Methode zur Berechnung des Schadens
    public float CalculateDamage(bool isCritical)
    {
        float finalDamage = physicalDamage; // Verwende den physischen Schaden

        if (isCritical)
        {
            finalDamage *= criticalDamage; // Wendet den kritischen Schaden an
        }

        return finalDamage;
    }

    // Methode zur Berechnung des Fernkampfschadens
    public float CalculateRangedDamage(bool isCritical)
    {
        float finalDamage = rangedDamage; // Verwende den Fernkampfschaden

        if (isCritical)
        {
            finalDamage *= criticalDamage; // Wendet den kritischen Schaden an
        }

        return finalDamage;
    }

    // Methode zur Überprüfung, ob ein kritischer Treffer eintritt
    public bool IsCriticalHit()
    {
        return Random.Range(0f, 1f) < criticalChance; // Überprüft, ob ein kritischer Treffer eintritt
    }

    // Methode zur Berechnung der Erfahrung
    public float CalculateExperience(float baseExperience, float experienceMultiplier)
    {
        return baseExperience * experienceMultiplier; // Berechnet die Erfahrung basierend auf dem Multiplikator
    }

}