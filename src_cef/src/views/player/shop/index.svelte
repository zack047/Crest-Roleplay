<script>
    import './main.sass' 
    let isOwner = false;
    let isCurrentRent = false;
    let isNull = false;
    let isPlayer = true;
    let elementsList = [
        {
            name: "Boots",
            img: "",
            descr: "Гавно говна говну",
            price: 5000,
            count: 2,
            status: 55
        },
        {
            name: "Boots3",
            img: "",
            descr: "Гавно говна говну",
            price: 5000,
            count: 2,
        },
        {
            name: "Boots 2",
            img: "",
            descr: "Гавно говна говну",
            price: 5000,
        },
        {
            name: "Boots",
            img: "",
            descr: "Гавно говна говну",
            price: 5000,
            count: 2,
            status: 55
        },
        {
            name: "Boots3",
            img: "",
            descr: "Гавно говна говну",
            price: 5000,
            count: 2,
        },
        {
            name: "Boots 2",
            img: "",
            descr: "Гавно говна говну",
            price: 5000,
        },
        {
            name: "Ботинки",
            img: "",
            descr: "Гавно говна говну",
            price: 5000,
            count: 2,
            status: 55
        },
        {
            name: "Ботинки3",
            img: "",
            descr: "Гавно говна говну",
            price: 5000,
            count: 2,
        },
        {
            name: "Ботинки 2",
            img: "",
            descr: "Гавно говна говну",
            price: 5000,
        },
    ]
</script>
<div id="retail">
    <div class="retail__content">
        {#if isOwner == false && isCurrentRent == false && isPlayer == false}
            <div class="retail__content_left center">
                <div class="retail__image_lavka"></div>
                <div class="retail__title">Rent a tent</div>
                <div class="retail__content_flex">
                    <div class="gray">10 min</div>
                    <div class="green">500$</div>
                </div>
                <div class="retail__grid">
                    <div class="retail__grid_element active">5 min</div>
                    <div class="retail__grid_element">10 min</div>
                    <div class="retail__grid_element">15 min</div>
                    <div class="retail__grid_element">30 min</div>
                    <div class="retail__grid_element">1 hour</div>
                    <div class="retail__grid_element">2 hours</div>
                    <div class="retail__grid_element">3 hours</div>
                    <div class="retail__grid_element">4 hours</div>
                </div>
            </div>
            <div class="retail__content_right"></div>
        {/if}     
        {#if isOwner == true && isPlayer == false}
            <div class="retail__content_left">
                <div class="retail__content__header">
                    <div class="retail__image_lavka"></div>
                    <div class="box-column">
                        <div class="retail__title">Rent a tent</div>
                        <div class="retail__progress">
                            <div class="retail__progress_current" style="width: 30%"></div>
                        </div>
                        <div class="retail__time">25:00</div>
                        <div class="retail__subtitle">By the end of the lease</div>
                    </div>
                </div>
                <div class="box-between">
                    <div class="retail__button">Extend</div>
                    <div class="retail__button gray">Title</div>
                    <div class="retail__button red">Finish</div>
                </div>
                {#if isNull}
                    <div class="retail__title m-t72" style="text-align:center">This seems to be empty. You can put items for sale from the menu on the right</div>
                {/if}
                {#if isNull == false}
                    <div class="retail__sell_items m-t72">
                        {#each new Array(20) as item}
                            <div class="retail__content_element">
                                <div class="retail__element_title">Hiking boots</div>
                                <div class="box-flex">
                                    <div class="retail__item_box">
                                        <div class="retail__item_img"></div>
                                    </div>
                                    <div class="box-column">
                                        <div class="retail__info">
                                            <div class="retail__info_count green">5000000$</div>
                                            <div>Cost</div>
                                        </div>
                                        <div class="retail__info">
                                            <div class="retail__info_count">5 piece.</div>
                                            <div>Quantity</div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        {/each}
                    </div>
                {/if}
            </div>
            <div class="retail__content_right">
                <div class="retail__element_title">Inventory</div>
                <div class="retail__inventory">
                    {#each new Array(40) as item}
                        <div class="retail__item_box n-m">
                            <div class="retail__item_img"></div>
                        </div>
                    {/each}
                </div>
            </div>
        {/if}     
        {#if isPlayer == true && isOwner == false}
            <div class="box-column">
                <div class="retail__content__header">
                    <div class="retail__image_lavka"></div>
                    <div class="box-column">
                        <div class="retail__title">Tent #228</div>
                    </div>
                </div>
                <div class="retail__player_list">
                    {#each elementsList as item}
                        <div class="retail__player_element">
                            <div class="box-flex">
                                <div class="retail__item_box">
                                    <div class="retail__item_img"></div>
                                </div>
                                <div class="box-column">
                                    <div class="retail__buyelement_title">{item.name} 
                                        {#if item.status}
                                            <span class="orange">{item.status}%</span>
                                        {/if}
                                        {#if item.count}
                                            <span class="gray">{item.count} piece.</span>
                                        {/if}
                                    </div>
                                    <div class="retail__element_subtitle">{item.descr}</div>
                                </div>
                            </div>
                            <div class="box-flex">
                                <div class="price">{item.price}$</div>
                                <div class="retail__element_button">Buy</div>
                            </div>
                        </div>
                    {/each}
                </div>
            </div>
        {/if}
    </div>
    <div class="box-between">
        <div class="house_bottom_buttons box-flex back" style="opacity:0">
            <div>Select</div>
            <div class="house_bottom_button">Enter</div>
        </div>
        <div class="house_bottom_buttons box-flex back">
            <div>Rent</div>
            <div class="house_bottom_button">Enter</div>
        </div>
        <div class="house_bottom_buttons box-flex">
            <div>Log out</div>
            <div class="house_bottom_button">ESC</div>
        </div>
    </div>
</div>