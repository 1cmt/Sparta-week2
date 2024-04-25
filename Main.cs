using System;

namespace Dungeon 
{
    //시간부족으로 던전 구현은 실패했습니다 ㅠㅠ
    internal class Program
    {
        private int choice;

        void PrintMain()
        {
            Console.WriteLine("");
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
            Console.WriteLine("");
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("0. 게임 종료");
            Console.WriteLine("");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
        }

        void PlayGame()
        {
            bool showList = true;
            Status myStatus = new Status();
            Inventory myInventory = new Inventory();
            Shop myShop = new Shop();

            while(true)
            {
                if(showList) PrintMain();
                Console.Write(">> ");

                int choice;
                if(int.TryParse(Console.ReadLine(), out choice))
                {
                    switch(choice)
                    {
                        case 0:
                            return;
                        case 1:
                            myStatus.ShowStatus();
                            break;
                        case 2:
                            myInventory.showInventory(myStatus);
                            break;
                        case 3:
                            myShop.ShowShop(myStatus, myInventory);
                            break;
                        default:
                            Console.WriteLine("잘못된 입력입니다");
                            showList = false;
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다");
                    showList = false;
                }
            }
        }
        
        static void Main(string[] args)
        {
            Program pg = new Program();

            pg.PlayGame();
        }
    }
}