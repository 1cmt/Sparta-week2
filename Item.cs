using System;

namespace Dungeon
{
    public class Item 
    {
        public int Id { get; set;}  
        public string Name { get; set; }
        public int AttackPower { get; set; }
        public int DefensePower { get; set; }
        public string Information { get; set; }
        public int Price { get; set; }
        public int SellPrice { get; set; }
        public bool IsWear { get; set; }

        public Item()
        {
            Id = 0;
            Name = "";
            AttackPower = 0;
            DefensePower = 0;
            Information = "";
            Price = 0;
            SellPrice = 0;
            IsWear = false;
        }   

        public Item(int id, string name, int attackPower, int defensePower, string information, int price)
        {
            Id = id;
            Name = name;
            AttackPower = attackPower;
            DefensePower = defensePower;
            Information = information;
            Price = price;
            SellPrice = (int)(price * 0.85f);
            IsWear = false;
        }

        public void PrintInfo()
        {
            if(AttackPower != 0) Console.Write($"{Name, -9} | 공격력 +{AttackPower} | {Information} ");
            else Console.Write($"{Name, -9} | 방어력 +{DefensePower} | {Information} ");
        }
    }
    
}