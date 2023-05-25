using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyChanger : MonoBehaviour
{
    // This is the data from our Scriptable Objects. The EnemyChanger does not need to know
    // anything about the data itself - it is only implementing the game logic and therefore
    // data and logic are completely separated
    [SerializeField] EnemyData[] enemyData;

    [SerializeField] Image img;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI descText;
    [SerializeField] TextMeshProUGUI indexText;

    int enemyIndex = 0;

    // This reads a value with key 'EnemyIndex' from disk before anything else happens
    void Awake() => enemyIndex = PlayerPrefs.GetInt("EnemyIndex", 0);
    void Start() => LoadNextEnemy();

    public void LoadNextEnemy()
    {
        // This writes a value with key 'EnemyIndex' on disk (to the registry on Windows PCs). 
        // The index is saved, so that we can save the current enemy we were at when the game was closed.
        // On start the code in Awake() will load that index and therefore restore the previous game state
        PlayerPrefs.SetInt("EnemyIndex", enemyIndex);

        // We load data from the Scriptable Objects that are not in the Scene
        img.sprite = enemyData[enemyIndex].image;
        nameText.text = enemyData[enemyIndex].enemyName;
        descText.text = enemyData[enemyIndex].description;

        // This shows the current index and the total amount of enemies. Then the index is
        // incremented and wraps around when it reaches the last enemy.
        indexText.text = $"{enemyIndex + 1}/{enemyData.Length}"; 
        enemyIndex = (enemyIndex + 1) % enemyData.Length;  
    }
}