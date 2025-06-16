var dataTable;
$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url:'/admin/company/getall'},
        "columns": [
            { data: 'name' },
            { data: 'city' },
            { data: 'state' },
            { data: 'phoneNumber' },
            {
                data: 'id',
                "render": function (data) {
                    return `
                        <a href="/admin/company/upsert?id=${data}">Edit</a>
                        <a onclick="Delete('/admin/company/delete/${data}')">Delete</a>
                    `;
                }
            }
        ]
    });
}
function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'Delete',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    });
}