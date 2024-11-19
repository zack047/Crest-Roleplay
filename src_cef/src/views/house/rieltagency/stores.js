import { writable } from 'svelte/store';

export const currentHouseData = writable([
    {
        number: 219,
        class: "Premium +",
        price: 5000,
    },
    {
        number: 220,
        class: "Premium",
        price: 4500,
    },
    {
        number: 218,
        class: "Economy",
        price: 500,
    },
    {
        number: 217,
        class: "Standard",
        price: 2500,
    },
]);

export const currentBusinessData = writable([
    {
        number: 219,
        class: "Premium +",
        price: 5000,
    },
    {
        number: 220,
        class: "Premium",
        price: 4500,
    },
    {
        number: 218,
        class: "Economy",
        price: 500,
    },
    {
        number: 217,
        class: "Standard",
        price: 2500,
    },
    
    {
        number: 217,
        class: "Standard",
        price: 2500,
    },
]);