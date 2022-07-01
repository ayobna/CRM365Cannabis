function OpenNewLicense(formContext) {
    debugger
    var name = formContext.getAttribute("new_name").getValue();
    var patientid = formContext.data.entity.getId();
    var entityFormOptions = {
        entityName:"new_license"
    };
    var formParameters = {
        new_amount: 145,
        new_patientid: patientid,   
        new_patientidname: name,
        new_patientidtype:"new_patientid",
    }
    Xrm.Navigation.openForm(entityFormOptions, formParameters);

}
