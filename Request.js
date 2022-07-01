var formContext;

var RequestStatus= {
    New: 1,
    InTreatment: 2,
    EndOfTreatment: 3
};

function onLoad(context) {
    formContext = context.getFormContext();
    TimeRequest();
}



function TimeRequest() {
    debugger
    var new_requestid = formContext.getAttribute("new_requestnumber").getValue()
    debugger
    if (new_requestid!=null && formContext.getAttribute("new_status").getValue()!= RequestStatus.EndOfTreatment) {
        var deadline = formContext.getAttribute("new_slaend").getValue()
        var dateNow = Date.now()
        if (deadline < dateNow) {

            formContext.ui.setFormNotification(" הזמן עבר", "INFO", "1");
        }
        else {     
            formContext.ui.setFormNotification(` הזמן לא עבר נשאר  ${ Math.round((deadline - dateNow) / (1000 * 60 * 60 * 24))}  יום `, "INFO", "1");
        }

    }    
}