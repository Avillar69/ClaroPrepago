

//Redirecciona a una página
function OpenPage(page) {
    document.location.href = page;
}

//Abre una página en modo popup
function OpenPopup(page, width, height) {
    var IdPopup = 'Popup_' + Math.round(Math.random() * 100);
    var screenX = (window.screen.availWidth - width) / 2;
    var screenY = (document.body.offsetHeight - height) / 2;
    window.open(page, IdPopup, 'width=' + width + ', height=' + height + ', top=' + screenY + ', left=' + screenX + ', resizable=yes, scrollbars=yes');
}

//Cierra la página
function ClosePage() {
    self.close();
}

//Hace click a un objeto
function Click(obj) {
    document.getElementById(obj).click();
}

//Retorna a la pagina anterio
function BackPage() {
    window.history.go(-1);
}

// DataGrid
function GridView_OnMouseOver(Row) {
    Row.className = 'GridView_OnMouseOver';
}
function GridView_OnMouseOut(Row) {
    Row.className = Row.InitialStyle;
}

//Obtiene el valor de un parametro de la URL
function GetURLParam(strParamName) {
    //strParamName = strParamName.toLowerCase();
    var strReturn = "";
    var strHref = window.location.href;
    if (strHref.indexOf("?") > -1) {
        var strQueryString = strHref.substr(strHref.indexOf("?")); //.toLowerCase();
        var aQueryString = strQueryString.split("&");
        for (var iParam = 0; iParam < aQueryString.length; iParam++) {
            if (aQueryString[iParam].indexOf(strParamName + "=") > -1) {
                var aParam = aQueryString[iParam].split("=");
                strReturn = aParam[1];
                break;
            }
        }
    }
    return strReturn;
}
//Obtiene el valor de un parametro de la URL
function GetURLParam_Parent(strParamName) {
    //strParamName = strParamName.toLowerCase();
 
    var strReturn = "";
    var strHref = window.parent.location.href;
    if (strHref.indexOf("?") > -1) {
        var strQueryString = strHref.substr(strHref.indexOf("?")); //.toLowerCase();
        var aQueryString = strQueryString.split("&");
        for (var iParam = 0; iParam < aQueryString.length; iParam++) {
            if (aQueryString[iParam].indexOf(strParamName + "=") > -1) {
                var aParam = aQueryString[iParam].split("=");
                strReturn = aParam[1];
                break;
            }
        }
    }
    return strReturn;
}

//Selecciona un item de un data grid y lo coloca el valor 
//en un control hidden y el valor en un textbox de la 
//página padre
function SelectedItemPopup(Value, Text) {
    var ControlValue = GetURLParam('ControlValue');
    if (ControlValue != "")
        window.opener.document.getElementById(ControlValue).value = Value;

    var ControlText = GetURLParam('ControlText');
    if (ControlText != "")
        window.opener.document.getElementById(ControlText).value = Text;

    var CSAction = GetURLParam('CSAction');
    if (CSAction != "") {
        window.opener.document.getElementById(CSAction).click(); //ClickParent(CSAction);
    }

    var JSAction = GetURLParam('JSAction');
    if (JSAction != "") {
        eval("window.opener." + JSAction + "()");
    }

    var CheckCerrar = window.document.getElementById("CheckCerrar");
    if (CheckCerrar != null) {
        if (CheckCerrar.checked == true) {
            ClosePage();
        }
    }
    else {
        ClosePage();
    }
}