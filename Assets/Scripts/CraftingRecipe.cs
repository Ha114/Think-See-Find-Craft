using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "CraftingRecipe/baseRecipe")]
public class CraftingRecipe : Item
{
    public int _id;
    public Item result;
    public Ingredient[] ingredients;

    private bool CanCraft()
    {
        foreach (Ingredient ingredient in ingredients)
        {
            bool containsCurrentIngredient = Inventory.instance.ContainsItem(ingredient.item.name, ingredient.amount);

            if (!containsCurrentIngredient)
            {
                return false;
            }
        }

        return true;
    }


    private void RemoveIngredientsFromIventory()
    {
        foreach (Ingredient ingredient in ingredients)
        {
            Inventory.instance.RemoveItems(ingredient.item.name, ingredient.amount);
        }
    }

    public override void Use()
    {
        if (CanCraft())
        {
            //remove items
            RemoveIngredientsFromIventory();

            //add a item to the inventory
            Inventory.instance.AddStackebleItem(result);
            OpenGate(result);
            Debug.Log("You just crafted a: " + result.name);
            GameManager.instance.MassageOfCraftedItem(result.name);
        }
        else
        {
            Debug.Log("You dont have enaugh ingredients to craft: " + result.name);
            GameManager.instance.SetTextM("You dont have enaugh ingredients to craft: " + result.name);
        }
    }

    void OpenGate(Item result) {
        if (result.name == "Key") {
            OpenTheDoor._instance.isGateOpen = true;
        }
    
    }

    public override string GetItemDescription()
    {
        string itemIngredients = "";

        foreach (Ingredient ingredient in ingredients)
        {
            itemIngredients += " - " + ingredient.amount + " " + ingredient.item.name + "\n";
        }
        return itemIngredients;
    }


    [System.Serializable]
    public class Ingredient
    {
        public Item item;
        public int amount;
    }
}
