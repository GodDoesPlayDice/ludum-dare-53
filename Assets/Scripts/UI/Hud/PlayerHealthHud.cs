﻿using System.Collections.Generic;
using Actors;
using UnityEngine;

namespace UI.Hud
{
    public class PlayerHealthHud : MonoBehaviour
    {
        [SerializeField] private int healthInHeart = 2;
        [SerializeField] private GameObject heartPrefab;

        private int curMaxHealth;
        private List<PlayerHeart> _hearts = new();

        private void Awake()
        {
            var playerHealth = GetComponentInParent<ActorHealth>();
            SetHealth(playerHealth.MaxHealth, playerHealth.Health);
            playerHealth.OnHealthChanged +=
                curHealth => SetHealth(playerHealth.MaxHealth, curHealth);
        }

        private void SetHealth(int maxHealth, int health)
        {
            if (curMaxHealth != maxHealth)
            {
                RecreateHearts(maxHealth / healthInHeart);
            }
            
            FillHearts(health);
        }

        private void RecreateHearts(int hearts)
        {
            _hearts.Clear();
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            for (var i = 0; i < hearts; i++)
            {
                var obj = Instantiate(heartPrefab, transform);
                _hearts.Add(obj.GetComponent<PlayerHeart>());
            }
        }

        private void FillHearts(int health)
        {
            var fullHearts = Mathf.FloorToInt(health / healthInHeart);
            var healthLeft = health % healthInHeart;

            for (var i = 0; i < _hearts.Count; i++)
            {
                var heart = _hearts[i];
                if (i < fullHearts)
                    heart.SetFill(1);
                else if (i == fullHearts)
                    heart.SetFill(healthLeft / (float) healthInHeart);
                else
                    heart.SetFill(0);
            }
        }
    }
}