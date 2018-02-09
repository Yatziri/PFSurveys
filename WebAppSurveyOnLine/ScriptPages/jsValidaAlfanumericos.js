function IsAlphaNumeric(e) {
    var keyCode = e.which ? e.which : e.keyCode
    var result = ((keyCode >= 65 && keyCode <= 90) ||   //Mayusculas
        (keyCode >= 97 && keyCode <= 122) ||   // Minusculas
        (keyCode == 45) ||   //Guion -
        (keyCode == 32) ||   //Espacio    
        (keyCode >= 48 && keyCode <= 58) ||   //Numeros
        (keyCode == 13)                          //Enter
    );
    return result;

}