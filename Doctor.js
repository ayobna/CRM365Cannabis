// JavaScript source code
var formContext;
var lastDate;
function onLoad(context) {
    debugger
    formContext = context.getFormContext();
    registerEvents();
    lastDate= formContext.getAttribute('new_birthdate').getValue();
}
function registerEvents() {
    formContext.getAttribute('new_birthdate').addOnChange(datechange)
}

function datechange(){

    debugger
    //"new_doctor"
    //new_birthdate
    var  dateFieldValue = formContext.getAttribute('new_birthdate').getValue();
    if (dateFieldValue > Date.now()) {
        alert("Future birth date");
      //  formContext.getAttribute('new_birthdate').setValue(lastDate);
    }
    //new_systemsettings
    //new_name
    //doctorDate
    Xrm.WebApi.retrieveMultipleRecords("new_systemsettings", `?$select=new_value&$filter=new_name eq 'doctorDate'`).then(
        function success(result) {
            if (result && result.entities && result.entities.length > 0) {
                var min_age = result.entities[0].new_value;
            }
           
            if ((new Date().getFullYear() - dateFieldValue.getFullYear())<min_age) {
                alert("age not valid")
            }
            // perform additional operations on retrieved records
        },
        function (error) {
            debugger    
            console.log(error.message);
            // handle error conditions
        }
    );
 

}