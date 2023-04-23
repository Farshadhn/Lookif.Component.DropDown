export function SetOrUnsetInstance(dotNetHelper, identity, IsItSet) { 
    if (window.LFDropDowns == undefined)
        window.LFDropDowns = [];
    var obj = { "instance": dotNetHelper, "identity": identity };

    if (IsItSet) { 
        window.LFDropDowns.push(obj);
    }

    else { 
        removeItemOnce(window.LFDropDowns, identity);
    }



}

function removeItemOnce(arr, identity) { 
    window.LFDropDowns = window.LFDropDowns.filter(function (item) { return item.identity != identity })
}


$(document).click(function (e) { 
    if (!$(e.target).hasClass("DropDownContainer") && $(e.target).closest(".DropDownContainer").length === 0) {
        $.each(window.LFDropDowns, function (key, value) {
            value.instance.invokeMethodAsync('Toggle');

        }); 
        window.LFDropDowns = []

    }
});