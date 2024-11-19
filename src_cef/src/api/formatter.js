function replaceAll(string, search, replace) {
    return string.split(search).join(replace);
}

const newRankPattern = /^[а-яА-Яa-zA-Z0-9_\-.\s]+$/;
const textPattern = /[^0-9a-zA-Zа-яА-Яё_ @*()-=?«»"[\]!#$%:;.,^"'\s\d]/g;

export let format = (name, value) =>
{
    try {
        let text = "";
        switch(name) {
            case "rank":
                if(value.length < 2)
                    text = "Rank cannot be less than 2 characters.";
                else if(value.length > 25)
                    text = "Rank cannot be more than 25 characters.";
                else if(!newRankPattern.test(String(value).toLowerCase()))
                    text = "The rank may consist of letters of the Latin alphabet and numbers and specials.symbols ( - _ . ).";
                break;
            case "tag":
                if(value.length < 2)
                    text = "The tag cannot be less than 2 characters.";
                else if(value.length > 5)
                    text = "Tag cannot be more than 5 characters.";
                else if(!newRankPattern.test(String(value).toLowerCase()))
                    text = "The tag can consist of letters of the Latin alphabet and numbers and special characters.symbols ( - _ . ).";
                break;
            case "createOrg":
                if(value.length < 3)
                    text = "Organization name cannot be less than 3 characters.";
                else if(value.length > 30)
                    text = "Organization name cannot be more than 30 characters.";
                else if(!newRankPattern.test(String(value).toLowerCase()))
                    text = "The name of the organization may consist of letters of the Latin alphabet and numbers and special characters.symbols( - _ . ).";
                break;
            case "name":
                if(value.length < 2)
                    text = "The field cannot be less than 2 characters.";
                else if(value.length > 32)
                    text = "The field cannot be more than 32 characters.";
                else if(!newRankPattern.test(String(value).toLowerCase()))
                    text = "The field can consist of letters of the Latin alphabet and numbers and special characters.symbols ( - _ . ).";
                break;
            case "text":
                if(value.length < 2)
                    text = "The text cannot be less than 2 characters.";
                else if(value.length > 185)
                    text = "Text cannot be more than 185 characters.";
                else if(!newRankPattern.test(String(value).toLowerCase()))
                    text = "The text can consist of letters of the Latin alphabet and numbers and special characters.symbols ( - _ . ).";
                break;
            case "call":
                if(value.length < 2)
                    text = "The callsign cannot be less than 2 characters.";
                else if(value.length > 6)
                    text = "The callsign cannot be more than 6 characters.";
                else if(!newRankPattern.test(String(value).toLowerCase()))
                    text = "The call sign may consist of letters of the Latin alphabet and numbers and special.symbols ( - _ . ).";
                break;
            case "money":
                if (!value) return value;
                // Форматирование денег 1.000.000.000
                value = value.toString().replace(/\D/,'');
                return value.toString().replace(/(\d)(?=(\d\d\d)+([^\d]|$))/g, '$1.');
            case "materials":
                if (!value) return value;
                // Форматирование материалов в гараже фракции 14 000                
                value = value.toString().replace(/\D/,'');
                return value.toString().replace(/(\d)(?=(\d\d\d)+([^\d]|$))/g, '$1.');
            case "stringify":                
                if (!value) return value;
                const entityServerMap = {
                    '&amp;': '$1$',
                    '&lt;': '$2$',
                    '&gt;': '$3$',
                    '&quot;': '$4$',
                    "&#39;": '$5$',
                    '&#x2F;': '$6$',
                    '&#x60;': '$7$',
                    '&#x3D;': '$8$',
                    '&': '$1$',
                    '<': '$2$',
                    '>': '$3$',
                    '"': '$4$',
                    "'": '$5$',
                    '/': '$6$',
                    '`': '$7$',
                    '=': '$8$',
                };
                for(let key in entityServerMap) {
                    value = replaceAll (value, key, entityServerMap [key])
                }
                return value;
                //return String(value).replace(/[&<>"'`=\/]/g, function (s) {
                //    return entityServerMap[s];
                //});
            case "parse":
                if (!value) return value;
                const entityClientMap = {
                    '$1$': '&amp;',
                    '$2$': '&lt;',
                    '$3$': '&gt;',
                    '$4$': '&quot;',
                    '$5$': '&#39;',
                    '$6$': '&#x2F;',
                    '$7$': '&#x60;',
                    '$8$': '&#x3D;'
                };
                for(let key in entityClientMap) {
                    value = replaceAll (value, key, entityClientMap [key])
                }
                return value;
            case "parseDell":
                if (!value) return value;
                const entityClientMapDell = {
                    '&amp;': '',
                    '&lt;': '',
                    '&gt;': '',
                    '&quot;': '',
                    "&#39;": '',
                    '&#x2F;': '',
                    '&#x60;': '',
                    '&#x3D;': '',
                    '&nbsp;': '',
                    '&': '',
                    '<': '',
                    '>': '',
                    '"': '',
                    "'": '',
                    '/': '',
                    '`': '',
                    '=': '',
                };
                for(let key in entityClientMapDell) {
                    value = replaceAll (value, key, entityClientMapDell [key])
                }
                return value;
            case "textAd":
                if(value.length < 15)
                    text = "Text cannot be less than 15 characters.";
                else if(value.length > 150)
                    text = "The text cannot be more than 150 characters.";
                else if(!textPattern.test(String(value).toLowerCase()))
                    text = "The text can consist of letters of the Latin alphabet and numbers and special characters.characters ( - _ . ).";
                break;
        }
        return {valid: (text.length > 0 ? false : true), text: text};
    } catch(e) {
        console.log(e);
        return 0;
    }
}