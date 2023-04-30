﻿using Actors.InputThings;
using Common;
using Map.Runtime;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Map.Model
{
    public class RoomExit : MonoBehaviour
    {
        [SerializeField] private WallDirection direction;
        public WallDirection Direction => direction;

        [SerializeField] private GameObject door;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                var player = other.gameObject.GetComponent<PlayerActorInput>();
                CrawlController.Instance.ExitRoom(player, GetComponentInParent<Room>(), this);
            }
        }
        
        public void CloseDoor()
        {
            door.SetActive(true);
        }
        
        public void OpenDoor()
        {
            door.SetActive(false);
        }
    }
}