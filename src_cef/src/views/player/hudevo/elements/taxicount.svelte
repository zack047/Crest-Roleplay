<script>
    import { translateText } from 'lang'
    import { format } from "api/formatter";

    import { executeClientAsync} from 'api/rage'

    let selectTaxi = {}

    const getCounter = () => {
        executeClientAsync("phone.taxi.getCounter").then((result) => {
            if (result && typeof result === "string") {
                selectTaxi = JSON.parse(result);
            }
        });
    }

    getCounter();
    import { addListernEvent } from 'api/functions';
    addListernEvent ("hud.taxi.updateCounter", getCounter);


</script>
{#if selectTaxi}
    <div class="haxzertaxi">
        <p>{translateText('player2', 'Счётчик включен')}</p>
        <b>${format("money", selectTaxi.price)}</b>
    </div>
{/if}