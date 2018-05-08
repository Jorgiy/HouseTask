using System.Collections.Generic;

namespace HouseTask
{
    public class Room : Stationare
    {
        private readonly List<Room> _neighbourRooms;

        private readonly List<Stationare> _roomObjects = new List<Stationare>();

        private readonly List<Window> _windows;
        
        public Room(string name, string documentation, List<Room> neighbourRooms, List<Window> windows) 
            : base(name, documentation)
        {
            _neighbourRooms = neighbourRooms;
            _windows = windows;
        }

        public void AddObjectToRoom(Stationare @object)
        {
            _roomObjects.Add(@object);
        }

        public void RemoveObjectFromRoom(Stationare @object)
        {
            _roomObjects.Remove(@object);
        }

        public List<Stationare> GetRoomObjects()
        {
            return _roomObjects;
        }
        
        public List<Room> GetNeighbourRooms()
        {
            return _neighbourRooms;
        }

        public List<Window> GetWindows()
        {
            return _windows;
        }
    }
}