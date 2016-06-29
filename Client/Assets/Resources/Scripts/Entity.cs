using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

struct EntityEventData
{
    enum Type
    {
        DESTROYED
    }
}

public class Entity
{
    public int _master
    {
        get; set;
    }

    public Entity()
    {

    }

    void HandleBattleEntityEvent(ref Entity entity, ref EntityEventData event_data)
    {
        if(entity == null)
        {
            return;
        }

        Entity master = EntityManager.Instance.GetEntity(1);
//         if (entity == master)
//         {
//             unsigned int count = (unsigned int)event_handler_list_.size();
//             for (unsigned int i = 0; i < count; i++)
//             {
//                 A2S_ASSERT(i < event_handler_list_.size());
//                 BattleEntityEventHandler* event_handler = event_handler_list_[i];
//                 if (event_handler)
//                 {
//                     event_handler->HandleBattleEntityEvent(master, event_data);
//                 }
//             }
//         }
// 
//         switch (event_data.type_)
//         {
//             case BattleEntityEventData::RESET_TRIGGER:
//                 {
//                     int trigger_index;
//                     event_data.data_stream_->Reset();
//                     event_data.data_stream_->Read(trigger_index);
// 
//                     if (trigger_index < (int)trigger_comps_.size())
//                     {
//                         trigger_comps_[trigger_index]->Reset();
//                     }
//                     else
//                     {
//                         A2S_ASSERT(false);
//                     }
//                 }
//                 break;
//             case BattleEntityEventData::DAMAGED:
//                 {
//                     if (visual_slave_comp_ && visual_slave_comp_->GetListenDamageEvent())
//                     {
//                         int visual_slave_entity_id = visual_slave_comp_->GetVisualSlaveEntityId();
//                         BattleEntity* visual_slave_entity = BattlePlayground::Get()->GetEntity(visual_slave_entity_id);
//                         if (visual_slave_entity)
//                         {
//                             visual_slave_entity->BroadCastEvent(event_data);
//                         }
//                     }
//                 }
//                 break;
//             default:
//                 break;
//         }
    }
}
