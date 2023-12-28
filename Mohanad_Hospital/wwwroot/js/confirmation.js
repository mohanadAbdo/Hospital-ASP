﻿$(document).ready(function (){
    loadDataTable();

});

function loadDataTable() {
    dataTable = $('#tblData').DataTable( {
        "ajax": { url:'/admin/confirmation/getall' },
        "columns": [
            { data: 'id', "width": "5%" },
            { data: 'theName', "width": "20%" },
            { data: 'number', "width": "30%" },
            { data: 'applicationUser.email', "width": "50%" },
            { data: 'apoointmentStatus', "width": "15%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w—75 btn—group" role="group">
                    <a href="/admin/confirmation/details?confirmationId=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i></a>
                    </div>`
                }
            }
        ]
    });
}