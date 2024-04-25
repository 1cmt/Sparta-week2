using System;

namespace Dungeon
{
    public class Status
    {
        public int Level { get; set; }
        public string Name { get; set; }
        public string Job { get; set; }
        public int AttackPower { get; set; }
        public Item? MyAttackItem { get; set; }
        public int DefensePower { get; set; }
        public Item? MyDefenseItem { get; set; }
        public int Hp { get; set; }
        public int Gold { get; set; }

        public Status()
        {
            Level = 1;
            Name = "Chad";
            Job = "전사";
            AttackPower = 10;
            MyAttackItem = null;
            DefensePower = 5;
            MyDefenseItem = null;
            Hp = 100;
            Gold = 1500;
        }

        public void WearItem(Item item)
        {
            if (item is AttackItem)
            {
                if (MyAttackItem == null)
                {
                    AttackPower += item.GetPower();
                    MyAttackItem = item;
                    MyAttackItem.IsWear = true;
                }
                else
                {
                    //공격형 아이템이 있으면 기존 장비는 해제해야함
                    UnWearItem(item);
                    AttackPower += item.GetPower();
                    MyAttackItem = item;
                    MyAttackItem.IsWear = true;
                }
            }
            else if (item is DefenseItem)
            {
                if (MyDefenseItem == null)
                {
                    DefensePower += item.GetPower();
                    MyDefenseItem = item;
                    MyDefenseItem.IsWear = true;
                }
                else
                {
                    //공격형 아이템이 있으면 기존 장비는 해제해야함
                    UnWearItem(item);
                    DefensePower += item.GetPower();
                    MyDefenseItem = item;
                    MyDefenseItem.IsWear = true;
                }
            }
        }

        public void UnWearItem(Item item)
        {
            if (item is AttackItem)
            {
                if (MyAttackItem != null)
                {
                    MyAttackItem.IsWear = false;
                    AttackPower -= MyAttackItem.GetPower();
                    MyAttackItem = null;
                }
            }
            else if (item is DefenseItem)
            {
                if (MyDefenseItem != null)
                {
                    MyDefenseItem.IsWear = false;
                    DefensePower -= MyDefenseItem.GetPower();
                    MyDefenseItem = null;
                }
            }
        }

        public void ShowStatus()
        {
            int choice = 0;
            PrintInfo();

            while (true)
            {
                Console.Write(">> ");

                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 0:
                            return;
                        default:
                            Console.WriteLine("잘못된 입력입니다");
                            continue;
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다");
                    continue;
                }
            }
        }

        public void PrintInfo()
        {
            Console.WriteLine("");
            Console.WriteLine("상태보기");
            Console.WriteLine("케릭터의 정보가 표시됩니다.");
            Console.WriteLine("");

            if (Level < 10) Console.WriteLine($"Lv. 0{Level}");
            else Console.WriteLine($"Lv. {Level}");

            Console.WriteLine($"{Name} ( {Job} )");

            if (MyAttackItem == null) Console.WriteLine($"공격력 : {AttackPower}");
            else Console.WriteLine($"공격력 : {AttackPower} (+{MyAttackItem.GetPower()})");

            if (MyDefenseItem == null) Console.WriteLine($"방어력 : {DefensePower}");
            else Console.WriteLine($"방어력 : {DefensePower} (+{MyDefenseItem.GetPower()})");

            Console.WriteLine($"체 력 : {Hp}");
            Console.WriteLine($"Gold : {Gold} G");
            Console.WriteLine("");

            Console.WriteLine("0. 나가기");
            Console.WriteLine("원하시는 행동을 입력해주세요");
        }
    }
}