using System;

namespace Dungeon
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Information { get; set; }
        public int Price { get; set; }
        public int SellPrice { get; set; }
        public bool IsWear { get; set; }

        public Item()
        {
            Id = 0;
            Name = "";
            Information = "";
            Price = 0;
            SellPrice = 0;
            IsWear = false;
        }

        public Item(int id, string name, string information, int price)
        {
            Id = id;
            Name = name;
            Information = information;
            Price = price;
            SellPrice = (int)(price * 0.85f);
            IsWear = false;
        }

        public virtual void PrintInfo(){}

        public virtual int GetPower(){ return 0;}
    }

    public class AttackItem : Item
    {
        public int AttackPower { get; set; }

        public AttackItem(int id, string name, int attackPower, string information, int price) : base(id, name, information, price)
        {
            AttackPower = attackPower;
        }

        public override void PrintInfo()
        {
            Console.Write($"{Name,-9} | 공격력 +{AttackPower} | {Information} ");
        }

        public override int GetPower()
        {
            return AttackPower;
        }

    }

    public class DefenseItem : Item
    {
        public int DefensePower { get; set; }

        public DefenseItem(int id, string name, int defensePower, string information, int price) : base(id, name, information, price)
        {
            DefensePower = defensePower;
        }

        public override void PrintInfo()
        {
            Console.Write($"{Name,-9} | 방어력 +{DefensePower} | {Information} ");
        }

        public override int GetPower()
        {
            return DefensePower;
        }
    }

}