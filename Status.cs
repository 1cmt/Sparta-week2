using System;

namespace Dungeon
{
    public class Status
    {
        public int Level { get; set; }
        public string Name { get; set; }
        public string Job { get; set; }
        public int AttackPower { get; set; }
        public int ItemAttackPower { get; set; }
        public int DefensePower { get; set; }
        public int ItemDefensePower { get; set; }
        public int Hp { get; set; }
        public int Gold { get; set; }
        public Item[] MyItem { get; set; }

        public Status()
        {
            Level = 1;
            Name = "Chad";
            Job = "전사";
            AttackPower = 10;
            ItemAttackPower = 0;
            DefensePower = 5;
            ItemDefensePower = 0;
            Hp = 100;
            Gold = 1500;
            MyItem = new Item[2];
        }

        public void WearItem(Item item)
        {
            item.IsWear = true;
            if (item.AttackPower != 0)
            {
                AttackPower += item.AttackPower;
                ItemAttackPower += item.AttackPower;
            }
            else
            {
                DefensePower += item.DefensePower;
                ItemDefensePower += item.DefensePower;
            }
        }

        public void UnWearItem(Item item)
        {
            item.IsWear = false;
            if (item.AttackPower != 0)
            {
                AttackPower -= item.AttackPower;
                ItemAttackPower -= item.AttackPower;
            }
            else
            {
                DefensePower -= item.DefensePower;
                ItemDefensePower -= item.DefensePower;
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

            if (ItemAttackPower > 0) Console.WriteLine($"{AttackPower} (+{ItemAttackPower})");
            else Console.WriteLine($"{AttackPower}");

            if (ItemDefensePower > 0) Console.WriteLine($"{DefensePower} (+{ItemDefensePower})");
            else Console.WriteLine($"{DefensePower}");

            Console.WriteLine($"체 력 : {Hp}");
            Console.WriteLine($"Gold : {Gold} G");
            Console.WriteLine("");

            Console.WriteLine("0. 나가기");
            Console.WriteLine("원하시는 행동을 입력해주세요");
        }
    }
}