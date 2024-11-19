<script>
    import './main.sass'
    import { translateText } from 'lang'
    import { onMount, onDestroy } from 'svelte'

    export let popupData;
    import router from "router";
    import { executeClient } from "api/rage";

    const onFreeMode = () => {
        executeClient ("camera.freemode")
    }

    const onClose = () => {
        executeClient ("camera.close")
    }

    const onScreen = () => {
        executeClient ("camera.screen")
    }

    const onKeyUp = (event) => {
        if (!$router.opacity)
            return;
        const { keyCode } = event;

        if (keyCode == 9)
            onFreeMode ()

        if (keyCode == 13)
            onScreen ()

        if (keyCode == 27)
            onClose ()
    }

    const updateCameraClose = (isScreen) => {
        if (typeof popupData === "function")
            popupData (isScreen);

        window.router.setPopUp()
    }

    let cameraData = {
        timeCycle: `Фильтр (1)`,
        animName: "",
        emotion: `Эмоция (1)`,
        isPhoneThisFrame: false,
        isFreeMode: false,
        isDofEnabled: true,
    };
    
    const onCameraUpdate = (json) => {
        cameraData = JSON.parse(json);
    }

    onMount(() => {
        window.events.addEvent("camera.close", updateCameraClose);
        window.events.addEvent("camera.update", onCameraUpdate);
    });

    onDestroy(() => {
        window.events.removeEvent("camera.close", updateCameraClose);
        window.events.removeEvent("camera.update", onCameraUpdate);
    });
</script>

<svelte:window on:keyup={onKeyUp} />

<div id="photo">
    <div class="photo__buttons">
        {#if !cameraData.isPhoneThisFrame}
        <div class="photo__bottom_buttons center">
            <div class="photo__bottom_button">TAB</div>
            <div>{!cameraData.isFreeMode ? 'Remove the camera' : 'Get the Camera'}</div>
        </div>
        {/if}
        <div class="box-flex">

            {#if !cameraData.isFreeMode}
                {#if cameraData.isPhoneThisFrame}
                <div class="photo__absolute_leftbottom">
                    <div class="photo__bottom_buttons center">
                        <div class="photo__bottom_button">ENTER</div>
                        <div>{translateText('popups', 'Photo')}</div>
                    </div>
                    <div class="photo__bottom_buttons center">
                        <div class="photo__bottom_button mr-5">W</div>
                        <div class="photo__bottom_button">S</div>
                        <div>{cameraData.timeCycle}</div>
                    </div>
                    <div class="photo__bottom_buttons center">
                        <div class="photo__bottom_button mr-5">A</div>
                        <div class="photo__bottom_button">D</div>
                        <div>{translateText('popups', 'Change of action')}</div>
                    </div>
                    <div class="photo__bottom_buttons center">
                        <div class="photo__bottom_button">SHIFT</div>
                        <div>{cameraData.animName}</div>
                    </div>
                    <div class="photo__bottom_buttons center">
                        <div class="photo__bottom_button mr-5">Q</div>
                        <div class="photo__bottom_button">E</div>
                        <div>{cameraData.emotion}</div>
                    </div>
                </div>
                <div class="photo__absolute_rightbottom">
                    <div class="photo__bottom_buttons center">
                        <div class="photo__bottom_button">&#x2191;</div>
                        <div>{translateText('popups', 'View')}</div>
                    </div>
                    <div class="photo__bottom_buttons center">
                        <div class="photo__bottom_button">V</div>
                        <div>{translateText('popups', 'Bokeh')}</div>
                    </div>
                    <div class="photo__bottom_buttons center">
                        <div class="photo__mouseleft"></div>
                        <div>{translateText('popups', 'Angle')}</div>
                    </div>
                    <div class="photo__bottom_buttons center">
                        <div class="photo__mouseright"></div>
                        <div>{translateText('popups', 'Profile')}</div>
                    </div>
                    <div class="photo__bottom_buttons esc">
                        <div class="photo__bottom_button">ESC</div>
                        <div>{translateText('popups', 'Back')}</div>
                    </div>
                </div>
                {:else}
                    <div class="photo__absolute_leftbottom">
                        <div class="photo__bottom_buttons center">
                            <div class="photo__bottom_button">ENTER</div>
                            <div>{translateText('popups', 'Photo')}</div>
                        </div>
                        <div class="photo__bottom_buttons center">
                            <div class="photo__bottom_button mr-5">W</div>
                            <div class="photo__bottom_button">S</div>
                            <div>{cameraData.timeCycle}</div>
                        </div>
                        <div class="photo__bottom_buttons center">
                            <div class="photo__mousemiddle"></div>
                            <div>{translateText('popups', 'Zoom')}</div>
                        </div>
                    </div>
                    <div class="photo__absolute_rightbottom">
                        {#if cameraData.isDofEnabled}
                        <div class="photo__bottom_buttons center">
                            <div class="photo__bottom_button">&#x2191;</div>
                            <div>{translateText('popups', 'View')}</div>
                        </div>
                        {/if}
                        <div class="photo__bottom_buttons esc">
                            <div class="photo__bottom_button">ESC</div>
                            <div>{translateText('popups', 'Back')}</div>
                        </div>
                    </div>
                {/if}
            {/if}
        </div>
    </div>
</div>