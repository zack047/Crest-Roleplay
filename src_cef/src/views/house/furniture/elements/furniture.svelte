<script>
    let furnitureData = [
        {
            name: "Signalling",
            price: "500000"
        },
        {
            name: "Tables",
        },
        {
            name: "Chairs",
        },
        {
            name: "Pictures",
        },
    ]
</script>
{#if false}
    <div class="box-column">
        <div class="house__main_menu">
            {#each furnitureData as furniture, index}
                <div class="house__element">
                    {#if furniture.price}
                    <div class="box-between">
                        <div>{furniture.name}</div>
                        <div class="green">${furniture.price}</div>
                    </div>
                    {:else}
                        <div class="box-center" style="width:100%">
                            <div>{furniture.name}</div>
                        </div>
                    {/if}
            
                </div>
            {/each}
        </div>
        <div class="house__info small m-0 margin-none">
            List of available home upgrades
        </div>
    </div>
{/if}
{#if true}
    <div class="house__furniture">
        <div class="house__furniture_element">
            <div class="box-between">
                <div class="box-column">
                    <div class="house__furniture_title">Lamp</div>
                    <div class="house__furniture_text money">$20.000</div>
                    <div class="box-flex">
                        <div class="house__furniture_smallimage"></div>
                        <div class="house__furniture_text">10 Pine</div>
                    </div>
                    <div class="box-flex">
                        <div class="house__furniture_smallimage"></div>
                        <div class="house__furniture_text">10 Pine</div>
                    </div>
                    <div class="box-flex">
                        <div class="house__furniture_smallimage"></div>
                        <div class="house__furniture_text">10 Pine</div>
                    </div>
                </div>
                <div class="house__furniture_image"></div>
            </div>
            <div class="box-between">
                <div class="house__element_button">
                    <div class="houseicon-safe house__furniture_icon"></div>
                    Buy with Cash
                </div>
                <div class="house__element_button">
                    <div class="houseicon-garage house__furniture_icon"></div>
                    Craft yourself
                </div>
            </div>
        </div>
        <div class="house__furniture_element">
            <div class="box-between">
                <div class="box-column">
                    <div class="house__furniture_title">Lamp</div>
                    <div class="house__furniture_text money">$20.000</div>
                    <div class="box-flex">
                        <div class="house__furniture_smallimage"></div>
                        <div class="house__furniture_text">10 Pine</div>
                    </div>
                    <div class="box-flex">
                        <div class="house__furniture_smallimage"></div>
                        <div class="house__furniture_text">10 Pine</div>
                    </div>
                    <div class="box-flex">
                        <div class="house__furniture_smallimage"></div>
                        <div class="house__furniture_text">10 Pine</div>
                    </div>
                </div>
                <div class="house__furniture_image"></div>
            </div>
            <div class="box-between">
                <div class="house__element_button">
                    <div class="houseicon-safe house__furniture_icon"></div>
                    Buy with Cash
                </div>
                <div class="house__element_button">
                    <div class="houseicon-garage house__furniture_icon"></div>
                    Craft yourself
                </div>
            </div>
        </div>
        <div class="house__furniture_element">
            <div class="box-between">
                <div class="box-column">
                    <div class="house__furniture_title">Lamp</div>
                    <div class="house__furniture_text money">$20.000</div>
                    <div class="box-flex">
                        <div class="house__furniture_smallimage"></div>
                        <div class="house__furniture_text">10 Pine</div>
                    </div>
                    <div class="box-flex">
                        <div class="house__furniture_smallimage"></div>
                        <div class="house__furniture_text">10 Pine</div>
                    </div>
                    <div class="box-flex">
                        <div class="house__furniture_smallimage"></div>
                        <div class="house__furniture_text">10 Pine</div>
                    </div>
                </div>
                <div class="house__furniture_image"></div>
            </div>
            <div class="box-between">
                <div class="house__element_button">
                    <div class="houseicon-safe house__furniture_icon"></div>
                    Buy with Cash
                </div>
                <div class="house__element_button">
                    <div class="houseicon-garage house__furniture_icon"></div>
                    Craft yourself
                </div>
            </div>
        </div>
        <div class="house__furniture_element">
            <div class="box-between">
                <div class="box-column">
                    <div class="house__furniture_title">Lamp</div>
                    <div class="house__furniture_text money">$20.000</div>
                    <div class="box-flex">
                        <div class="house__furniture_smallimage"></div>
                        <div class="house__furniture_text">10 Pine</div>
                    </div>
                    <div class="box-flex">
                        <div class="house__furniture_smallimage"></div>
                        <div class="house__furniture_text">10 Pine</div>
                    </div>
                    <div class="box-flex">
                        <div class="house__furniture_smallimage"></div>
                        <div class="house__furniture_text">10 Pine</div>
                    </div>
                </div>
                <div class="house__furniture_image"></div>
            </div>
            <div class="box-between">
                <div class="house__element_button">
                    <div class="houseicon-safe house__furniture_icon"></div>
                    Buy with Cash
                </div>
                <div class="house__element_button">
                    <div class="houseicon-garage house__furniture_icon"></div>
                    Craft yourself
                </div>
            </div>
        </div>

    </div>
{/if}