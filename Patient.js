// JavaScript source code
var formContext;
var statusCode = {
    Approved: 100000002
};

function onLoad(context) {
    formContext = context.getFormContext();
    
    SumAmount()
  
}



function SumAmount() {
    var id = formContext.data.entity.getId();

    
    fetchXml = "<fetch distinct='false' mapping='logical' output-format='xml - platform' version='1.0'>" +
        " <entity name='new_license'>" +

        "<attribute name='new_number' />" +
        " <attribute name='new_treatmenttype'/>" +
        "<attribute name='statuscode'/>" +
        "<attribute name='new_amount'/>" +
        "<attribute name='new_licenseid'/>" +
        "  <order descending='false' attribute='new_number'/>" +
        "    <filter type='and' >" +
        "<condition attribute='statuscode' value='" + statusCode.Approved + "' operator='eq'/>" +
        " <condition attribute='new_patientid' value='" + id + "' uitype='new_patient' uiname='HWUser' operator='eq'/>" +
        "    </filter>" +
        "  </entity>" +
        "</fetch>";

    fetchXml = "?fetchXml=" + encodeURIComponent(fetchXml);
    var sumAmount = 0
    Xrm.WebApi.retrieveMultipleRecords("new_license", fetchXml).then(
        function success(result) {
            
            
            if (result && result.entities && result.entities.length > 0) {
                for (var i = 0; i < result.entities.length ; i++) {
                   
                    var test = result.entities[i];
               
                    sumAmount+= test.new_amount  
                   
                }
            }  
            
            formContext.ui.setFormNotification(" Approved " + sumAmount + " grams", "INFO", "1");
        },
        function (error) {
            
            console.log(error.message);
            // handle error conditions
        }
    );
  
}



