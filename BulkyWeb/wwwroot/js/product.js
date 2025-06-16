var dataTable;
$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url:'/admin/product/getall'},
        "columns": [
            { data: 'title' },
            { data: 'isbn' },
            { data: 'author' },
            { data: 'price' },
            { data: 'category.name' },
            {
                data: 'id',
                "render": function (data) {
                    return `
                        <a href="/admin/product/upsert?id=${data}">Edit</a>
                        <a onclick="Delete('/admin/product/delete/${data}')">Delete</a>
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