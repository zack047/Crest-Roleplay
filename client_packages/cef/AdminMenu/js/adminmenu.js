var AdminMenu = new Vue({
    el: "#admin",
    data: {
        enable: true,
        page: 0,
        main: {
            inputGlobal: "",
        },
        player: {
            inputId: null,
            inputMessage: "",
            inputDim: null,
            inputPunishTime: null,
            inputPubishReason: "",
        },
    },
    methods: {
        Open() {
            this.enable = true;
            this.page = 0
            mp.trigger("openAdminPanel");
            mp.events.callRemote("openAdminPanel");
        },
        changePage(index) {
            this.page = index;
        },
        useCommand(commandName) {
            mp.invoke("command", commandName);
        },
        Close() {
            this.enable = false;
            mp.trigger("closeAdminPanel");
            mp.events.callRemote("closeAdminPanel");
        }
    }
});