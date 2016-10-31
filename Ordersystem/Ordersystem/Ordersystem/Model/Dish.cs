﻿namespace Ordersystem.Model
{
    public class Dish
    {
        public Dish(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }

        // TODO: Integrer med databaser.
        // TODO: Find ud af hvordan vi inkluderer billedet.
        //public Image image { get; set; }
    }
}