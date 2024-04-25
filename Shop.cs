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
            ItemArr = new Item[9];
            ItemArr[0] = new Item();
            ItemArr[1] = new DefenseItem(1, "수련자 갑옷", 5, "수련에 도움을 주는 갑옷입니다.", 1000);
            ItemArr[2] = new DefenseItem(2, "무쇠 갑옷", 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2000);
            ItemArr[3] = new DefenseItem(3, "스파르타의 갑옷", 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500);
            ItemArr[4] = new DefenseItem(4, "부실한 갑옷", 2, "티타늄으로 만들어진 매우 강력한 갑옷입니다.", 300);
            ItemArr[5] = new AttackItem(5, "낡은 검", 2, "쉽게 볼 수 있는 낡은 검 입니다.", 600);
            ItemArr[6] = new AttackItem(6, "청동 도끼", 5, "어디선가 사용됐던거 같은 도끼입니다.", 1500);
            ItemArr[7] = new AttackItem(7, "스파르타의 창", 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 2000);
            ItemArr[8] = new AttackItem(8, "부실한 창", 1, "미지에서 온 강력한 창입니다.", 200);

            IsSoldArr = new bool[9];
            SellableIdArr = new int[9];
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

                    PrintList(ShopEnum.ShowShop, myStatus);

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
                    PrintList(ShopEnum.BuyItem, myStatus);
                    Console.WriteLine("0. 나가기");
                    Console.WriteLine("");

                    Console.WriteLine("구매를 원하는 아이템 번호를 입력해주세요");
                }
                Console.Write(">> ");

                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    if (choice == 0) return;

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
                            continue;
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
                    PrintList(ShopEnum.SellItem, myStatus);
                    Console.WriteLine("0. 나가기");
                    Console.WriteLine("");
                    Console.WriteLine("판매를 원하는 아이템 번호를 입력해주세요");
                }
                Console.Write(">> ");

                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    if (choice == 0) return;
                    else if (choice >= myInventory.Count() || choice < 0) // >= 한 이유는 인덱스 맞추고자 인벤토리에 비어있는 아이템을 추가했기에
                    {
                        Console.WriteLine("잘못된 입력입니다");
                        showList = false;
                        continue;
                    }
                    else //판매 로직
                    {
                        int itemId = SellableIdArr[choice]; //판매가능한 아이템 Id를 보관하는 배열에서 Id를 가져옴
                        myStatus.Gold += ItemArr[itemId].SellPrice; //판매했으니 돈 추가

                        SellableIdArr[choice] = 0; //판매했으니 Id는 사용하지 않는 0로
                        IsSoldArr[itemId] = false; //판매했으니 이제 판매 불가해짐
                        myInventory.RemoveItem(itemId, myStatus);
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

        void PrintList(ShopEnum methodName, Status myStatus)
        {
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{myStatus.Gold} G");
            Console.WriteLine("");

            Console.WriteLine("[아이템 목록]");

            int sellNum = 1;
            for (int i = 1; i < ItemArr.Length; i++)
            {
                switch (methodName)
                {
                    case ShopEnum.ShowShop:
                    case ShopEnum.BuyItem:
                        Console.Write("- ");
                        if (methodName == ShopEnum.BuyItem) Console.Write($"{i} ");

                        ItemArr[i].PrintInfo();
                        if (!IsSoldArr[i]) Console.WriteLine($"| {ItemArr[i].Price} G");
                        else Console.WriteLine("| 구매완료");
                        break;
                    case ShopEnum.SellItem:
                        if (IsSoldArr[i])
                        {
                            Console.Write($"- {sellNum} ");
                            ItemArr[i].PrintInfo();
                            Console.WriteLine($"| {ItemArr[i].SellPrice} G");
                            SellableIdArr[sellNum++] = i; //판매가능한 아이템의 Id(== i)를 판매가능 아이템 Id 보관 배열에 담는다.
                        }
                        else
                        {
                            continue;
                        }
                        break;
                }
            }
            Console.WriteLine("");
        }
    }

}