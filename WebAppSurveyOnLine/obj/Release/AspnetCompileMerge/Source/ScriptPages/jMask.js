/*$(document).on('ready', function () {

  //  alert('ok');

    $('.dateMask').datepicker({
        //format: 'dd/mm/yyyy'  //Para bootstrap habilitar en: _LayoutMaster.cshtml lineas: 38 y 536
        dateFormat: 'dd/mm/yy'  //Para jQuery
    });

    $('.hourFormat').inputmask({
        alias: 'hh:mm', onincomplete: function () { $(this).val(''); }, oncomplete:
        function () {
            var values = $(this).val().split(':');
            var hour = Number(values[0]);
            // Horario de firma
            if (hour < 9 || hour >= 18) $(this).val('');
        }
    });
    
    $('.numerico').inputmask({ 'mask': '9999999999999999999999999999999999' });

    $('.monedaFormatNegativo').autoNumeric({ aSep: ',', aDec: '.', mDec: '2', wEmpty: '0.00', vMin: -999999999999.00, vMax: 999999999999.00 });
    $('.monedaFormat').autoNumeric({ aSep: ',', aDec: '.', mDec: '2', wEmpty: '', vMin: 0.00, vMax: 999999999999 });
    $('.cuentaClabeFormat').autoNumeric({ aSep: '', aDec: '.', mDec: '0', wEmpty: '', vMin: 0, vMax: 999999999999999999, lZero: 'keep' });
    $('.noCuentaFormat').autoNumeric({ aSep: '', aDec: '.', mDec: '0', wEmpty: '', vMin: 0, vMax: 999999999999, lZero: 'keep' });
    $('.noCreditoFormat').autoNumeric({ aSep: '', aDec: '.', mDec: '0', wEmpty: '', vMin: 0, vMax: 99999999999, lZero: 'keep' });
    $('.codigoPostalFormat').autoNumeric({ aSep: '', aDec: '.', mDec: '0', wEmpty: '', vMin: 0, vMax: 99999, lZero: 'keep' });
    $('.cuvFormat').autoNumeric({ aSep: '', aDec: '.', mDec: '0', wEmpty: '', vMin: 0, vMax: 9999999999999999, lZero: 'keep' });
    $('.edad').autoNumeric({ aSep: '', aDec: '.', mDec: '0', wEmpty: '', vMin: 0, vMax: 999, lZero: 'deny' });
    $('.numeroEscrituraFormat').autoNumeric({ aSep: '', aDec: '.', mDec: '0', vMin: 0, vMax: 9999999999, lZero: 'keep' });
    $('.anio').autoNumeric({ aSep: '', aDec: '.', mDec: '0', wEmpty: '', vMin: 0, vMin: 0, vMax: 9999, lZero: 'deny' });

    $('.folioBanco').autoNumeric({ aSep: '', aDec: '.', mDec: '0', wEmpty: '', vMin: 0, vMax: 99999999999999999999, lZero: 'keep' });
    $('.folioAvaluoSHF').autoNumeric({ aSep: '', aDec: '.', mDec: '0', wEmpty: '', vMin: 0, vMax: 99999999999999999999, lZero: 'keep' });
    $('.folioTransferencia').autoNumeric({ aSep: '', aDec: '.', mDec: '0', wEmpty: '', vMin: 0, vMax: 999999999999999, lZero: 'keep' });
    $('.porcentaje').autoNumeric({ aSep: '', aDec: '.', mDec: '6', wEmpty: '', vMin: 0.00, vMax: 999.999999 });
    $('.porcentajeCinco').autoNumeric({ aSep: '', aDec: '.', mDec: '2', wEmpty: '', vMin: 0.00, vMax: 999.99 });
    $('.porcentajeOcho').autoNumeric({ aSep: '', aDec: '.', mDec: '2', wEmpty: '', vMin: 0.00, vMax: 999999.99 });
    $('.porcentajeTiieFormat').autoNumeric({ aSep: '', aDec: '.', mDec: '4', wEmpty: '', vMin: 0.0000, vMax: 999.9999 });
    $('.porcentajeSieteDec').autoNumeric({ aSep: '', aDec: '.', mDec: '7', wEmpty: '', vMin: -999.9999999, vMax: 9999.9999999 });
    $('.porcentajeCat').autoNumeric({ aSep: '', aDec: '.', mDec: '1', wEmpty: '', vMin: 0.0, vMax: 999.9 });

    $('.numeros').autoNumeric({ aSep: '', aDec: '.', mDec: '0', wEmpty: '', vMin: 0, vMax: 999999999999999999, lZero: 'keep' });
    $('.numerosCC').autoNumeric({ aSep: '', aDec: '.', mDec: '0', wEmpty: '', vMin: 0, vMax: 99999999999999999999, lZero: 'keep' });
    //$('.numeros').inputmask({ 'mask': '999999999999999999999999' });
    $('.numeroTelefono').autoNumeric({ aSep: '', aDec: '.', mDec: '0', wEmpty: '', vMin: 0, vMax: 9999999999, lZero: 'keep' });
    $('.diaCorte').autoNumeric({ aSep: ',', aDec: '.', mDec: '2', wEmpty: '0.00', vMin: 1, vMax: 31 });
    $('.rfcMask').inputmask({ 'mask': 'aaaa999999***' });
    $('.rfcMoral').inputmask({ 'mask': 'aaa999999***' });
    $('.curpMask').inputmask({ 'mask': 'aaaa999999aaaaaa**' });
    $('.nssMask').inputmask({ 'mask': '99999999999' });
    $('.TelExtMask').inputmask({ 'mask': '9999999999/Ext.9999999' });
    $('.dateVigencia').inputmask('99/99/9999', { placeholder: "dd/mm/yyyy" });

    $(".numerosGuionesMask").keypress(function (e) {
        var code = e.charCode || e.keyCode;
        if (!String.fromCharCode(code).match(/^[-()0123456789]+$/g)) {
            switch (code) {
                case 8:  // Backspace
                case 9:  // Tab
                case 13: // Enter
                case 35: // Inicio
                case 36: // Fin
                case 37: // Left
                case 38: // Up
                case 39: // Right
                case 40: // Down
                case 46: // Supr
                    break;
                default:
                    return false;
            }
        }
    });

    $.datepicker.regional['es'] = {
        closeText: 'Cerrar',
        prevText: '< Ant',
        nextText: 'Sig >',
        currentText: 'Hoy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
        dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
        dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        weekHeader: 'Sm',
        dateFormat: 'dd/mm/yy',
        firstDay: 1,
        isRTL: false,
        showMonthAfterYear: false,
        yearSuffix: ''
    };

    $.datepicker.setDefaults($.datepicker.regional['es']);

});

// handler para knockoutJs montos con comas
function autoNumericKnockout() {
    ko.bindingHandlers.autoNumeric = {
        init: function (el, valueAccessor, bindingsAccessor, viewModel) {
            var $el = $(el),
                bindings = bindingsAccessor(),
                settings = bindings.settings,
                value = valueAccessor();

            $el.autoNumeric(settings);
            $el.autoNumeric('set', parseFloat(ko.utils.unwrapObservable(value()), 10));
            $el.change(function () {
                value(parseFloat($el.autoNumeric('get'), 10));
            });
        },
        update: function (el, valueAccessor, bindingsAccessor, viewModel) {
            var $el = $(el),
                newValue = ko.utils.unwrapObservable(valueAccessor()),
                elementValue = $el.autoNumeric('get'),
                valueHasChanged = (newValue != elementValue);

            if ((newValue === 0) && (elementValue !== 0) && (elementValue !== "0")) {
                valueHasChanged = true;
            }

            if (valueHasChanged) {
                $el.autoNumeric('set', newValue);
            }
        }
    };
}*/