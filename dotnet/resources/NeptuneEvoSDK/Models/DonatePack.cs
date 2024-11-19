using System.Collections.Generic;

namespace Redage.SDK.Models
{
    public class DonatePack
    {
        /// <summary>
        /// Множетель доната
        /// </summary>
        public int[] PriceRB = new int[] 
        {
            499,
            999,
            14999,
            4999,
            7999,
            8999,
            9999,
            17999,
            19999,
            29999,
            49999
        };
        /// <summary>
        /// Цены донат пакетов
        /// </summary>
        public int[] GiveMoney = new int[]
        {
            12500,
            25000,
            60000,
            120000,
            80000,
            200000,
            275000,
            125000,
            450000,
            450000,
            500000
        };

        public List<string>[] List =
        {
            new List<string>()
            {
                "12.500$",
                "Standard case", 
                "VIP Diamond for 5 days", 
                "10 exp (experience)", 
                "Motor vehicle license.",
                "License for Passenger Vehicles",
                "Working Axe",
                "Reinforced Pickaxe",
                "Vape",
            },
            new List<string>()
            {
                "25.000$",
                "Strange Case",
                "VIP Diamond for 10 days",
                "10 exp (experience)",
                "Truck Vehicle License",
                "License for passenger vehicles",
                "Radio",
                "Bong",
                "Seminole Car",
            },
            new List<string>()
            {
                "60.000$",
                "Special Case",
                "VIP Diamond for 15 days",
                "15 exp (experience)",
                "Motor Vehicle License",
                "License for cars and trucks",
                "Medical card",
                "Hookah",
                "Baller Car",
            },
            new List<string>()
            {
                "120000$",
                "Rare Case",
                "30 days Diamond VIP",
                "20 exp (experience)",
                "Medcard",
                "Helicopter License",
                "License for airplanes",
                "Weapons License",
                "Professional pickaxe",
            },
            new List<string>()
            {
                "{0}$",
                "Машина Komoda",
                "30 дней Diamond VIP",
                "35 exp (опыта)",
            },
            new List<string>()
            {
                "{0}$",
                "45 days Diamond VIP",
                "30 exp (experience)",
                "License for helicopters and planes",
            },
            new List<string>()
            {
                "{0}$",
                "Livid Case",
                "30 days Diamond VIP",
                "15 exp (experience)",
                "Medcard",
                "Helicopter License",
                "License for planes",
                "Schafter2 Car",
            },
            new List<string>()
            {
                "{0}$",
                "Pariah Machine",
                "60 days Diamond VIP",
                "45 exp (experience)",
            },
            new List<string>()
            {
                "{0}$",
                "Legendary Case",
                "45 days Diamond VIP",
                "30 exp (experience)",
                "Random six-digit phone number",
                "Helicopter and airplane license",
                "GUCCI bag",
                "Gun and Paramedic License",
                "Legendary Car - Carbonizzare",
            },
            new List<string>()
            {
                "{0}$",
                "Car Neon",
                "120 days Diamond VIP",
                "60 exp (experience)",
                "Random five-digit phone number",
            },
            new List<string>()
            {
                "{0}$",
                "90 days Diamond VIP",
                "60 exp (experience)",
                "Random five-digit phone number",
                "Helicopter and airplane license",
                "Unique Accessory - Beard",
                "Car - Tesla",
                "Gun License and Medical Card",
                "Paramedic License",
            },
        };
    }
}