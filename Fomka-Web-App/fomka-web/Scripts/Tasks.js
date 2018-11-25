$(document).onload(() => {
    
});

function add(id, call) {
    var selection = $("#selection").val();
    var ids = selection.split(',');
    if (ids.length === 1 && ids[0]==="") {
        $("#selection").val(id);
    } else {
        $("#selection").val(selection + ',' + id);
    }
    submitSelection();
}

function remove(id,call) {
    var selection = $("#selection").val();
    var current = selection.split(',');
    var ids = current.filter(v => v != id);
    selection = ids[0];
    for (var i in ids) {
        if (i != 0) {
             selection += ','+ids[i];
        }
    }
    $("#selection").val(selection);
    submitSelection();
}

function submitSelection() {
    var selection = $("#selection").val();
    var taskId = $("#taskId").val();
    var blocks = $("#blocks").val();
    window.location.href = window.location.origin + '/Tasks/Select?taskId='+taskId+'&selection='+selection+'&blocks='+blocks;
}