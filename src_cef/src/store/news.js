
import { writable } from 'svelte/store';


export const newsData = writable([]);


newsData.update((newsList) => {
  const newNews = {
    image_s3: 'https://media.discordapp.net/attachments/1092512256765464636/1118994686389014619/Screenshot_1.png?width=1252&height=704',
    description: 'you can do the best roleplay here (no cap)'
  };
  return [...newsList, newNews];
});
