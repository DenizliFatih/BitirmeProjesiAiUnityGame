using UnityEngine;

namespace CagataySc
{
    [CreateAssetMenu(fileName = "EntityData", menuName = "Datas/EntityData", order = 0)]
    public class EntityData : ScriptableObject
    {
        [field : SerializeField] public string EntityName { get; private set; }
        [field : SerializeField] public int MaxHealth{ get; private set; }
        [field : SerializeField] public int Defense{ get; private set; }
    }
}
