using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using static Azure.Core.HttpHeader;

namespace la_mia_pizzeria_static.Models
{
    enum IngredientsOptions {Pomodoro, Mozzarella, Funghi, Salsiccia, Wurstel, Olive, Pancetta, Zucchine, Melanzane, ProsciuttoCotto, AcetoBalsamico }

    [Table("pizzas")]
    internal class Pizza : Timestamps
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("img")]
        public string Img { get; set; }
        [Column("price")]
        public double Price { get; set; }

        //relations
        [Column("ingredients")]
        private string ingredients;

        public string /*List<IngredientsOptions>*/ GetIngredients()
        {
            return "gli ingredienti sono: "+ingredients;
           // return ingredients.Split(',').Select(s => (IngredientsOptions)Enum.Parse(typeof(IngredientsOptions), s)).ToList();
        }
        //internal void SetIngredients(params IngredientsOptions[] _ingredients)
        //{
        //    var validIngredients = Enum.GetValues(typeof(IngredientsOptions)).Cast<IngredientsOptions>();
        //    //se tutti i valori dell'array Value passato al setter sono contenuti nell'elenco validato sopra di ingredientsOptions allora aggiorno a database la stringa degli enum
        //    if (_ingredients.All(i => validIngredients.Contains(i)))
        //    {
        //        ingredients = string.Join(",", _ingredients);
        //    }
        //    else
        //    {
        //        throw new ArgumentException("gli ingredienti inseriti non sono validi!");
        //    }
        //}
    [NotMapped]
    public List<IngredientsOptions> Ingredients
    {
        get
        {
                //sta roba mi da errore Sytem.NullReferenceExeption
            //splitto i valori e con Select li converto in una Lista di IngredientsOptions
            return ingredients.Split(',').Select(s => (IngredientsOptions)Enum.Parse(typeof(IngredientsOptions), s)).ToList();
        }
        set
        {   //prende la list di tutti i valori degli ingredientsOptions validi e li casta ( IEnumerable<IngredientsOptions> )
            var validIngredients = Enum.GetValues(typeof(IngredientsOptions)).Cast<IngredientsOptions>();
            //se tutti i valori dell'array Value passato al setter sono contenuti nell'elenco validato sopra di ingredientsOptions allora aggiorno a database la stringa degli enum
            if (value.All(i => validIngredients.Contains(i)))
            {
                ingredients = string.Join(",", value);
            }
            else
            {
                throw new ArgumentException("gli ingredienti inseriti non sono validi!");
            }
        }
    }

    //+timestamps

    //LO VUOLE VUOTO IL DATABASE!!! AAAAAAAAARG
    public Pizza() { }
        public Pizza(string? name, string _description, string _img, double _price, params IngredientsOptions[] _ingredients)
        {
            Name = name;
            Description = _description;
            Img = "~/wwwroot/img/" + _img;
            Price = _price;
            //SetIngredients(_ingredients);
            Ingredients = _ingredients.ToList();//i controlli li faccio nel setter
        }

        //Pizza p1 = new Pizza("p1", "buona buona", "baby.jpg", 5.99, IngredientsOptions.Mozzarella, IngredientsOptions.Olive);
    }
}
