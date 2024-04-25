using System;
using Microsoft.VisualBasic;

namespace Dungeon
{
    public class Inventory
    {
        public List<Item> ItemList { get; set; }

        public Inventory()
        {
            ItemList = new List<Item>();
            ItemList.Add(new Item());
        }

        public int Count()
        {   
            return ItemList.Count;
        }

        public void AddItem(Item item)
        {
            ItemList.Add(item);
        }

        public void RemoveItem(int itemId, Status myStatus) 
        {
            for (int i = 1; i < ItemList.Count; i++)
            {
                if(ItemList[i].Id == itemId)
                {
                    if(ItemList[i].IsWear) myStatus.UnWearItem(ItemList[i]);
                    ItemList.RemoveAt(i);
                }
            }
        }

        public void ShowInventory(Status myStatus)
        {
            bool showList = true;
            int choice = 0;

            while (true)
            {
                if (showList)
                {
                    Console.WriteLine("");
                    Console.WriteLine("인벤토리");
                    Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다");
                    Console.WriteLine("");
                    PrintList(InventoryEnum.ShowInventory);
                    Console.WriteLine("1. 장착 관리");
                    Console.WriteLine("2. 나가기");
                    Console.WriteLine("");

                    Console.WriteLine("원하시는 행동을 입력해주세요.");
                }
                Console.Write(">> ");

                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            ManageWear(myStatus);
                            showList = true;
                            break;
                        case 2:
                            return;
                        default:
                            Console.WriteLine("잘못된 입력입니다");
                            showList = false;
                            continue;
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다");
                    showList = false;
                    continue;
                }

            }
        }

        void ManageWear(Status myStatus)
        {
            bool showList = true;
            int choice = 0;

            while (true)
            {
                if (showList)
                {
                    Console.WriteLine("");
                    Console.WriteLine("인벤토리 - 장착관리");
                    Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다");
                    Console.WriteLine("");
                    PrintList(InventoryEnum.ManageWear);
                    Console.WriteLine("0. 나가기");
                    Console.WriteLine("");
                    Console.WriteLine("장착 혹은 장착 해제를 원하는 아이템의 번호를 적어주세요");
                }
                Console.Write(">> ");

                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    if (choice == 0) return;
                    else if (choice > 0 && choice <= ItemList.Count)
                    {
                        if(!ItemList[choice].IsWear) myStatus.WearItem(ItemList[choice]);
                        else myStatus.UnWearItem(ItemList[choice]);

                        showList = true;
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다");
                        showList = false;
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다");
                    showList = false;
                    continue;
                }
            }
        }

        void PrintList(InventoryEnum methodName)
        {
            Console.WriteLine("[아이템 목록]");

            for(int i = 1; i < ItemList.Count; i++)
            {
                Console.Write("- ");
                
                if(methodName == InventoryEnum.ManageWear)  
                {
                    Console.Write($"{i} ");
                }
                
                if (ItemList[i].IsWear)
                {
                    Console.Write("[E]");
                }
                ItemList[i].PrintInfo();
                Console.WriteLine("");
            }

            Console.WriteLine("");
        }
    }
}