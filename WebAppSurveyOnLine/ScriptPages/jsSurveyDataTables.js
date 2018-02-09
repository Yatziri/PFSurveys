/* [ ---- survey Admin - datatables ---- ] */

    $(document).ready(function() {
        //* datatables
        survey_datatables.basic();
        //survey_datatables.basic2();
        survey_datatables.hScroll();
        survey_datatables.colReorder_visibility();
        survey_datatables.table_tools();
        
        $('.dataTables_filter input').each(function() {
            $(this).attr("placeholder", "Buscar...");
        })
    });

    //* datatables
    survey_datatables = {
        basic: function() {
            if ($('#dt_Preguntas').length) {
                $('#dt_Preguntas').dataTable({
                    "bDestroy": true,
                    "oLanguage": {
                        "oPaginate": {
                            "sFirst": "Primero",
                            "sLast": "&Uacute;ltimo",
                            "sNext": "Siguiente",
                            "sPrevious": "Previo"
                            
                        },
                        "sEmptyTable": "Sin informaci&oacute;n",
                        "sZeroRecords": "No se encontr&oacute; informaci&oacute;n",
                        "sInfo": "Mostrando _START_ a _END_ de _TOTAL_ registros",
                    } 
                });
            }

            if ($('#tblEncuesta').length) {
                $('#tblEncuesta').dataTable({
                    "bDestroy": true,
                    "oLanguage": {
                        "oPaginate": {
                            "sFirst": "Primero",
                            "sLast": "&Uacute;ltimo",
                            "sNext": "Siguiente",
                            "sPrevious": "Previo"

                        },
                        "sEmptyTable": "Sin informaci&oacute;n",
                        "sZeroRecords": "No se encontr&oacute; informaci&oacute;n",
                        "sInfo": "Mostrando _START_ a _END_ de _TOTAL_ registros",
                    }
                });
            }
        },
        
        //* horizontal scroll
        hScroll: function() {
            if ($('#dt_tblRespuestas').length) {
                $('#dt_tblRespuestas').dataTable({
                "sScrollX": "100%",
                "sScrollXInner": '150%',
                "sPaginationType": "bootstrap",
                "bScrollCollapse": true 
            });
            }
        },
        //* column reorder & toggle visibility
        colReorder_visibility: function() {
            if($('#dt_colVis_Reorder').length) {
                $('#dt_colVis_Reorder').dataTable({
                    "sPaginationType": "bootstrap",
                    "sDom": "R<'dt-top-row'Clf>r<'dt-wrapper't><'dt-row dt-bottom-row'<'row-fluid'ip>",
                    "fnInitComplete": function(oSettings, json) {
                        $('.ColVis_Button').addClass('btn btn-mini btn-inverse').html('Columns');
                    }
                });
            }
        },
        //* column reorder & toggle visibility
        table_tools: function() {
            if($('#dt_table_tools').length) {
                $('#dt_table_tools').dataTable({
                    "sDom": "<'dt-top-row'Tlf>r<'dt-wrapper't><'dt-row dt-bottom-row'<'row-fluid'ip>",
                    "oTableTools": {
                        "aButtons": [
                            "copy",
                            "print",
                            {
                                "sExtends":    "collection",
                                "sButtonText": 'Save <span class="caret" />',
                                "aButtons":    [ "csv", "xls", "pdf" ]
                            }
                        ],
                        "sSwfPath": "~/js/lib/datatables/extras/TableTools/media/swf/copy_csv_xls_pdf.swf"
                    },
                    "fnInitComplete": function(oSettings, json) {
                        $(this).closest('#dt_table_tools_wrapper').find('.DTTT.btn-group').addClass('table_tools_group').children('a.btn').each(function(){
                            $(this).addClass('btn-mini btn-inverse');
                        });
                    }
                });
            }
        }
    };