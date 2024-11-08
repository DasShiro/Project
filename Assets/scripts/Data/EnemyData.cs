using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    public GameObject enemyPrefab; // Prefab des Gegners
    public int health; // Gesundheit des Gegners
    public float speed; // Bewegungsgeschwindigkeit des Gegners
    public int pointsOnDeath; // Punkte, die der Spieler erhält, wenn der Gegner stirbt
    public GameObject[] dropItems; // Items, die der Gegner fallen lässt
    public float spawnChance; // Wahrscheinlichkeit, dass dieser Gegner spawnt
    public int damage; // Schaden, den dieser Gegner verursacht
    public int expAmount; // Erfahrungspunkte die ein Gegner gibt.
}