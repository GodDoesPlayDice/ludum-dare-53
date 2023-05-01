﻿using System;
using System.Collections.Generic;
using Actors.InputThings;
using Actors.Upgrades;
using Common;
using Scene;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Upgrades
{
    public class UpgradeSystemUI : SingletonScene<UpgradeSystemUI>, Initializable
    {
        [SerializeField] private GameObject upgradePanel;
        [SerializeField] private RectTransform upgradeItemsContainer;
        [SerializeField] private UpgradeItemUI upgradeItemPrefab;
        
        private ActorStatsSo[] _upgrades;

        private DynamicActorStats _playerStats;

        public void Initialize()
        {
            _upgrades = Resources.LoadAll<ActorStatsSo>("Upgrades");
            var player = FindObjectOfType<PlayerActorInput>();
            _playerStats = player.GetComponent<DynamicActorStats>();
        }

        public void ShowUpgradeSelection(Action<ActorStatsSo> callback)
        {
            ClearContainer();
            
            var selectedUpgrades = new List<ActorStatsSo>();
            while (selectedUpgrades.Count < 3)
            {
                var upgrade = _upgrades[Random.Range(0, _upgrades.Length)];
                if (selectedUpgrades.Contains(upgrade))
                    continue;
                
                selectedUpgrades.Add(upgrade);
                Instantiate(upgradeItemPrefab, upgradeItemsContainer)
                    .Initialize(upgrade, upgrade => OnUpgradeSelected(upgrade, callback));
            }
            
            upgradePanel.SetActive(true);
        }
        
        private void ClearContainer()
        {
            foreach (Transform child in upgradeItemsContainer)
            {
                Destroy(child.gameObject);
            }
        }

        private void OnUpgradeSelected(ActorStatsSo upgrade, Action<ActorStatsSo> callback)
        {
            upgradePanel.SetActive(false);
            _playerStats.ModifyCurrentStatsSo(upgrade);
            callback?.Invoke(upgrade);
        }
    }
}