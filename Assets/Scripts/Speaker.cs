using UnityEngine;

[CreateAssetMenu(fileName = "NewSpeaker", menuName = "Data/New Speaker")]
[System.Serializable]
public class Speaker : ScriptableObject
{
    public string speakerName;
    public Color textColor;
    public Sprite characterImage;
    public bool isMainCharacter;
    public bool isSmallCharacter;
}
