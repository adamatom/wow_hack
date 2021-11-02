using System;
using System.Collections;
using System.Text;

namespace Woah
{
    /// <summary>
    /// All Possible actions - Obviously not all are implemented or relevant
    /// </summary>
    public enum BotAction
    {
        /// <summary>Initializes the bot</summary>
        Initialize,
        /// <summary>Finds the closest waypoint and sets the waypoint iterator</summary>
        SetClosestWaypoint,
        /// <summary>Sets waypoint iterator to the next waypoint to fight at</summary>
        SetNextWaypoint,
        /// <summary>Moves to the targetted waypoint</summary>
        MoveToWaypoint,
        /// <summary>Find target</summary>
        FindTarget,
        /// <summary>Checks agro</summary>
        CheckAgro,
        /// <summary>Gets in range</summary>
        GetInRange,
        /// <summary>Starts the ranged fight sequence</summary>
        RangedFight,
        /// <summary>Starts the melee fight sequence</summary>
        MeleeFight,
        /// <summary>Decide if we have to flee</summary>
        CheckFlee,
        /// <summary>Flee</summary>
        Flee,
        /// <summary>Loot all nearby targets</summary>
        Loot,
        /// <summary>Skin all nearby targets</summary>
        Skinning,
        /// <summary>Mine</summary>
        Mine,
        /// <summary>Gather herbs</summary>
        GatherHerbs,
        /// <summary>Open chests</summary>
        OpenChests,
        /// <summary>Rest</summary>
        Rest,
        /// <summary>Check buffs</summary>
        CheckBuffs,
        /// <summary>Died, do corpse run and find body</summary>
        DeadCorpseRun,
        /// <summary>Find body</summary>
        DeadFindBody,
        /// <summary>Resurrect</summary>
        Resurrect,
        /// <summary>Paused or Stopped after death</summary>
        Pause
    }
    public class WoahQueue
    {
        private ArrayList _actions;

        public WoahQueue()
        {
            _actions = new ArrayList();
        }

        public void Enqueue(BotAction action)
        {
            _actions.Add(action);
        }

        public BotAction Dequeue()
        {
            if (_actions.Count == 0)
                throw new Exception("Can't dequeue action because the queue is empty.");

            BotAction action = (BotAction)_actions[0];
            _actions.RemoveAt(0);

            return action;
        }

        public BotAction Peek(int index)
        {
            if (index > _actions.Count - 1)
                throw new Exception("Can't Peek because the index is greater than the number of actions in the queue.");

            BotAction action = (BotAction)_actions[index];

            return action;
        }

        public void Insert(int index, BotAction action)
        {
            _actions.Insert(index, action);
        }

        public void Clear()
        {
            _actions.Clear();
        }

        public int Count
        {
            get
            {
                return _actions.Count;
            }
        }
    }
}
