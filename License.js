// JavaScript source code
var formContext;
var statusCode = {
    Approved: 100000002
};
var doctorType = { Requester: 1, Approver: 2 }


function onLoad(context) {
    formContext = context.getFormContext();
    registerEvents();
    setApprovedByStatus();
    getDoctor();
      formContext.ui.setFormNotification("200 נא לא להזין כמות גדולה מ", "INFO", "1");
}



function getDoctor() {
    
    if (formContext.getAttribute("statuscode").getValue() != statusCode.Approved) {

        formContext.getControl("statuscode").removeOption(statusCode.Approved)

        var userSettings = Xrm.Utility.getGlobalContext().userSettings;
        var userId = userSettings.userId;

        Xrm.WebApi.retrieveMultipleRecords("new_doctor", "?$select=new_tybe&$filter=new_systemuserid/systemuserid eq " + userId).then(
            function success(result) {
                if (result && result.entities && result.entities.length > 0) {
                    var doctor = result.entities[0];
                    ;
                    var option = {
                        text: "אושר",
                        value: statusCode.Approved
                    }
                    if (doctor.new_tybe == doctorType.Approver) {
                        formContext.getControl("statuscode").addOption(option)
                    }
                }
                // perform additional operations on retrieved records
            },
            function (error) {
                console.log(error.message);
                // handle error conditions
            }
        );
    }
}







function onStatusChange() {
    setApprovedByStatus();
}

function setApprovedByStatus() {
    var status = formContext.getAttribute("statuscode").getValue();

    if (status == statusCode.Approved) {
        formContext.getControl("new_approver").setVisible(true);
        formContext.getAttribute("new_approver").setRequiredLevel("required");
    } else {
        formContext.getControl("new_approver").setVisible(false);
        formContext.getAttribute("new_approver").setRequiredLevel("none");
    }
}















//getFormContext
function registerEvents() {
    formContext.getAttribute("statuscode").addOnChange(onStatusChange);
    formContext.getControl("new_approver").addPreSearch(function () {
        var filter = '<filter type="and">\
                        <condition attribute = "new_tybe" operator = "eq" value = "' + doctorType.Approver + '" />\
                      </filter>';

        formContext.getControl("new_approver").addCustomFilter(filter, "new_doctor");
    });

    formContext.getControl("new_requester").addPreSearch(function () {
        var filter = '<filter type="and">\
                        <condition attribute = "new_tybe" operator = "eq" value = "' + doctorType.Requester + '" />\
                      </filter>';

        formContext.getControl("new_requester").addCustomFilter(filter, "new_doctor");
    });
}








