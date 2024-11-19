import { writable } from 'svelte/store';
import {executeClientToGroup} from "api/rage";

export const isMapLoad = writable(false);

export const isSim = writable(false);

export const selectedImage = writable(false);
export const selectedImageFunc = writable(false);
export const radioState = writable(false);
export const radioStation = writable(0);


let pageArray = [];

export const currentPage = writable("mainmenu");

currentPage.subscribe(page => {
    if (page === "mainmenu") {
        pageArray = [];
        executeClientToGroup ("finger", 1)
    } else if (page !== "callView") {
        pageArray.push(page)
        executeClientToGroup ("finger", 5)
    }
});


export const pageBack = () => {

    let page = "mainmenu";
    const lastIndex = pageArray.length - 1;

    if (typeof pageArray [lastIndex] === "string") {
        page = pageArray [lastIndex];
        pageArray.splice(lastIndex, 1);
    }

    if (page !== "call")
        selectNumber.set(null);

    currentPage.set (page);
}


export const selectNumber = writable(null);




export const currentWeather = writable("thunder");


export const categoriesList = writable([
    {
        name: "Government agencies",
        icon: "gos",
        content: [
            "City Hall",
            "LSPD",
            "EMS",
            "FIB",
            "NEWS",
            "Kontrollzentrum",
            "SHERIFF 1",
            "SHERIFF 2",
        ]
    },
    {
        name: "Gangs",
        icon: "weapons",
        content: [
            "Marabunta Grande",
            "Vagos",
            "Ballas",
            "The Families",
            "Bloods Street",
        ]
    },
    {
        name: "Mafia",
        icon: "mafia",
        content: [
            "La Cosa Nostra",
            "Russische Mafia",
            "Yakuza",
            "Armenische Mafia",
        ]
    },
    {
        name: "Work",
        icon: "licenses",
        content: [
            "Power plant",
            "Postamt",
            "Cab Depot",
            "Bus depot",
            "Lawn mower",
            "Trucker",
            "Collection agency",
            "Car mechanic",
        ]
    },
    {
        name: "Part-time",
        icon: "jobs",
        content: [
            "Civil mine 1",
            "Civil mine 2",
            "Civil mine 3",
            "Civil mine 4",
            "State mine",
            "Lumberjack 1",
            "Lumberjack 2",
            "Lumberjack 3",
            "Lumberjack 4",
            "Lumberjack 5",
        ]
    },
    {
        name: "Places near",
        icon: "recent",
        content: [

            "Nearest bike rental",
            "Nearest bike rental",
            "nearest boat rental",
           /* "Ближайшая аренда самолета",
            "Ближайшая аренда вертолета",*/
        ]
    },
    {
        name: "Other",
        icon: "clubs",
        content: [
            "Driving school",
            "Casino",
            "families",
            "Arena",
            "Amphitheater",
            "Human Labs",
            "The lighthouse",
            "The hunting store",
            "Main market",
            "Black market",
            "Church",
            "Animal Trader",
            "Court",
        ]
    }
])