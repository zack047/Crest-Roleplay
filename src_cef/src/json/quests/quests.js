
import npc_tracyJson from './ValentineDay/npc_tracy.json';
import npc_doctorJson from './ValentineDay/npc_doctor.json';
import npc_grannyJson from './ValentineDay/npc_granny.json';


import npc_fd_dadaJson from './defenderFatherlandDay/npc_dada.json';
import npc_fd_edwardJson from './defenderFatherlandDay/npc_pavel.json';
import npc_fd_zakJson from './defenderFatherlandDay/npc_zak.json';

import npc_airdrop from './npc_airdrop.json';
import npc_oressale from './npc_oressale.json';
import npc_fracpolic from './fraction/npc_fracpolic.json';
import npc_fracsheriff from './fraction/npc_fracsheriff.json';
import npc_fracnews from './fraction/npc_fracnews.json';
import npc_fracems from './fraction/npc_fracems.json';
import npc_premium from './biz/npc_premium.json';
import npc_huntingshop from './npc_huntingshop.json';
import npc_treessell from './npc_treessell.json';

import npc_stock from './npc_stock.json';

import npc_donateautoroom from './npc_donateautoroom.json';
import npc_cityhall from './npc_cityhall.json';
import npc_wedding from './npc_wedding.json';

import npc_pet from './biz/npc_pet.json';
import npc_petshop from './biz/npc_petshop.json';
import npc_rieltor from './biz/npc_rieltor.json';
import npc_furniture from './biz/npc_furniture.json';
import npc_carevac from './npc_carevac.json';
import npc_airshop from './npc_airshop.json';
import npc_eliteroom from './npc_eliteroom.json';

import npc_zdobich from './npc_zdobich.json';

import npc_automechanic from './work/npc_automechanic.json';
import npc_bus from './work/npc_bus.json';
import npc_collector from './work/npc_collector.json';
import npc_electrician from './work/npc_electrician.json';
import npc_gopostal from './work/npc_gopostal.json';
import npc_lawnmower from './work/npc_lawnmower.json';
import npc_taxi from './work/npc_taxi.json';
import npc_truckers from './work/npc_truckers.json';
import npc_org from './npc_org.json';
import npc_birthday from './npc_birthday.json';

/* type
    quest - The usual quest that is only in the menu
    lists - A simple list with quests I don't remember what for
    talk - quest conversation
*/
const list = {
    npc_tracy: npc_tracyJson,
    npc_doctor: npc_doctorJson,
    npc_granny: npc_grannyJson,

    npc_fd_dada: npc_fd_dadaJson,
    npc_fd_edward: npc_fd_edwardJson,
    npc_fd_zak: npc_fd_zakJson,

    npc_airdrop: npc_airdrop,
    npc_oressale: npc_oressale,
    npc_fracpolic: npc_fracpolic,
    npc_fracsheriff: npc_fracsheriff,
    npc_fracnews: npc_fracnews,
    npc_fracems: npc_fracems,
    
    npc_premium: npc_premium,

    npc_stock: npc_stock,
    npc_huntingshop: npc_huntingshop,
    npc_treessell: npc_treessell,

    npc_donateautoroom: npc_donateautoroom,
    npc_cityhall: npc_cityhall,
    npc_wedding: npc_wedding,

    npc_pet: npc_pet,
    npc_petshop: npc_petshop,
    npc_furniture: npc_furniture,
    npc_rieltor: npc_rieltor,
    npc_carevac: npc_carevac,
    npc_airshop: npc_airshop,
    npc_eliteroom: npc_eliteroom,

    npc_zdobich: npc_zdobich,

    npc_automechanic: npc_automechanic,
    npc_bus: npc_bus,
    npc_collector: npc_collector,
    npc_electrician: npc_electrician,
    npc_gopostal: npc_gopostal,
    npc_lawnmower: npc_lawnmower,
    npc_taxi: npc_taxi,
    npc_truckers: npc_truckers,
    npc_org: npc_org,
    npc_birthday: npc_birthday,
}

const actorData = {
    npc_tracy: {
        name: "Tracy"
    },
    npc_doctor: {
        name: "Dr. Schultz"
    },
    npc_granny: {
        name: "Granny"
    },

    npc_fd_dada: {
        name: "Uncle"
    },
    npc_fd_edward: {
        name: "Edward"
    },
    npc_fd_zak: {
        name: "Zack Zuckerberg"
    },
    npc_airdrop: {
        name: "Juan de Cartel"
    },
    npc_oressale: {
        name: "Mark"
    },
    npc_fracpolic: {
        name: "Police Officer"
    },
    npc_fracsheriff: {
        name: "Sheriff"
    },
    npc_fracnews: {
        name: "Jennifer"
    },
    npc_fracems: {
        name: "Emmanuel"
    },
    npc_premium: {
        name: "Vovchik"
    },
    npc_stock: {
        name: "Alexander"
    },
    npc_huntingshop: {
        name: "Bear Grylls"
    },
    npc_treessell: {
        name: "Dmitry"
    },
    npc_donateautoroom: {
        name: "Donata Redbaksovna"
    },
    npc_cityhall: {
        name: "Elnara Karimova"
    },
    npc_wedding: {
        name: "Father Michael"
    },
    npc_pet: {
        name: "Veterinarian Mikhail"
    },
    npc_petshop: {
        name: "Pet Seller"
    },
    npc_zdobich: {
        name: "Vitaly Dobich"
    },
    npc_rieltor: {
        name: "Elon Tusk"
    },
    npc_furniture: {
        name: "Ivan"
    },
    npc_carevac: {
        name: "Robert"
    },
    npc_airshop: {
        name: "Air Transport Seller"
    },
    npc_eliteroom: {
        name: "Seller of Luxury Transports"
    },
    npc_automechanic: {
        name: "Chief Mechanic"
    },
    npc_bus: {
        name: "Convoy Commander"
    },
    npc_collector: {
        name: "Banking HR"
    },
    npc_electrician: {
        name: "Senior Electrician"
    },
    npc_gopostal: {
        name: "Senior Postman"
    },
    npc_lawnmower: {
        name: "Chief Lawn Mower"
    },
    npc_taxi: {
        name: "Taxi Company Director"
    },
    npc_truckers: {
        name: "Trucker Driver"
    },
    npc_org: {
        name: "Director Polly"
    },
    npc_birthday: {
        name: "Festive Monkey"
    },
}

export const getQuests = () => {
    return questsnpc_tailerJsonJson;
}

export const getQuest = (name, questId) => {
    return list[name][questId];
}

export const getActors = (name) => {
    return actorData[name];
}