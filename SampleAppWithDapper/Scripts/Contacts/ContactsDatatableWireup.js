// ======== Datatables wireup ========
$(document).ready(function () {
    //var existTheme = $('.right-sidebar .demo-choose-skin li.active').data('theme');
    //$.fn.dataTable.ext.classes.sLengthSelect = 'btn bg-' + existTheme; // Change Pagination Button Class
    //$.fn.dataTable.ext.classes.sPageButton = 'btn bg-' + existTheme; // Change Pagination Button Class
    //$.fn.dataTable.ext.classes.sPageButtonActive = 'btn bg-' + existTheme; // Change Pagination Button Class

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
    var datatableName = '#ContactsDashboard_ContactsDatatable';
    var dateFormat = 'DD.MM.YYYY HH:MM:SS';

    var operationsDatatable = $(datatableName).DataTable({
        dom: 'Bfrltip',
        colReorder: true,
        "columnDefs": [
            { "width": "5%", "visible": true, "targets": [0] }, // Id
            { "width": "10%", "searchable": true, "orderable": true, "targets": [2] }, // FirstName
            { "width": "10%", "searchable": true, "orderable": true, "targets": [3] }, // LastName
            { "width": "10%", "searchable": true, "orderable": true, "targets": [4] }, // TelephoneNumber_Entry
            { "width": "20%", "searchable": true, "orderable": true, "targets": [5] }, // EMail
            { "width": "20%", "searchable": false, "orderable": true, "targets": [6] }, // CreatedUtc
            { "width": "20%", "searchable": false, "orderable": true, "targets": [7] }, // ModifiedUtc
            { "className": "text-center custom-middle-align", "targets": [4, 6, 7] }
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
                //var start = $('#DateRangePickerOnOperationsStartHidden').val();
                //var end = $('#DateRangePickerOnOperationsEndHidden').val();
                //data.from = start;
                //data.to = end;
                //data.show_inactive = true;
                //data.show_deleted = false;
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
            { "data": "Id", "name": "Id", "autoWidth": true },
            { "data": "FirstName", "name": "FirstName", "autoWidth": true },
            { "data": "LastName", "name": "LastName", "autoWidth": true },
            { "data": "TelephoneNumber_Entry", "TelephoneNumber_Entry": "Active", "autoWidth": false },
            { "data": "EMail", "name": "EMail", "autoWidth": true },
            {
                "data": "CreatedUtc", "name": "CreatedUtc", "autoWidth": false, type: "datetime",
                render: function (data, type, row) {
                    return moment(data).format(dateFormat);
                }
            },
            {
                "data": "ModifiedUtc", "name": "ModifiedUtc", "autoWidth": false, type: "datetime",
                render: function (data, type, row) {
                    return moment(data).format(dateFormat);
                }
            },
            { "data": "Action" }
        ],
        "drawCallback": function (settings) {

        },
        "initComplete": function (settings, json) {

        }
    });

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
            //var min = $('#min-date').val();
            //var max = $('#max-date').val();
            //var createdAt = data[2] || 0; // Our date column in the table

            //if (
            //    (min == "" || max == "") ||
            //    (moment(createdAt).isSameOrAfter(min) && moment(createdAt).isSameOrBefore(max))
            //) {
            //    return true;
            //}
            //return false;
        }
    );

    //$('#my-table_filter').hide();

});

