<script>

let chat = {
  history: [
    {
        type: "OOC",
        id: 69,
        name: "Romario Richardson:",
        text: "Привет, мир, это я, ваш Король!"
    },
    {
        type: "OOC",
        id: 69,
        name: "Romario Richardson:",
        text: "фу, вы очень некультурный человек, раз позволяете себе пукать в газетки"
    },
    {
        type: "ME",
        id: 69,
        name: "Romario Richardson:",
        text: "Что-то сделал непонятное"
    },
    {
        type: "OOC",
        id: 69,
        name: "Romario Richardson:",
        text: "Привет, мир, это я, ваш Король!"
    },
    {
        type: "DO",
        id: 69,
        name: "(( Romario Richardson ))",
        text: "Воняет."
    }
  ]
}

import { translateText } from 'lang'
  import moment from 'moment';
  import { executeClient, invokeMethod } from 'api/rage'

  import { format } from 'api/formatter'

  import { isInputToggled, isHelp } from 'store/hud'
  export let SafeSone;
    //
  //
  let selectChat = 0;
  const ChatTegs = [
  {
  name: "IC",
  chat: false,
  visible: true
  },
  {
  name: "OOC",
  chat: "b",
  visible: true
  },
  {
  name: "ME",
  chat: "me",
  visible: true
  },
  {
  name: "DO",
  chat: "do",
  visible: true
  },
  {
  name: "TRY",
  chat: "try",
  visible: true
  },
  /* 
  {
  name: "R",
  chat: "r",
  visible: false
  },
  {
  name: "F",
  chat: "f",
  visible: false
  },
  {
  name: "DEP",
  chat: "dep",
  visible: false
  },
  {
  name: "GOV",
  chat: "gov",
  visible: false
  },*/
  ]

  // Input Element
  let TextInput;

  // Message Pool
  let Messages = [];

  // Current value of the input
  let InputValue = "";

  // Save message from the input if the chat is closed via Esc
  let savedInput = "";

  // Current buffer index
  let bufferState = -1;
  // Current buffer value
  let bufferCurrent = "";
  // Buffer of sent messages
  let buffer = [];

  // Chat Size
  let pagesize = 10;

  // We update the states of the rage, that we have an open input
  //$: invokeMethod("setTypingInChatState", $isInputToggled);

  // API чата
  let timerId = 0;
  let noActive = false;
  const AddChatMessage = (text) => {
  text = format("parse", text);

  if (Messages.length > 50) {
  Messages.pop();
  }
  let generatedMessage = getColorizedMessagePart(text);
  Messages = [{time: moment().format('HH:mm:ss'), parts: generatedMessage}, ...Messages];

  onActive ();
  };

  const onActive = () => {
  if ($isInputToggled)
  return;
  noActive = false;

  if (timerId)
  clearTimeout (timerId);

  timerId = setTimeout(ClearData, 15000);
  }

  const ClearData = () => {
  timerId = 0;
  noActive = true;
  }

  const parseMessage = (text) => {
  const re = new RegExp('!{((#[0-9a-f]{3})|(#[0-9a-f]{6})|([0-9a-f]{3})|([0-9a-f]{6})|(\\w+))}', 'i');
  const match = re.exec(text);

  if (!match) {
  return null;
  }

  let color = match[1];

  color = color ? `${color.startsWith('#')
  ? color
  : /(^[0-9A-Fa-f]{6}$)|(^[0-9A-Fa-f]{3}$)/i.test(color) ? '#' + color : color}` : null

  return {
  color,
  index: match.index,
  remainingText: text.slice(match.index + match[0].length)
  };
  }

  const getColorizedMessagePart = (text) => {
  const parts = [];
  let prevColor = null;
  let currentText = text;
  let message;
  let attempts = 0;

  for (; (message = parseMessage(currentText)) || !attempts;) {
  const prevText = currentText.slice(0, message ? message.index : currentText.length);

  if (prevText.length) {
  parts.push({
  color: prevColor,
  text: prevText.replace(/\\!{/g, '!{').replace(/\\}/g, '}')
  });
  }

  if (message) {
  prevColor = message.color;
  currentText = message.remainingText;
  } else {
  attempts++;
  }
  }
  return parts;
  }

  const ToggleChatInput = (state, clearchat = false, updatelastbuffer = false, event = false) => {
  if(state) {
  noActive = false;

  if (timerId)
  clearTimeout (timerId);

  window.hudStore.isInputToggled (true);
  timerId = 0;

  setTimeout(() => {
  if(TextInput) {
  TextInput.focus();

  if(bufferState === -1) {
  InputValue = savedInput;
  SetInputFocused();
  } else {
  if(buffer[bufferState] !== undefined) {
  InputValue = buffer[bufferState];
  } else {
  InputValue = "";
  }
  SetInputFocused();
  }
  if (event) executeClient ("client:OnChatInputChanged", true);
  }
  }, 0);
  } else {
  bufferCurrent = clearchat ? "" : (updatelastbuffer ? InputValue : bufferCurrent),
  bufferState = clearchat ? -1 : bufferState,
  savedInput = clearchat ? "" : InputValue;
  window.hudStore.isInputToggled (false);
  //selectChat = 0;
  onActive ();
  InputValue = "";

  if (event) executeClient ("client:OnChatInputChanged", false);
  }
  };

  const OnSubmitMessage = () => {
  bufferState = -1;
  bufferCurrent = "";

  if(InputValue) {
  if(buffer.length > 50) buffer.shift();

  // We do not save consecutive identical messages.
  if(buffer[buffer.length - 1] !== InputValue) buffer.push(InputValue);

  if(InputValue[0] === '/') {
  let commandText = InputValue;
  commandText = commandText.trim().substr(1);
  if (commandText.length > 0)
  {
  let params = commandText.split(' ');
  let command = params[0];
  params.shift();
  switch(command)
  {
  case "widthsize":
  case "pagesize":
  case "fontsize":
  case "timestamp":
  case "chatalpha":
  window.notificationAdd(4, 9, translateText('player2', 'This function has been moved to settings. You can find them by going to the inventory > settings tab.'), 3000);
  break;
  case "weatherinfo":
  executeClient ("weatherinfo");
  break;
  case "restartopen":
  executeClient ("restart.open");
  break;
  case "greenzone":
  executeClient ("greenzone");
  break;
  case "cltest":
  executeClient ("test.test");
  break;
  default:
  commandText = format("parseDell", commandText);

  if (commandText.length > 0)
  invokeMethod("command", commandText);
  break;
  }
  }
  } else {
  let message = format("stringify", InputValue);

  if (message.length > 0) {
  if (selectChat === 0) {
  invokeMethod("chatMessage", message);
  } else {
  const commandText = `${ChatTegs [selectChat].chat} ${message}`

  if (commandText.length > 0)
  invokeMethod("command", commandText);

  }
  }

  }
  }
  ToggleChatInput(false, true, true, true);
  };

  const SetInputFocused = () => {
  if(TextInput) {
  TextInput.focus();
  TextInput.selectionStart = InputValue.length;
  }
  }

  const handleKeyUp = (e) => {
  if($isInputToggled) {
  switch(e.keyCode) {
  case 13:
  OnSubmitMessage();
  break;
  }
  }
  }

  const handleKeyDown = (event) => {
  if($isInputToggled) {
  switch(event.keyCode) {
  case 9: // Shutting down TAB
  event.preventDefault();

  if (++selectChat >= ChatTegs.length)
  selectChat = 0;
  break;
  case 38: // Raising the buffer up
  event.preventDefault();
  if(bufferState === -1) {
  bufferState = (buffer.length - 1);
  bufferCurrent = InputValue;
  }
  else if(bufferState > 0) {
  bufferState = bufferState - 1;
  SetInputFocused();
  } else {
  SetInputFocused();
  break;
  }

  if(buffer[bufferState]) {
  InputValue = buffer[bufferState];
  SetInputFocused();
  }
  break;
  case 40: // Lowering the buffer down
  event.preventDefault();
  if(bufferState === -1) break;
  if(bufferState < buffer.length - 1) {
                        bufferState = bufferState + 1;
                        SetInputFocused();
                    }
                    else {
                        InputValue = bufferCurrent;
                        bufferState = -1;
                        bufferCurrent = "";
                        SetInputFocused();
                        break;
                    }

                    if(buffer[bufferState] !== undefined) {
                        InputValue = buffer[bufferState];
                        SetInputFocused();
                    }
                    break;
                default:
                    break;
            }
        }
    };

    let Alpha = 100;
    let Transition = 0;

    const SetChatAlpha = (alpha, transition) => {
        Alpha = alpha;
        Transition = transition;
    }

    const GetChatAlpha = () => {
        return Alpha;
    }


    let TimeStamp = true;

    const ToggleTimeStamp = (toggle) => {
        TimeStamp = toggle;
    }

    const GetTimeStamp = () => {
        return TimeStamp;
    }

    let PageSize = 10;
    const UpdatePageSize = (pageSize) => {
        PageSize = pageSize;
    }

    let WidthSize = 50;
    const UpdateWidthSize = (widthSize) => {
        WidthSize = widthSize;
    }

    let FontSize = 16;
    const UpdateFontSize = (fontSize) => {
        FontSize = fontSize;
        UpdateMessageHeight(fontSize);
    }

    let MessageHeight = 0;
    const UpdateMessageHeight = (messageHeight) => {
        MessageHeight = messageHeight * 1.25;
    }
    UpdateMessageHeight (FontSize);

    let chatShadow = false;
    window.chat = {
        updateConfig: (config) => {
            config = JSON.parse(config);

            ToggleTimeStamp (config["Timestamp"]);
            UpdatePageSize (config["Pagesize"]);
            SetChatAlpha (config["ChatOpacity"], config["Transition"]);
            UpdateFontSize (config["Fontsize"]);
            UpdateWidthSize (config["Widthsize"]);
            chatShadow = config["ChatShadow"];
        },
        toggleInput: (toggled, clearchat = false, updatelastbuffer = false, event = false) => {
            ToggleChatInput (toggled, clearchat, updatelastbuffer, event)
        },
        addMessage: (message, clearhtml = true) => {
            AddChatMessage (message);
        }
    }
    if (window.mp && window.mp.events) {

        window.mp.events.add("chat:push", (message) => {
            AddChatMessage(message);
        });
    }

    let heightChat = 0;


    let messageElement;

    /*$: if (messageElement) {
        console.log(messageElement)
    }*/
</script>
<svelte:window on:keyup={handleKeyUp} on:keydown={handleKeyDown}/>
<div class="hud__chat">
  <div class="hud__chat_history">
    {#each Messages as msgs, index}
      {#each msgs.parts as msgpart}
          <h4>
            <div class="text">
              <h2>{@html msgpart.text}</h2>
            </div>
            </h4>
      {/each}
      
    {/each}
  </div>
  {#if $isInputToggled}
  <div class="hud__chat_inputbox">
      <div class="hud__chat_inputbox-input">
          <span>{ChatTegs[selectChat].name}</span>
          <input bind:this={TextInput} spellCheck={false} maxLength={144} placeholder="Enter a message" bind:value={InputValue} type="text">
          <button on:click="{OnSubmitMessage}"><img src="./assets/img/icons/send.svg" alt=""></button>
      </div>
      <div class="hud__chat_inputbox-modes">
        {#each ChatTegs as tag, index}
            {#if tag.visible}
              <button class:active={index === selectChat} on:click="{() => selectChat = index}">
                {tag.name}
              </button>
            {/if}
        {/each}
      </div>
  </div>
  {/if}
</div>