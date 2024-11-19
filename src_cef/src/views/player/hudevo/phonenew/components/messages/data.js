export const smilesList = [
    "😀",
    "😁",
    "😂",
    "😃",
    "😄",
    "😅",
    "😆",
    "😇",
    "😈",
    "😉",
    "😊",
    "😋",
    "😌",
    "😍",
    "😎",
    "😏",
    "😐",
    "😑",
    "😒",
    "😓",
    "😔",
    "😕",
    "😖",
    "😗",
    "😘",
    "😙",
    "😚",
    "😛",
    "😜",
    "😝",
    "😞",
    "😟",
    "😠",
    "😡",
    "😢",
    "😣",
    "😤",
    "😥",
    "😦",
    "😧",
    "😨",
    "😩",
    "😪",
    "😫",
    "😬",
    "😭",
    "😮",
    "😯",
    "😰",
    "😱",
    "😲",
    "😳",
    "😴",
    "😵",
    "😶",
    "😷",
    "🙁",
    "🙂",
    "🙃",
    "🙄",
    "🤐",
    "🤑",
    "🤒",
    "🤓",
    "🤔",
    "🤕",
    "🤠",
    "🤡",
    "🤢",
    "🤣",
    "🤤",
    "🤥",
    "🤧",
    "🤨",
    "🤩",
    "🤪",
    "🤫",
    "🤬",
    "🤭",
    "🤮",
    "🤯",
    "🧐"
]


export const messageType = {
    text: 0,
    map: 1,
    img: 2,
    sticker: 3
}

export const messageStatus = {
    sent: 0,
    received: 1,
    error: 2
}

export const chatStatusName = [
    "",
    "Online",
    "Was on the network recently",
    "Prints .."
]

import { format } from "api/formatter";


import { getListData } from '@/views/player/hudevo/phonenew/components/messages/emoji/data';


const emojiPattern = /\:([a-zA-Z])\w+\:/g;

export const formatMessage = (text) => {
    if (text) {
        text = format("parse", text);

        let emojiList = text.match(emojiPattern);

        if (emojiList && typeof emojiList === "object" && emojiList.length) {
            emojiList.forEach((emoji) => {
                const emojiData = getListData(emoji);

                if (emojiData)
                    text = text.replace(emoji, emojiData.html);
            })
        }

        return text;
    }
}

export const getMessageUniqueKey = () => {
    const key = Math.floor(Math.random() * (100000 - 0 + 1)) + 1;
    return key;
}

export const inputMaxLength = 140;