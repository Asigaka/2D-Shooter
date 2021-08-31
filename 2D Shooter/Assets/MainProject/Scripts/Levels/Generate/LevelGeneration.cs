using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    [Header("Генератор")]
    [SerializeField] private int numberOfFloors = 4;
    [SerializeField] private int numberOfRooms = 5;

    [SerializeField] private List<Room> spawnedRooms = new List<Room>();
    [SerializeField] private Room[,] roomsArray;

    [Header("Комнаты")]
    [SerializeField] private Room firstRoom;
    [SerializeField] private Room[] roomPrefabs;
    [SerializeField] private Room lastRoom;

    private void Start()
    {
        roomsArray = new Room[numberOfFloors, numberOfRooms];
        roomsArray[0, 0] = firstRoom;
        spawnedRooms.Add(firstRoom);
        Generate();
    }

    public void Generate()
    {
        for (int y = 0; y < numberOfFloors; y++)
        {
            for (int x = 0; x < numberOfRooms; x++)
            {
                Room newRoom = Instantiate(roomPrefabs[Random.Range(0, roomPrefabs.Length)]);
                newRoom.transform.position = spawnedRooms[spawnedRooms.Count - 1].End.position - newRoom.Begin.localPosition;
                spawnedRooms.Add(newRoom);
            }
        }

        lastRoom = spawnedRooms[spawnedRooms.Count - 1];
    }
}
