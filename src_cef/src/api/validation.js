let 
    text = "",
    textPattern = /^[a-zA-Z0-9]+$/,
    onlyTextPattern = /^[a-zA-Z]+$/,
    phoneTextPattern = /^[а-яА-Яa-zA-Z0-9]+$/,
    loginPattern = /^[a-zA-Z0-9]+$/,
    emailPattern = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/,
    textAdPattern = /^[0-9a-zA-Zа-яА-Яё_ @*()-=?«»[\]!#$%:;.,^]+$/;

export let validate = (validator, value) => {
    text = "";
    switch(validator) {
        case "login":
            if(value.length < 3)
                text = "Login cannot be less than 5 characters.";
            else if(value.length > 32)
                text = "Login can not be more than 32 characters.";
            else if(!textPattern.test(String(value).toLowerCase()))
                text = "Login can consist of letters of the Latin alphabet and numbers and special characters. ( - _ . ).";
            break;
        case "password":
            if(value.length < 5)
                text = "Password cannot be less than 5 characters.";
            else if(value.length > 32)
                text = "ПThe password cannot be more than 32 characters.";
            else if(!textPattern.test(String(value).toLowerCase()))
                text = "The password can consist of letters of the Latin alphabet and numbers.";
            break;
        case "email":
            if(value.length < 5)
                text = "Email cannot be less than 5 characters.";
            else if(value.length > 32)
                text = "Email cannot be longer than 32 characters.";
            else if(!emailPattern.test(String(value).toLowerCase()))
                text = "Email does not fit the format.";
            break;
        case "promocode":
            if(value.length < 5)
                text = "Promo code cannot be less than 5 characters.";
            else if(value.length > 16)
                text = "Promo code can not be more than 16 characters.";
            else if(!textPattern.test(String(value).toLowerCase()))
                text = "Пromocode does not match the format.";
            break;
        case "name":
            if(!value || value.length < 3)
                text = "The name cannot be less than 3 characters.";
            else if(value.length > 25)
                text = "The name cannot be more than 25 characters.";
            else if(!onlyTextPattern.test(String(value).toLowerCase()))
                text = "A character's name can only consist of letters of the Latin alphabet.";
            else {
                let firstSymbol = value[0];
                if(firstSymbol !== firstSymbol.toUpperCase()) {
                    text = "The first character must be uppercase. Allowed formats: Pavel, Michael";
                    break;
                }

                let upperCaseCount = 0; // Кол-во заглавных символов
                for (let i = 0; i != value.length; i++) {
                    let symbol = value[i];
                    if (symbol === symbol.toUpperCase()) upperCaseCount++;
                }

                if (upperCaseCount > 1) {
                    text = "There are more than 1 capital letters in the name. Allowed formats: Pavel, Michael";
                    break;
                }
            }
            break;
        case "surname":
            if(!value || value.length < 3)
                text = "Last name cannot be less than 3 characters.";
            else if(value.length > 25)
                text = "Last name cannot be more than 25 characters.";
            else if(!onlyTextPattern.test(String(value).toLowerCase()))
                text = "A character's last name can only consist of letters of the Latin alphabet.";
            else {
                let firstSymbol = value[0];
                if(firstSymbol !== firstSymbol.toUpperCase()) {
                    text = "The first character must be uppercase. Allowed formats: Best, Sokolyansky";
                    break;
                }

                let upperCaseCount = 0; // Кол-во заглавных символов
                for (let i = 0; i != value.length; i++) {
                    let symbol = value[i];
                    if (symbol === symbol.toUpperCase()) upperCaseCount++;
                }
                
                if (upperCaseCount > 2) { // Если больше 2х заглавных символов, то отказ. (На сервере по правилам разрешено иметь Фамилию, например McCry, то есть с приставками).
                    text = "There are more than two capital letters in the last name. Allowed formats: Best, Sokolyansky";
                    break;
                }
            }
            break;
        case "phonename":
            if(value.length < 1)
                text = "The name cannot be less than 4 characters.";
            else if(value.length > 16)
                text = "The name cannot be more than 16 characters.";
            else if(!textAdPattern.test(String(value).toLowerCase()))
                text = "Incorrect format.";
            break;
        case "phonenumber":
            value = Number(value);
            if(!value || value < 2)
                text = "The name cannot be less than 2 characters.";
            else if(value > 999999999)
                text = "The name cannot be more than 9 characters long.";
            break;
        case "textAd":
            if(value.length < 15)
                text = "Text cannot be less than 15 characters long.";
            else if(value.length > 150)
                text = "Text cannot exceed 150 characters.";
            else if(!textAdPattern.test(String(value).toLowerCase()))
                text = "ТThe text can consist of letters of the Latin alphabet and numbers and special symbols. ( - _ . ).";
            break;
        case "titleAd":
            if(value.length < 3)
                text = "The header cannot be less than 3 characters long.";
            else if(value.length > 20)
                text = "The title cannot be more than 20 characters long.";
            else if(!textAdPattern.test(String(value).toLowerCase()))
                text = "The title may consist of letters of the Latin alphabet and numbers and special symbols. ( - _ . ).";
            break;
        case "vehicleNumber":
            if(value.length < 2)
                text = "The number cannot be less than 2 characters.";
            else if(value.length > 8)
                text = "The number cannot be more than 8 characters long.";
            else if(!textPattern.test(String(value).toLowerCase()))
                text = "The number does not match the format.";
            break;

        case "discord":
            if (!value.includes("https://discord.gg/"))
                text = "Incorrect link format.";
            
            const discord = value.replace("https://discord.gg/", "")
            
            if(discord.length < 2)
                text = "The link cannot be less than 1 character long.";
            else if(discord.length > 10)
                text = "The link cannot be longer than 10 characters.";
            else if(!textPattern.test(String(discord).toLowerCase()))
                text = "The link can consist of letters of the Latin alphabet and numbers and special characters. ( - _ . ).";
            break;
    }
    return {
        valid: (text.length > 0 ? false : true),
        text: text
    };
}