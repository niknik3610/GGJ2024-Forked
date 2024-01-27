using UnityEngine;

namespace IngredientDetails
{
    public enum Emotion
    {
        None,
        Soothing,
        Introspective,
        Energetic,
        Tranquil,
        Happy,
    }

    [CreateAssetMenu(fileName = "MaterialData", menuName = "ScriptableObjects/Material", order = 1)]
    public class Material : ScriptableObject
    {
        public string ingredientName;
        public uint cost;
        public Emotion emotion;
        public Severity severity;
        public float weight;
    }
}