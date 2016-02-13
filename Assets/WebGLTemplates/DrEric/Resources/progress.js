function UnityProgress(dom) {
    var loading = document.getElementById("loading");
    var messages = document.getElementById("messages");
    var squid = document.getElementById("squid");
    var bsod = document.getElementById("bsod");

    this.progress = 0.0;
    this.message = "";
    this.dom = dom;

    SetFullscreen(1);

    this.SetProgress = function (progress) {
        this.progress = progress;
        if (Math.abs(progress - 0.5) > 0.001) {
            this.Update();
        }
    };

    this.SetMessage = function (message) {
        this.message = message;
        this.Update();
    };

    this.Clear = function () {
        setTimeout(function () {
            loading.style.display = "none";
            messages.style.display = "none";
            squid.style.display = "none";
            bsod.style.display = "none";
        }, 2000);
    };

    this.Update = function () {
        var percent = (this.progress * 100).toFixed(0);
        if (percent >= 97) {
            bsod.style.display = "block";
        }
        else {
            //loading.innerHTML = percent + "%";
            //messages.innerHTML = this.message;
            var scale = (percent * percent / 4);
            if (scale < 150)
                scale = 150;
            messages.innerHTML = percent + "%";
            squid.style.width =  scale + "px";
            squid.style.height = scale + "px";
        }
    };

    this.Update();
}