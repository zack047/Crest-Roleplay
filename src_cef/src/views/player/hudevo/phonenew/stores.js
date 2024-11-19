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
        name: "State. structures",
        icon: "gos",
        content: [
            "City Hall",
            "LSPD",
            "EMS",
            "FIB",
            "NEWS",
            "Control center",
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
            "Russian Mafia",
            "Yakuza",
            "Armenian mafia",
        ]
    },
    {
        name: "Work",
        icon: "licenses",
        content: [
            "Power station",
            "Post office",
            "Taxopark",
            "Bus depot",
            "Parking of lawn mowers",
            "Parking of truckers",
            "Parking of the collectors",
            "Parking of auto mechanics",
        ]
    },
    {
        name: "Part-time job",
        icon: "jobs",
        content: [
            "Civil Mine 1",
            "Civil mine 2",
            "Civil Mine 3",
            "Civil Mine 4",
            "State mine",
            "Limella 1",
            "Lesruk 2",
            "Lesruk 3",
            "Lesruk 4",
            "Lesruk 5",
        ]
    },
    {
        name: "Nearest places",
        icon: "recent",
        content: [

            "Next to the rental of motorcycles",
            "Next lease of a bicycle",
            "Nearest rental of a boat",
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
            "Families",
            "Arena",
            "Amphitheater",
            "Humane Labs",
            "Lighthouse",
            "Hunting store",
            "Main market",
            "Black market",
            "Church",
            "Seller of the pets",
            "Court",
        ]
    }
])