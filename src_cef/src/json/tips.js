export default {
    job: {
        get: {//Когда подошёл к боту Emma Smith, у которого можно устроиться на работу или уволиться
            desc: `Choose any job you like, click "Apply", and then get to the starting point, which will be automatically set on the map. You can only work at one job at a time!`,
        },
        dismissal: {//Когда уволился с работы
            desc: `You can choose another job or join one of the factions at any time. Information about all factions can be found in the help menu by pressing F10, then "Criminal Factions" and "State Factions". Note that it is not possible to be in a faction and a job at the same time.`,
        },
        electric: {//Когда подошёл к боту Ryan Nelson
            desc: "Press E to start the work day. Red marks will appear on the map. Run up to them to complete tasks for which you will receive money. To end the work day, run up to the bot again and press E.",
        },
        lawn: {//Когда подошёл к работе газонокосильщика
            desc: "Get on the lawnmower to get started. Then follow the checkpoints. You will receive money for each checkpoint you pass.",
        },
        post: {//Когда подошёл к Отделению почты
            desc: `Press E, then "Get Started". You need to deliver mail to addresses that will appear on the map automatically. You can get a working transport, for this stand at the white checkpoint and press E.`,
        },
        post: {//Когда закончились посылки
            desc: "You have no parcels left for delivery. To get new ones, go back to the post office. The label on the card has been set automatically.",
        },
        taxi: {//Когда подошёл к таксопарку
            desc: `Get into a cab to rent transportation. There are 4 classes of cabs, which differ in fares and rental prices. To begin with, we recommend choosing the "Econom" class.`,
        },
        taxi: {//Когда арендовал транспорт
            desc: "Transport is rented, now we have to wait for the first order!",
        },
        taxi: {//Когда появился заказ
            desc: "There is an order in the system. To accept it, use the command /ta [ID]. Be careful, other players are placing orders.",
        },
        taxi: {//Когда клиент сел в транспорт
            desc: "Deliver the client to the desired address. To issue a check for payment, use the command /tprice [ID] [Amount].",
        },
        bus: {//Когда подошел к автопарку автобусов
            desc: "Find a free bus and rent it. Then follow the checkpoints. You will receive money for each checkpoint you pass.",
        },
        mechanik: {//Когда подошел к автопарку механиков
            desc: "Find a free pickup truck and rent it.",
        },
        mechanik: {//Когда арендовал транспорт
            desc: "The vehicles are rented, now we have to wait for the first order!",
        },
        mechanik: {//Когда появился заказ
            desc: "There is an order in the system. To accept it, use the command /ma [ID]. Be careful, other players are placing orders.",
        },
        mechanik: {//Когда приехал на заказ
            desc: "To offer repairs, use the command /repair [ID] [Price]. In addition to repairs, you can refuel customer vehicles. For more information, see the help menu (F10)>Works>Mechanic.",
        },
        truck: {//Когда подошел к автопарку дальнобойщиков
            desc: "Find a free truck and rent it.",
        },
        truck: {//Когда арендовал транспорт
            desc: "Use the /orders command to select your order.",
        },
        truck: {//Когда взял заказ
            desc: "Pick up your order from the loading location. The label on the map has been set automatically.",
        },
        truck: {//Когда забрал прицеп
            desc: "Deliver the order to the business. The label on the card has been set automatically.",
        },
        col: {//Когда подошел к автопарку инкассаторов
            desc: "Find a free truck and rent it..",
        },
        col: {//Когда арендовал транспорт
            desc: "You need to deliver bags to addresses that will appear on the map automatically.",
        },
        col: {//Когда закончились мешки
            desc: `You have no delivery bags left. To get new ones, go back to the parking lot, stand on the "Get Money Bags" marker and press E. The marker on the map was set automatically.`,
        },
    },
    other: {
        death: {//Игрока убивают
            desc: `If you notice a violation of server rules, then contact the administration. To do this, use the command /report.
                    The rules of the server can be found on the forum, which can be opened at: https://forum.redage.net
                    In addition to the rules, you can leave a complaint about a player or administrator in the forum. To do this, go to the "Complaints" section.`,
        },
        money: {//Игрок имеет на руках больше 15.000$
            desc: "You are holding a large sum of money in your hands. Be careful, you can be robbed by other players. To save money, you can put it into a bank account. To find a bank, press M>GPS>Near places>Near ATM.",
        },
        money: {//Игрок имеет больше 50.000$ и не владеет домом
            desc: "You have saved enough money to buy a house. Free houses are indicated by a green house on the map. If they are not available, you can buy a house from another player. There are often advertisements for houses for sale in the chat room.",
        },
        money: {//Игрок имеет дом и больше 10.000
            desc: "You can buy your first vehicle. To do this, go to Autoroom. It can be found via GPS. To do this, press M>GPS>Businesses>Low Autoroom.",
        },
        simcard: {//Игрок открыл телефон
            desc: "To receive and make calls, you need to buy a SIM card. You can do this in the 24/7 store. Press M>GPS>Businesses>24/7.",
        },
        transfer: {//Игроку пришли деньги на банковский счет
            desc: "Someone sent money to your bank account. Use an ATM to withdraw money. To do this, press M>GPS>Near places>Near ATM.",
        },
        stock: {//Когда игрок получил предмет на склад
            desc: "A parcel has been delivered to your warehouse. The warehouse can be found on the map, it looks like a blue box.",
        },
        pickup: {//Когда игрок поднял предмет с земли
            desc: "You have picked up the item and it has been successfully moved into your inventory. Open your inventory - I.",
        },
        bag: {//Когда в инвентаре игрока находится более 14 предметов
            desc: "You already have a lot of items in your inventory. You can buy a bag from the 24/7 store in which you can store items. Store 24/7 can be found by pressing M>GPS>Businesses>24/7.",
        },
        bag: {//Когда игрок купил сумку
            desc: "To fold or remove items from the bag, you must put it on. Be careful, if you die the moment the bag is put on, you will lose it.",
        },
        heal: {//Когда у игрока мало хп
            desc: "You are low on hp. The easiest way to replenish hp is to use the food you can buy at the 24/7 store or Burger-Shot. You can find them using the GPS in your phone.",
        },
        demorgan: {//Когда игрок получил деморган
            desc: "You were punished for breaking the rules. Use the /time command to find out the reason and duration of the punishment. If you do not agree with the punishment, you can leave a complaint against the administrator on the forum https://forum.redage.net",
        },
        cuff: {//На игрока надели наручники или стяжки
            desc: "You are handcuffed. Under no circumstances should you leave the game until they are removed. This will result in a 240 minute penalty. The handcuffs can be removed at the black market.",
        },
    },
    location: {
        ems: {//Игрока появился в EMS после смерти
            desc: `To restore health, stand on the white "Start Treatment" marker and press E. To restore health instantly, use a doctor. Remember, doctors are real players.`,
        },
        ghetto: {//Игрок заехал в гетто
            desc: "You are in a dangerous ghetto neighborhood. Be careful, you could be robbed, kidnapped, or killed!",
        },
        voice: {//Игрок вошел в зону войс-чата и слышит разговоры
            desc: "To chat in voice chat, press the N key. The key can be changed in the binder menu (Tab). Do not resort to insulting your parents, religion and nation. It is forbidden and punishable on the server!",
        },
        voicemute: {//Игрок вошел в зону c игроком, который установил решим "Я не слышу"
            desc: `If you see the inscription "Can't hear" above the player's head, he can only see the text chat. You can set such a solution in the settings. Press I>Settings>I can't hear.`,
        },
        autoroom: {//Игрок подошёл к авто или мото руму и не имеет дома
            desc: "You can buy personal transportation at the salon, but you have to buy a house first!",
        },
        house: {//Игрок подошёл к свободному или чужому дому
            desc: "You can buy a personal home that can be improved (including garage spaces). They can be sold to other players. In the house you can store things and vehicles, install a first aid kit, and decorate the interior.",
        },
        autoschool: {//Игрок подошёл к автошколе
            desc: "You can get driving licenses at the driving school. They are needed for employment in some jobs and fractions. The police can give you a fine for driving without a license.",
        },
        greenzone: {//Игрок вошёл в ЗЗ
            desc: "You are in the Green Zone (Peace Zone). Damage cannot be done in the Zone and all criminal actions are forbidden.",
        },
    },
    vehicles: {
        seat: {//Игрок сел в машину
            desc: "To start the engine, use the B key.",
        },
        key: {//Игрок зашёл в гараж, в котором стоит купленная машина
            desc: "ЧTo use a personal vehicle, you need keys. Press M>Machines>My cars>Select a vehicle>Get a duplicate key.",
        },
        drive: {//Спустя минуту поездки на транспорте
            desc: "Vehicles can be navigated using autopilot. To do this, you need to put a marker on the map and press Z.",
        },
        belt: {//Игрок сел на пассажирское место
            desc: "To fasten your seat belt, press G>Belt buckle.",
        },
        key: {//Игрок попытался сесть в чужой закрытый транспорт
            desc: "If the car is locked, you can open it only with the key. The key can be given by the owner of the vehicle.",
        },
        lsc: {//Если игрок купил машину, прошёл час, а он не заходил в LSC
            desc: "Personal vehicles can be tuned. This can be done in LS Customs. Press M>GPS>Businesses> LS Customs.",
        },
        seat: {//Когда игрок пытается сесть на водительское место фракционного транспорта
            desc: "Only faction members can drive faction vehicles.",
        },
    },
    licenses: {
        gun: {//Игрок подошёл к gun-shop
            desc: "You need a license to buy a gun. You can get one at the police station. Get more information from the officers.",
        },
        gun: {//Игрок получил лицензию на оружие
            desc: "You can now buy weapons in the store. To find the right store, press M>GPS>Businesses>Gun shop. Be careful, the police can take away your license for breaking the law.",
        },
        licB: {//Игрок получил лицензию категории В
            desc: "You have been issued a Category B license. Now you can choose one of the following jobs: Letter carrier, Taxi Driver, Mechanic. You can get a job at City Hall, which is shown on the map as a flag.",
        },
        licC: {//Игрок получил лицензию категории С
            desc: "You have received your C license. Now you can choose one of the following jobs: Bus Driver, Truck Driver, Collector. You can get a job at City Hall, which is shown on the map as a flag.",
        },
    },
}