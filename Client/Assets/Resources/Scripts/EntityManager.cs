using UnityEngine;
using System.Collections.Generic;

public class EntityManager : MonoBehaviour
{
    Dictionary<int, Entity> _entity_list = new Dictionary<int, Entity>();

    int _last_entity_id = -1;

    private static EntityManager _instance;
    public static EntityManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<EntityManager>();
                if (_instance == null)
                {
                    GameObject container = new GameObject();
                    container.name = "EntityManager";
                    _instance = container.AddComponent(typeof(EntityManager)) as EntityManager;
                }
            }
            
            return _instance;
        }
    }

    public Entity GetEntity(int entityID)
    {
        Entity entity = null;
        if(_entity_list.ContainsKey(entityID))
        {
            entity = _entity_list[entityID];
        }

        return entity;
    }
    
    public void AddEntity(Entity entity)
    {
        _entity_list.Add(++_last_entity_id, entity);
    }
}