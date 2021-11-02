using System;
using System.Collections.Generic;
using System.Text;

namespace Woah
{
    struct Waypoint
    {
        public Vector3 location;
        public int ID;
        public WaypointAction DoAction;
    }
    enum WaypointAction { Mount, Stop, Jump, Wait2Min, None }
    class WaypointList
    {
        public string Name;
        List<Waypoint> waypoints = new List<Waypoint>();
        public WaypointList(string name)
        {
            Name = name;
        }

        public void Add(Vector3 v)
        {
            Waypoint wp = new Waypoint();
            wp.location = v;

            wp.ID = waypoints.Count;
            wp.DoAction = WaypointAction.None;

            waypoints.Add(wp);
        }

        public void Add(Vector3 v, WaypointAction doact)
        {
            Waypoint wp = new Waypoint();
            wp.location = v;

            wp.ID = waypoints.Count;
            wp.DoAction = doact;

            waypoints.Add(wp);
        }


        

        public Waypoint FindClosestTo(Vector3 location)
        {
            if(waypoints.Count == 0)
            {
                throw new Exception("Tried to grab the closest waypoint from an empty waypoint list");
            }

            Waypoint final = new Waypoint();
            float bestdist = 999999999999999.0f;
            foreach(Waypoint w in waypoints)
            {
                float distance = (float)w.location.Distance(location);
                if(distance < bestdist)
                {
                    bestdist = distance;
                    final = w;
                }
            }
            return final;
        }

        public Waypoint Get(int fromID)
        {
            foreach (Waypoint w in waypoints)
            {
                if (w.ID == fromID)
                {
                    return w;
                }
            }
            // The above code should always return w. If the list is empty we'll get here
            throw new Exception("There is no next waypoint to return. This happens when the waypoint list is empty");

        }

        public Waypoint GetNext(int fromID, out bool wrapped)
        {
            if(fromID +1 >= waypoints.Count)
            {
                wrapped = true;
                fromID = 0;
            }
            else
            {
                wrapped = false;
                fromID += 1;
            }

            foreach (Waypoint w in waypoints)
            {
                if (w.ID == fromID)
                {
                    return w;
                }
            }
            
            // The above code should always return w. If the list is empty we'll get here
            throw new Exception("There is no next waypoint to return. This happens when the waypoint list is empty");


        }
        public int Count
        {
            get { return waypoints.Count; }
        }
        public Waypoint GetPrev(int fromID, out bool wrapped)
        {
            if (fromID - 1 < 0)
            {
                wrapped = true;
                fromID = waypoints.Count-1;
            }
            else
            {
                wrapped = false;
                fromID -= 1;
            }

            foreach (Waypoint w in waypoints)
            {
                if (w.ID == fromID)
                {
                    return w;
                }
            }

            // The above code should always return w. If the list is empty we'll get here
            throw new Exception("There is no next waypoint to return. This happens when the waypoint list is empty");


        }
    }
}
