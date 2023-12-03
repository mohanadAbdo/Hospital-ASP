﻿$(document).ready(function (){
    loadDataTable();

});

function loadDataTable() {
    dataTable = $('#tblData').DataTable( {
        "ajax": { url:'/admin/Doctor/getall' },
        "columns": [
            { data: 'doctorName', "width": "35%" },
            { data: 'category.name', "width": "35%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w—75 btn—group" role="group">
                    <a href="/admin/doctor/upsert?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i>Edit</a>
                    <a  href="/admin/doctor/delete/${data}" class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i>Delete</a>
                    </div>`
                }
            }
        ]
    });
}