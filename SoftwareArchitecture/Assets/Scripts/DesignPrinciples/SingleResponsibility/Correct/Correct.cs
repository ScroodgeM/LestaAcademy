
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WGADemo.DesignPrinciples.SingleResponsibility.Correct
{
    public class Item
    {
        private string name;
        public string Name => name;
    }

    public class UserInterface
    {
        public void PlayPopup(string text, Color color)
        {
            GameObject prefab = Resources.Load<GameObject>("UIPopups/Popup");
            GameObject instance = GameObject.Instantiate(prefab);
            instance.GetComponent<Text>().text = text;
            instance.GetComponent<Text>().color = color;
            instance.GetComponent<Animator>().Play("move_up_and_fade_out");
        }
    }

    public class SaveGameController
    {
        public void IncreaseItemCounter(Item item)
        {
            int oldItemCount = PlayerPrefs.GetInt($"item_{item.Name}_count");
            PlayerPrefs.SetInt($"item_{item.Name}_count", oldItemCount + 1);
        }
    }

    public class PlayerInventory
    {
        private List<Item> items;
        private int maxItemsCount;
        private Item headItem;

        public void AddItem(Item item)
        {
            items.Add(item);

            if (headItem == null && item.Name == "Helm")
            {
                headItem = item;
            }
        }

        public bool IsOveloaded => items.Count > maxItemsCount;

        public bool HasHelm => headItem != null;
    }

    public class PlayerCharacter
    {
        private PlayerInventory inventory;
        private bool canMove;

        public event Action OnItemPickedUp = () => { };
        public event Action OnInventoryOverloaed = () => { };

        public PlayerInventory Inventory => inventory;

        public void ProcessPickedItem(Item item)
        {
            inventory.AddItem(item);

            canMove = inventory.IsOveloaded == false;

            OnItemPickedUp();

            if (inventory.IsOveloaded == true)
            {
                OnInventoryOverloaed();
            }
        }
    }

    public class AvatarView
    {
        private PlayerCharacter playerCharacter;
        private Transform avatarTransform;
        private Animator animator;

        public void Init(PlayerCharacter playerCharacter)
        {
            this.playerCharacter = playerCharacter;
            playerCharacter.OnItemPickedUp += OnItemPickedUp;
        }

        private void OnItemPickedUp()
        {
            PlayPickItemAnimation();
            SetHelm(playerCharacter.Inventory.HasHelm);
        }

        private void PlayPickItemAnimation()
        {
            animator.Play("pick_item_animation");
        }

        private void SetHelm(bool hasHelm)
        {
            if (hasHelm == true)
            {
                GameObject helmPrefab = Resources.Load<GameObject>("ItemModels/Helm");
                GameObject helmInstance = GameObject.Instantiate(helmPrefab, avatarTransform);
            }
        }
    }

    public class Scenario
    {
        private PlayerCharacter playerCharacter;
        private UserInterface userInterface;
        private SaveGameController saveGameController;

        public Scenario()
        {
            playerCharacter.OnInventoryOverloaed += () =>
            {
                userInterface.PlayPopup("Too many items, can't move", Color.red);
            };
        }

        public void ProcessPickItemCommand(Item item)
        {
            userInterface.PlayPopup(item.Name, Color.white);

            playerCharacter.ProcessPickedItem(item);

            saveGameController.IncreaseItemCounter(item);
        }
    }
}
