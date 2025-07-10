using System.Diagnostics.Tracing;

namespace TextGame
{
    internal class Program
    {
        static int level = 1;
        static string job = "전사";
        static int attack = 10;
        static int attackPlus;
        static int shield = 5;
        static int shieldPlus;
        static int strength = 100;
        static int gold = 1500;
        
        class Item
        {
            public string Name; //아이템 이름
            public string StatType; //능력치 타입
            public int StatValue; //능력치 수치
            public string Description; //아이템 설명
            public bool Equipped; //장착 여부
            public int Price;    //가격
            public bool BuyStat; //구매 상태
        }

        static List<Item> itemList = new List<Item>() { //아이템 관리할 리스트 생성/초기화
            new Item() { Name="무쇠갑옷", StatType="방어력", StatValue=9, Description="무쇠로 만들어져 튼튼한 갑옷입니다.", Equipped=false, Price=1000, BuyStat=false},
            new Item() { Name="스파르타의 창", StatType="공격력", StatValue=15, Description="스파르타의 전사들이 사용했다는 전설의 창입니다.", Equipped=false, Price=3500, BuyStat=false},
            new Item() { Name="낡은 검", StatType="공격력", StatValue=2, Description="쉽게 볼 수 있는 낡은 검 입니다.", Equipped=false, Price=500, BuyStat=false},
            new Item() { Name="청동 도끼", StatType="공격력", StatValue=5, Description="어디선가 사용됐던거 같은 도끼입니다.", Equipped=false, Price=800, BuyStat=true},
            new Item() { Name="씹다 뱉은 껌", StatType="공격력", StatValue=1, Description="누군가가 씹다 뱉은 껌입니다.", Equipped=false, Price=50, BuyStat=false}
        };

        static List<Item> boughtItems = itemList.Where(item => item.BuyStat).ToList();
        //Where로 구매한 아이템만 필터링(BuyStat=true)
        //그 결과를 bougthItems 리스트로 만들어서 사용함(주소값 참조라 얕은 복사임)


        static void Main(string[] args)
        { //필수 기능 : 게임시작화면, 상태보기, 인벤토리, 장착관리, 상점, 아이템구매
            bool End = true;

            while (End)
            {
                Console.WriteLine("\n\n------------------------------");
                Console.WriteLine("바보몽총이 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
                Console.WriteLine("0. 게임 종료");
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.");

                int act;
                try //숫자가 아닌 값 입력했을 때 예외처리
                {
                    Console.Write(">> ");
                    act = int.Parse(Console.ReadLine());
                }
                catch (FormatException) //FormatException : 데이터 형식이 잘못되었을 때 발생하는 예외
                {
                    Console.WriteLine("잘못된 입력입니다. 숫자를 입력해주세요!");
                    continue; //다시 선택화면으로 돌아가기
                }

                switch (act)
                {
                    case 0:
                        Console.WriteLine("게임을 종료합니다.");
                        Console.WriteLine("아무 키나 누르면 종료됩니다...");
                        Console.ReadKey();  // 사용자가 키를 누를 때까지 대기
                        End = false;
                        break;
                    case 1:    //상태보기
                        Stat();
                        break;
                    case 2:    //인벤토리
                        Inventory();
                        break;
                    case 3:    //상점
                        Shop();
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.\n");
                        break;
                }
            }
                
        }

        static void Stat() { //상태 보기
            while (true)
            {
                Console.WriteLine("\n\n------------------------------");
                Console.WriteLine($"Lv. {level}");
                Console.WriteLine($"Chad ( {job} )");
                Console.WriteLine($"공격력 : {attack + attackPlus} (+{attackPlus})");
                Console.WriteLine($"방어력 : {shield + shieldPlus} (+{shieldPlus})");
                Console.WriteLine($"체 력 : {strength}");
                Console.WriteLine($"Gold : {gold} G\n");
                Console.WriteLine("0. 나가기\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.");

                int act;
                try //숫자나 0이 아닌 값 입력했을 때 예외처리
                {
                    Console.Write(">> ");
                    act = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    continue; //다시 선택화면으로 돌아가기
                }

                if (act == 0)
                {
                    break;
                }
                else { //0이 아닌 다른 숫자 입력 시
                    Console.WriteLine("잘못된 입력입니다.\n");
                    continue;
                }
            }
        }

        static void Inventory(){ //인벤토리
            while (true)
            {
                Console.WriteLine("\n\n------------------------------");
                Console.WriteLine("인벤토리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

                if (boughtItems.Count == 0) //구매한 아이템이 없으면
                {
                    Console.WriteLine("[아이템 목록]\n(아이템이 없습니다.)\n");
                }
                else { //구매한 아이템이 있으면
                    Console.WriteLine("[아이템 목록]\n");
                    for (int i = 0; i < boughtItems.Count; i++) {
                        string equippedMark = boughtItems[i].Equipped ? "[E] " : "";   //장착여부 표시. 장착=true면 [E]표시
                        Console.WriteLine($"- {equippedMark}{boughtItems[i].Name}\t | {boughtItems[i].StatType} +{boughtItems[i].StatValue} | {boughtItems[i].Description}");
                        //아이템 출력 : [장착]아이템 이름 등등
                    }
                    Console.WriteLine();
                }
                    Console.WriteLine("0. 나가기");
                    Console.WriteLine("1. 장착 관리\n");
                    Console.WriteLine("원하시는 행동을 입력해주세요.");

                int act;
                try //숫자가 아닌 값 입력했을 때 예외처리
                {
                    Console.Write(">> ");
                    act = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    continue; //다시 선택화면으로 돌아가기
                }

                if (act == 0)
                {
                    break;
                }
                else if (act == 1) {
                    EqipManage();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.\n");
                    continue;
                }
            }
        }

        static void EqipManage() { //장착 관리
            while (true)
            {
                Console.WriteLine("\n\n------------------------------");
                Console.WriteLine("인벤토리 - 장착 관리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
                if (boughtItems.Count == 0) //구매한 아이템이 없으면
                {
                    Console.WriteLine("[아이템 목록]\n(아이템이 없습니다.)\n");
                }
                else
                { //구매한 아이템이 있으면
                    Console.WriteLine("[아이템 목록]\n");
                    for (int i = 0; i < boughtItems.Count; i++)
                    {
                        string equippedMark = boughtItems[i].Equipped ? "[E] " : "";   //장착여부 표시. 장착=true면 [E]표시
                        Console.WriteLine($"{i+1}. {equippedMark}{boughtItems[i].Name}\t | {boughtItems[i].StatType} +{boughtItems[i].StatValue} | {boughtItems[i].Description}");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("0. 나가기");

                int act;
                try //숫자나 0이 아닌 값 입력했을 때 예외처리
                {
                    Console.Write(">> ");
                    act = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    continue; //다시 선택화면으로 돌아가기
                }

                if (act == 0) //act가 0이면 나가기
                {
                    break;
                }
                else if (act > 0) { //act가 아이템 번호라면 아이템 선택 & 장착 처리
                    int index = act - 1; //act는 1부터 시작하고, 배열 인덱스는 0부터니깐 배열 인덱스 맞추기 위해 선언
                    if (index >= 0 && index < boughtItems.Count) //인덱스가 0부터 구매한 아이템 개수 사이에 있어야함
                    {
                        Item selectedItem = boughtItems[index]; // 선택한 아이템

                        if (selectedItem.Equipped) //장착 상태가 true
                        {
                            selectedItem.Equipped = false;  //입력되면 장착 해제
                            ApplyStat(selectedItem, false); // 능력치 차감
                            Console.WriteLine($"{selectedItem.Name}이(가) 장착 해제되었습니다.");
                        }
                        else { //장착 상태가 false인데 누르면 true로 바뀌면서 장착됨
                            selectedItem.Equipped = true;
                            ApplyStat(selectedItem, true); // 능력치 추가
                            Console.WriteLine($"{selectedItem.Name}이(가) 장착되었습니다.");
                        }
                    }
                    else { //아이템이 존재하지 않으면
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                }
                else
                { //0이 아닌 다른 숫자 입력 시
                    Console.WriteLine("잘못된 입력입니다.\n");
                    continue;
                }
            }
        }

        static void ApplyStat(Item item, bool equip) { //아이템, 장착여부
            int stat = equip ? 1 : -1; //장착 상태가 참이면 1, 거짓이면 -1
            if (item.StatType == "공격력") {
                attackPlus += item.StatValue * stat; //공격력 증가/감소
            }
            else if (item.StatType == "방어력")
            {
                shieldPlus += item.StatValue * stat; //방어력 증가/감소
            }
        }

        static void Shop() //상점
        {
            while (true)
            {
                Console.WriteLine("\n\n------------------------------");
                Console.WriteLine("상점");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{gold} G\n");
                Console.WriteLine("[아이템 목록]\n");
                for (int i = 0; i < itemList.Count; i++) //아이템리스트 출력
                {
                    if (itemList[i].BuyStat == true) //아이템 구매한 상태라면
                    {
                        Console.WriteLine($"- {itemList[i].Name}\t | {itemList[i].StatType} +{itemList[i].StatValue} | {itemList[i].Description}\t | 구매완료");
                    }
                    else
                    {
                        Console.WriteLine($"- {itemList[i].Name}\t | {itemList[i].StatType} +{itemList[i].StatValue} | {itemList[i].Description}\t | {itemList[i].Price} G");
                    }
                }

                Console.WriteLine("\n0. 나가기");
                Console.WriteLine("1. 아이템 구매\n");

                Console.WriteLine("원하시는 행동을 입력해주세요.");

                int act;
                try //숫자가 아닌 값 입력했을 때 예외처리
                {
                    Console.Write(">> ");
                    act = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    continue; //다시 선택화면으로 돌아가기
                }

                if (act == 0) //0 누르면 나가기
                {
                    break;
                }
                else if (act == 1)
                {
                    ItemBuy();
                }
                else {
                    Console.WriteLine("잘못된 입력입니다.\n");
                    continue;
                }
            }
        }


        static void ItemBuy() { //상점에서 아이템 구매
            while(true)
            {
                Console.WriteLine("\n\n------------------------------");
                Console.WriteLine("상점 - 아이템 구매");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{gold} G\n");
                Console.WriteLine("[아이템 목록]\n");
                for (int i = 0; i < itemList.Count; i++) //아이템리스트 출력
                {
                    if (itemList[i].BuyStat == true) //아이템 구매한 상태라면
                    {
                        Console.WriteLine($"{i+1}. {itemList[i].Name}\t | {itemList[i].StatType} +{itemList[i].StatValue} | {itemList[i].Description}\t | 구매완료");
                    }
                    else
                    {
                        Console.WriteLine($"{i+1}. {itemList[i].Name}\t | {itemList[i].StatType} +{itemList[i].StatValue} | {itemList[i].Description}\t | {itemList[i].Price} G");
                    }
                }

                Console.WriteLine("\n0. 나가기\n");

                Console.WriteLine("원하시는 행동을 입력해주세요.\n");

                int act;
                try //숫자가 아닌 값 입력했을 때 예외처리
                {
                    Console.Write(">> ");
                    act = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    continue; //다시 선택화면으로 돌아가기
                }

                if (act == 0) //0 누르면 나가기
                {
                    break;
                }
                else if(act > 0){
                    int index = act - 1;
                    if (index >= 0 && index < itemList.Count)
                    {
                        if (itemList[index].BuyStat == false) //선택한 아이템이 팔리지 않았고, 아이템 범위 이내라면
                        {
                            //구매 가능
                            if (gold >= itemList[index].Price) //보유 금액이 충분하다면
                            {
                                gold -= itemList[index].Price; //금액 감소
                                itemList[index].BuyStat = true; //구매 상태
                                boughtItems.Add(itemList[index]);   //구매한 아이템 추가
                                Console.WriteLine($"구매를 완료했습니다.\n 남은 금액 : {gold}");
                            }
                            else { //보유 금액이 부족하다면
                                Console.WriteLine("Gold 가 부족합니다.");
                            }
                        }
                        else if (itemList[index].BuyStat == true)
                        { //선택한 아이템을 이미 가지고 있다면
                            Console.WriteLine("이미 구매한 아이템입니다.");
                        }
                    }
                    else {
                        Console.WriteLine("잘못된 입력입니다.");
                    }

                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.\n");
                    continue;
                }
            }
        }
    }
}