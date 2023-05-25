using UnityEngine;

// The EnemyData class is saving ONLY the data needed for our enemies. There is no game logic
// in here and, since it doesn't derive from MonoBehaviour, can't be referenced on a GameObject

// This is needed so that new objects can be created in the 'Create' menu in the project view
[CreateAssetMenu(menuName = "Data/Enemy")]
public class EnemyData : ScriptableObject
{
    // This is public on purpose. If it was private with [SerializeField], we wouldn't be able to get the 
    // values from other scripts. A better way would be to use [SerializeField] and have a public get-property
    // to return the value, like this:    public string EnemyName { get { return enemyName; } }
    public string enemyName;
    public Sprite image;
    public string description;
}
