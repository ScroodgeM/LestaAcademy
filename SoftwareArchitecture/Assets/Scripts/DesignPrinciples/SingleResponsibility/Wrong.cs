
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WGADemo.DesignPrinciples.SingleResponsibility.Wrong
{
    public class Item
    {
        private string name;
        public string Name => Name;
    }

    public class PlayerCharacter
    {
        private Transform playerAvatar;
        private Animator animator;
        private List<Item> items;
        private int maxItemsCount;
        private Item headItem;
        private bool canMove;

        public void ProcessPickItemCommand(Item item)
        {
            animator.Play("pick_item_animation");

            if (headItem == null && item.Name == "Helm")
            {
                headItem = item;
                GameObject helmPrefab = Resources.Load<GameObject>("ItemModels/Helm");
                GameObject helmInstance = GameObject.Instantiate(helmPrefab, playerAvatar);
            }

            GameObject popupPrefab = Resources.Load<GameObject>("UIPopups/PickedItemPopup");
            GameObject popupInstance = GameObject.Instantiate(popupPrefab);
            popupInstance.GetComponent<Text>().text = item.Name;
            popupInstance.GetComponent<Animator>().Play("move_up_and_fade_out");

            items.Add(item);
            if (items.Count >= maxItemsCount)
            {
                canMove = false;
                GameObject cantMovePopupPrefab = Resources.Load<GameObject>("UIPopups/TooManyItemsCantMove");
                GameObject cantMovePopupInstance = GameObject.Instantiate(cantMovePopupPrefab);
                cantMovePopupInstance.GetComponent<Text>().text = "Too many items, can't move";
                cantMovePopupInstance.GetComponent<Text>().color = Color.red;
                cantMovePopupInstance.GetComponent<Animator>().Play("move_up_and_fade_out");
            }

            int oldItemCount = PlayerPrefs.GetInt($"item_{item.Name}_count");
            PlayerPrefs.SetInt($"item_{item.Name}_count", oldItemCount + 1);
        }
    }
}
