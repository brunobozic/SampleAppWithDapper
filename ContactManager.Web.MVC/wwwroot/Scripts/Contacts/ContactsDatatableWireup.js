// ======== Datatables wireup ========
$(document).ready(function () {
    //// Setup - add a text input to each footer cell
    //$('#SearchResultTable tfoot th').each( function () {
    //    var title = $(this).text();
    //    $(this).html( '<input type="text" placeholder="Search '+title+'" />' );
    //});

    // Id
    // FirstName
    // LastName
    // TelephoneNumber_Entry
    // EMail
    // CreatedUtc
    // @* CreatedBy *@
    // ModifiedUtc
    // @* ModifiedBy *@

    try {
        var datatableName = '#ContactsDashboard_ContactsDatatable';
        var dateFormat = 'DD.MM.YYYY HH:MM:SS';

        var operationsDatatable = $(datatableName).DataTable({
            dom: 'Bfrltip',
            colReorder: true,
            "columnDefs": [
                { "width": "5%", "visible": true, "targets": [0] }, // Id
                { "width": "10%", "searchable": true, "orderable": true, "targets": [1] }, // FirstName
                { "width": "10%", "searchable": true, "orderable": true, "targets": [2] }, // LastName
                { "width": "10%", "searchable": true, "orderable": true, "targets": [3] }, // TelephoneNumber_Entry
                { "width": "20%", "searchable": true, "orderable": true, "targets": [4] }, // EMail
                { "width": "20%", "searchable": false, "orderable": true, "targets": [5] }, // CreatedUtc
                { "width": "20%", "searchable": false, "orderable": true, "targets": [6] }, // ModifiedUtc
                { "className": "text-center custom-middle-align", "targets": [4] },
                { "className": "daj_razmak", "targets": [0] }
            ],
            select: {
                style: 'multi'
            },
            buttons: [
                {
                    extend: 'copyHtml5',
                    title: 'Data Export'
                },
                {
                    extend: 'excelHtml5',
                    title: 'Data Export',
                    text: '<i class="fa fa-file-excel"></i>',
                    titleAttr: 'Excel'
                },
                {
                    extend: 'csvHtml5',
                    title: 'Data Export',
                    text: '<i class="fa fa-file-csv"></i>',
                    titleAttr: 'CSV'
                },
                {
                    extend: 'pdfHtml5',
                    title: 'Data Export',
                    text: '<i class="fa fa-file-pdf"></i>',
                    titleAttr: 'PDF'
                }
            ],
            "autoWidth": false,
            "keys": true,
            "fixedHeader": true,
            "sortable": true,
            processing: true,
            "serverSide": true,
            rowReorder: {
                selector: 'td:nth-child(2)'
            },
            responsive: true,
            "pageLength": 10,
            "pagingType": "full_numbers",
            "ajax": {
                url: datatablesConStringForContacts,
                type: 'POST',
                datatype: "json",
                "data": function (data) {
                    data.search_extra = $('#searchStringContactsMainGrid').val();
                }
            },
            "language": {
                "search": "",
                "searchPlaceholder": "Search...",
                loadingRecords:
                    '<div style="width:100%; z-index: 11000 !important; text-align: center;"><img src="http://www.snacklocal.com/images/ajaxload.gif"></div>',
                processing:
                    '<div style="width:100%; z-index: 11000 !important; text-align: center;"><img src="http://www.snacklocal.com/images/ajaxload.gif"></div>'
            },
            lengthMenu: [5, 10, 20, 50, 100, 200, 500, 1000],
            // Id
            // FirstName
            // LastName
            // TelephoneNumber_Entry
            // Mail
            // CreatedUtc
            // @* CreatedBy *@
            // ModifiedUtc
            // @* ModifiedBy *@
            "columns": [
                { "data": "id", "name": "id", "autoWidth": true },
                { "data": "firstName", "name": "firstName", "autoWidth": true },
                { "data": "lastName", "name": "lastName", "autoWidth": true },
                { "data": "telephoneNumber_Entry", "telephoneNumber_Entry": "Active", "autoWidth": false },
                { "data": "eMail", "name": "eMail", "autoWidth": true },
                {
                    "data": "createdUtc", "name": "createdUtc", "autoWidth": false, type: "datetimeoffset",
                    render: function (data, type, row) {
                        var stillUtc = moment.utc(data).toDate();
                        var local = moment(stillUtc).local().format('DD.MM.YYYY HH:mm:ss');
                        var minDate = moment.utc("0001-01-01"); // minimum value as per UTC

                        return moment(data).isAfter(minDate) ? local : "N/A";
                    }
                },
                {
                    "data": "modifiedUtc", "name": "modifiedUtc", "autoWidth": false, type: "datetimeoffset",
                    render: function (data, type, row) {
                        var stillUtc = moment.utc(data).toDate();
                        var local = moment(stillUtc).local().format('DD.MM.YYYY HH:mm:ss');

                        var minDate = moment.utc("0001-01-01"); // minimum value as per UTC

                        return moment(data).isAfter(minDate) ? local : "N/A";
                    }
                },
                { "data": "action" }
            ],
            "drawCallback": function (settings) {
            },
            "initComplete": function (settings, json) {
            }
        });
    } catch (e) {
        console.log(e);
    }

    $('#submitSearchContactsMainGrid').on('click', function (e) {
        operationsDatatable.draw();
    });

    $(document).ready(function () {
        $('#SearchResultTable_length').change(function () {
        });
    });

    // New record
    $('a.ContactsDashboard_ContactsDatatable_create').on('click', function (e) {
        e.preventDefault();
        var options = { keyboard: true, focus: true };
        $.ajax({
            type: "GET",
            url: insertUrl,
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (response) {
                $('#mdModal').modal(options);
                $('#mdModal').find('.modal-body').html("");
                $('#mdModal').modal('show');
                $('#mdModal').find('.modal-body').html(response);

                $('#mdModal').on('shown.bs.modal', function () {
                });
            },
            failure: function (response) {
                $('#mdModal').modal(options);
                $('#mdModal').find('.modal-body').html("");
                $('#mdModal').modal('show');
                $('#mdModal').find('.modal-body').html("");
                $('#mdModal').find('.modal-body').html("Problem loading your data...");

                $('#mdModal').on('shown.bs.modal', function () {
                });
            },
            error: function (response) {
                $('#mdModal').modal(options);
                $('#mdModal').find('.modal-body').html("");
                $('#mdModal').modal('show');
                $('#mdModal').find('.modal-body').html("");
                $('#mdModal').find('.modal-body').html("Problem loading your data...");

                $('#mdModal').on('shown.bs.modal', function () {
                });
            }
        });
    });

    // Edit record
    $('.btnGridEdit').on('click', function (e) {
        //e.preventDefault();

        var id = $(this).closest('tr').children('td:first').text();
        var options = { /*'backdrop': 'static',*/ keyboard: true, focus: true };
        var editUrl = '';

        alert(id);
    });

    // Delete a record
    $('btnGridDelete').on('click', function (e) {
        //e.preventDefault();

        var id = $(this).closest('tr').children('td:first').text();
        var options = { /*'backdrop': 'static',*/ keyboard: true, focus: true };
        var editUrl = '';

        alert(id);
    });

    $('#submitSearchContactsMainGrid').on('click', function (e) {
    });

    // Extend dataTables search
    $.fn.dataTable.ext.search.push(
        function (settings, data, dataIndex) {
        }
    );
});