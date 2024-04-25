using System;

namespace Dungeon
{
    public class Shop
    {
        public Item[] ItemArr { get; set; }
        public bool[] IsSoldArr { get; set; } //상점에서 아이템이 팔렸는지 여부를 bool 배열을 두어 확인
        public int[] SellableIdArr { get; set; } //유저가 판매가능한 아이템의 Id를 저장, 판매 불가는 초기값인 0

        public Shop()
        {
            ItemArr = new Item[8];
            ItemArr[0] = new Item(1, "수련자 갑옷", 0, 5, "수련에 도움을 주는 갑옷입니다.", 1000);
            ItemArr[1] = new Item(2, "무쇠 갑옷", 0, 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2000);
            ItemArr[2] = new Item(3, "스파르타의 갑옷", 0, 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500);
            ItemArr[3] = new Item(4, "티타늄 갑옷", 0, 20, "티타늄으로 만들어진 매우 강력한 갑옷입니다.", 4000);
            ItemArr[4] = new Item(5, "낡은 검", 2, 0, "쉽게 볼 수 있는 낡은 검 입니다.", 600);
            ItemArr[5] = new Item(6, "청동 도끼", 5, 0, "어디선가 사용됐던거 같은 도끼입니다.", 1500);
            ItemArr[6] = new Item(7, "스파르타의 창", 7, 0, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 2000);
            ItemArr[7] = new Item(8, "미지의 창", 15, 0, "미지에서 온 강력한 창입니다.", 3000);

            IsSoldArr = new bool[8];
            SellableIdArr = new int[8];
        }

        public void ShowShop(Status myStatus, Inventory myInventory)
        {
            bool showList = true; //목록을 띄워야 하는가
            int choice;

            while (true)
            {
                if (showList)
                {
                    Console.WriteLine("");
                    Console.WriteLine("상점");
                    Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                    Console.WriteLine("");

                    PrintList(0, myStatus);

                    Console.WriteLine("1. 아이템 구매");
                    Console.WriteLine("2. 아이템 판매");
                    Console.WriteLine("0. 나가기");
                    Console.WriteLine("");
                    Console.WriteLine("원하시는 행동을 입력해주세요.");
                }

                Console.Write(">> ");

                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 0:
                            return;
                        case 1:
                            BuyItem(myStatus, myInventory);
                            showList = true;
                            break;
                        case 2:
                            SellItem(myStatus, myInventory);
                            showList = true;
                            break;
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

        void BuyItem(Status myStatus, Inventory myInventory)
        {
            bool showList = true;
            int choice;

            while (true)
            {
                if (showList)
                {
                    Console.WriteLine("");
                    Console.WriteLine("상점 - 아이템 구매");
                    PrintList(1, myStatus);
                    Console.WriteLine("0. 나가기");
                    Console.WriteLine("");

                    Console.WriteLine("구매를 원하는 아이템 번호를 입력해주세요");
                }
                Console.Write(">> ");

                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    if (choice == 0) return;

                    choice--; //고르는건 1번시작이지만 배열은 0번 시작이다.

                    if (choice >= ItemArr.Length || choice < 0)
                    {
                        Console.WriteLine("잘못된 입력입니다");
                        showList = false;
                        continue;
                    }
                    else if (IsSoldArr[choice])
                    {
                        Console.WriteLine("이미 구매한 아이템입니다");
                        showList = false;
                        continue;
                    }
                    else
                    {
                        if (myStatus.Gold >= ItemArr[choice].Price)
                        {
                            IsSoldArr[choice] = true;
                            myInventory.AddItem(ItemArr[choice]);
                            myStatus.Gold -= ItemArr[choice].Price;
                            Console.WriteLine("");
                            Console.WriteLine("구매 완료");
                            showList = true;
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Gold 가 부족합니다.");
                            showList = false;
                            return;
                        }
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

        void SellItem(Status myStatus, Inventory myInventory)
        {
            bool showList = true;
            int choice;

            while (true)
            {
                if (showList)
                {
                    Console.WriteLine("");
                    Console.WriteLine("상점 - 아이템 판매");
                    PrintList(2, myStatus);
                    Console.WriteLine("0. 나가기");
                    Console.WriteLine("");
                    Console.WriteLine("판매를 원하는 아이템 번호를 입력해주세요");
                }
                Console.Write(">> ");

                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    if (choice == 0) return;
                    else if (choice > myInventory.Count() || choice < 0)
                    {
                        Console.WriteLine("잘못된 입력입니다");
                        showList = false;
                        continue;
                    }
                    else
                    {
                        choice--;
                        myStatus.Gold += ItemArr[SellableIdArr[choice]].SellPrice;
                        SellableIdArr[choice] = 0; //판매했으니 이제 판매는 불가하다.
                        myInventory.RemoveItem(SellableIdArr[choice]);
                        showList = true;
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다");
                    showList = false;
                    return;
                }
            }
        }

        void PrintList(int methodNum, Status myStatus)
        {
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{myStatus.Gold} G");
            Console.WriteLine("");

            Console.WriteLine("[아이템 목록]");

            int sellNum = 0;
            for (int i = 0; i < ItemArr.Length; i++)
            {
                if (methodNum == 0 || methodNum == 1)
                {
                    Console.Write("- ");
                    if (methodNum == 1) Console.Write($"{i + 1} ");

                    ItemArr[i].PrintInfo();
                    if (!IsSoldArr[i]) Console.WriteLine($"| {ItemArr[i].Price} G");
                    else Console.WriteLine("| 구매완료");
                }
                else //아이템 판매 메서드일 때
                {
                    if (!IsSoldArr[i]) continue;
                    else
                    {
                        Console.Write($"- {sellNum + 1} ");
                        ItemArr[i].PrintInfo();
                        Console.WriteLine($"| {ItemArr[i].SellPrice} G");
                        SellableIdArr[sellNum++] = ItemArr[i].Id; //판매가능한 아이템의 Id를 배열에 담는다.
                    }
                    //이렇게 하는 이유는 아이템 판매시 팔고자 하는 아이템 번호를 넣어야하는데 이를 Id와 매칭시키기 위해서다.
                }
            }

            Console.WriteLine("");
        }

    }
}