export default [
    {
        "cmd":"/report",
        "descr":"Submit a complaint to the administration.",
        "category":"Common"
    },  
    {
        "cmd":"/password",
        "descr":"Change your password.",
        "category":"Common"
    },
    {
        "cmd":"/myguest",
        "descr":"Enter a private house without opening the doors.",
        "category":"Common"
    },
    {
        "cmd":"/buybiz",
        "descr": "Buy a business.",
        "category":"Common"
    },
    {
        "cmd":"/sellbiz [id] [price]",
        "descr":"Sell a business to a player.",
        "category":"Common"
    },
    {
        "cmd":"/time",
        "descr":"Find out the time left on your punishment.",
        "category":"Common"
    },
    {
        "cmd":"/dice [id] [amount]",
        "descr":"Suggest a dice game.",
        "category":"Common"
    },
    {
        "cmd":"/findtrailer",
        "descr":"Mark location of trailer (Trucker Job).",
        "category":"Common"
    },
    {
        "cmd":"/q",
        "descr":"Fast disconnection from the server.",
        "category":"Common"
    },
    {
        "cmd":"Pressing 'G'",
        "descr":"Interaction Menu",
        "category":"Common"
    },
    {
        "cmd":"Pressing 'N'",
        "descr":"Microphone.",
        "category":"Common"
    },
    {
        "cmd":"Pressing 'I'",
        "descr":"Opens the inventory and statistics of the character.",
        "category":"Common"
    },
    {
        "cmd":"Pressing 'M'",
        "descr":"Open Phone.",
        "category":"Common"
    },
    {
        "cmd":"Pressing 'L'",
        "descr":"Lock / Unlock Vehicle.",
        "category":"Common"
    },
    {
        "cmd":"Pressing 'B'",
        "descr":"Start / Stop Vehicle Engine.",
        "category":"Common"
    },
    {
        "cmd":"Pressing 'U'",
        "descr":"List of animations.",
        "category":"Common"
    },
    {
        "cmd":"Pressing '5'",
        "descr":"Show/Hide Player IDs.",
        "category":"Common"
    },
    {
        "cmd":"Pressing '6'",
        "descr":"Enable cruise control.",
        "category":"Common"
    },
    {
        "cmd":"Pressing 'F10'",
        "descr":"Help Menu.",
        "category":"Common"
    },
    {
        "cmd":"Pressing 'F9'",
        "descr":"Donation Panel.",
        "category":"Common"
    },
    {
        "cmd":"Pressing 'F5'",
        "descr":"On/Off HUD interface.",
        "category":"Common"
    },
    {
        "cmd":"Pressing 'F1'",
        "descr":"Open the Rage menu.",
        "category":"Common"
    },
    {
        "cmd":"Pressing 'T'",
        "descr":"Send a chat message.",
        "category":"Chat"
    },
    {
        "cmd":"/b",
        "descr":"Send an ООС chat message.",
        "category":"Chat"
    },
    {
        "cmd":"/me",
        "descr":"Acting out an action in the first person.",
        "category":"Chat"
    },
    {
        "cmd":"/do",
        "descr":"Performing an action from the 3rd person.",
        "category":"Chat"
    },
    {
        "cmd":"/try",
        "descr":"Wagering an action with a random outcome.",
        "category":"Chat"
    },
    {
        "cmd":"/todo",
        "descr":"Acting out words + actions.",
        "category":"Chat"
    },
    {
        "cmd":"/s",
        "descr":"Krichat.",
        "category":"Chat"
    },
    {
        "cmd":"/f",
        "descr":"Faction Chat.",
        "category":"Chat"
    },
    {
        "cmd":"/dep",
        "descr":"Department Chat (General chat of all State factions).",
        "category":"Chat"
    },
    {
        "cmd":"/gov",
        "descr":"Send State Wave message.",
        "category":"Chat"
    },
    {
        "cmd":"/m",
        "descr":"Megaphone Chat.",
        "category":["Chat", "LSPD", "FIB", "Army"]
    },
    {
        "cmd":"/call",
        "descr":"SMS.",
        "category":"Chat"
    },
    {
        "cmd":"/fontsize",
        "descr":"Change the Chat font size (From 10 to 20, Default: 16).",
        "category":"Chat"
    },
    {
        "cmd":"/pagesize",
        "descr":"Change the number of Chat lines (From 5 to 20, Default: 10).",
        "category":"Chat"
    },
    {
        "cmd":"/timestamp",
        "descr":"Time display in Chat.",
        "category":"Chat"
    },
    {
        "cmd":"/chatalpha",
        "descr":"Switch Chat attenuation.",
        "category":"Chat"
    },
    {
        "cmd":"/buyfuel [quantity]",
        "descr":"Buy fuel at a gas station.",
        "category":"Mechanic"
    },
    {
        "cmd":"/sellfuel [id] [quantity] [price per liter]",
        "descr":"Sell fuel to a player.",
        "category":"Mechanic"
    },
    {
        "cmd":"/ma",
        "descr":"Accept the call for an auto mechanic.",
        "category":"Mechanic"
    },
    {
        "cmd":"/repair [id] [price]",
        "descr":"Repair the vehicle.",
        "category":"Mechanic"
    },
    {
        "cmd":"/ta [id]",
        "descr":"Take the challenge.",
        "category":"Taxi"
    },
    {
        "cmd":"/tprice [id] [price]",
        "descr":"Offer a taxi trip and the price.",
        "category":"Taxi"
    },
    {
        "cmd":"/orders",
        "descr":"List of orders.",
        "category":"Trucker"
    },
    {
        "cmd":"/t",
        "descr":"Truckers' walkie-talkie.",
        "category":"Trucker"
    },
    {
        "cmd":"/su [passport number] [number of stars] [reason]",
        "descr":"Put player on the Wanted list.",
        "category":["LSPD", "FIB"]
    },
    {
        "cmd":"/arrest [id]",
        "descr":"Arrest the player (put in jail).",
        "category":["LSPD", "FIB"]
    },
    {
        "cmd":"/rfp [id]",
        "descr":"Release a player from jail.",
        "category":["LSPD", "FIB"]
    },
    {
        "cmd":"/pd [id]",
        "descr":"Take the challenge.",
        "category":["LSPD", "FIB"]
    },
    {
        "cmd":"/givegunlic [id] [price]",
        "descr":"Issue a license for weapons.",
        "category":["LSPD", "FIB"]
    },
    {
        "cmd":"/takegunlic [id] ",
        "descr":"Withdraw the license for weapons.",
        "category":["LSPD", "FIB"]
    },
    {
        "cmd":"/pull [id]",
        "descr":"Pull the player out of the car.",
        "category":["LSPD", "FIB", "Mafia", "Army", "Government"]
    },
    {
        "cmd":"/incar [id]",
        "descr":"Put a player in a car.",
        "category":["LSPD", "FIB", "Mafia", "Army", "Government"]
    },
    {
        "cmd":"/warg",
        "descr":"Turn on emergency mode.",
        "category":["LSPD", "FIB"]
    },
    {
        "cmd":"/openstock /closestock ",
        "descr":"Open/Close Warehouse. ",
        "category":["LSPD", "FIB", "EMS", "Army", "Government", "Gangs", "Mafia"]
    },
    {
        "cmd":"Pressing 'X'",
        "descr":"Put on handcuffs.",
        "category":["LSPD", "FIB", "Army", "Government"]
    },
    {
        "cmd":"Pressing 'Z'",
        "descr":"Lead the player.",
        "category":["LSPD", "FIB", "Army", "Government"]
    },
    {
        "cmd":"Pressing 'U'",
        "descr":"[While in a working car] Open the on-board computer.",
        "category":["LSPD", "FIB"]
    },
    {
        "cmd":"Pressing 'G'",
        "descr":"Interaction with the player (search, check documents, etc.)",
        "category":["LSPD", "FIB"]
    },
    {
        "cmd":"/heal [id] [price]",
        "descr":"Offer treatment for payment.",
        "category":["EMS"]
    },
    {
        "cmd":"/ems [id]",
        "descr":"Take the challenge.",
        "category":["EMS"]
    },
    {
        "cmd":"/givemedlic [id]",
        "descr":"Issue a medical license.",
        "category":["EMS"]
    },
    {
        "cmd":"/givepmlic [id] [price]",
        "descr":"Sell a paramedic's license.",
        "category":["EMS"]
    },
    {
        "cmd":"Pressing 'F7'",
        "descr":"Open Ad Editing",
        "category":"LSNEWS"
    },
    {
        "cmd":"/capture",
        "descr":"Join Territory War.",
        "category":"Gangs"
    },
    {
        "cmd":"Pressing 'G'",
        "descr":"Interaction with a player (rob, put on ties/bag, etc.)",
        "category":["Mafia", "Gangs"]
    },
    {
        "cmd":"/bizwar",
        "descr":"Join Business War.",
        "category":"Mafia"
    },
    {
        "cmd":"/takebiz",
        "descr":"Take the business under your control.",
        "category":"Mafia"
    },
    {
        "cmd":"/respawn",
        "descr":"Respawn of all fraction transport.",
        "category":"Leadership"
    },
    {
        "cmd":"/setrank [id] [rank]",
        "descr":"Change a player's rank.",
        "category":"Leadership"
    },
    {
        "cmd":"/invite [ID]",
        "descr":"Accept a player into a faction.",
        "category":"Leadership"
    },
    {
        "cmd":"/uninvite [ID]",
        "descr":"Dismiss a player from a faction.",
        "category":"Leadership"
    }
]