using UnityEngine;

namespace IngredientDetails
{
    public enum Emotion
    {
        // TODO: Replace with actual emotions
        Happy,
        Sad,
        Angry,
        Fearful,
        Disgusted,
        Surprised
    }

    [CreateAssetMenu(fileName = "MaterialData", menuName = "ScriptableObjects/Material", order = 1)]
    public class Material : ScriptableObject
    {
        public string name;
        public uint cost;
        public Emotion emotion;
    }
}