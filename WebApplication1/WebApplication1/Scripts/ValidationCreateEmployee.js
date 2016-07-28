function IsFirstNameEmpty()
{
    if (document.getElementById("txtFirstName").value == "") {
        return "FirstName should not be empty";
    }
    else {
        return "";
    }
}

function IsFirstNameInValid()
{
    if (document.getElementById("txtFirstName").value.indexOf("@") != -1) {
        return "FirstName should not contain @";
    }
    else {
        return "";
    }
}

function IsLastNameInValid()
{
    if (document.getElementById("txtLastName").value.length > 5) {
        return "LastName should not contain more than 5 characters";
    }
    else {
        return "";
    }
}

function IsSalaryEmpty()
{
    if (document.getElementById("txtSalary").value == "") {
        return "Salary should not be empty";
    }
    else {
        return "";
    }
}

function IsSalaryInValid()
{
    //if (isNan(document.getElementById("txtSalary").value)) {
    //    return "Enter invalid Salary";
    //}
    //else {
    //    return "";
    //}

    return "";
}

function IsValid()
{
    var FirstNameEmptyMessage = IsFirstNameEmpty();
    var FirstNameInValidMessage = IsFirstNameInValid();
    var LastNameInValidMessage = IsLastNameInValid();
    var SalaryEmptyMessage = IsSalaryEmpty();
    var SalaryInValidMessage = IsSalaryInValid();

    var FinalMessage = "Errors:";

    if (FirstNameEmptyMessage != "")
    {
        FinalMessage += "\n" + FirstNameEmptyMessage;
    }
    if (FirstNameInValidMessage != "") {
        FinalMessage += "\n" + FirstNameInValidMessage;
    }
    if (LastNameInValidMessage != "") {
        FinalMessage += "\n" + LastNameInValidMessage;
    }
    if (SalaryEmptyMessage != "") {
        FinalMessage += "\n" + SalaryEmptyMessage;
    }
    if (SalaryInValidMessage != "") {
        FinalMessage += "\n" + SalaryInValidMessage;
    }

    if (FinalMessage != "Errors:") {
        alert(FinalMessage);
        return false;
    }
    else {
        return true;
    }
}