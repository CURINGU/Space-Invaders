using UnityEngine;
using System;

[Serializable]
public class playerVariables
{
    [field: SerializeField]
    public Sprite cockPitPrefab { get; private set; }
    [field: SerializeField]
    public Sprite wingLeftPrefab { get; private set; }
    [field: SerializeField]
    public Sprite wingRightPrefab { get; private set; }
    [field: SerializeField]
    public Sprite gunLeftPrefab { get; private set; }
    [field: SerializeField]
    public Sprite gunRightPrefab { get; private set; }
    [field: SerializeField]
    public Sprite trailPrefab { get; private set; }
    [field: SerializeField]

    public float speed { get; private set; }
    [field: SerializeField]
    public float reloadTime { get; private set; }
    [field: SerializeField]
    public int lives { get; private set; }
}

[CreateAssetMenu(fileName = "New Ship Data", menuName = "Ship Data")]
public class PlayerData : ScriptableObject
{
    public int selectedParts;
    public playerVariables[] playerVariables;
}
