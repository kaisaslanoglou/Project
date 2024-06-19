
$(document).ready(function (){
	loadDataTable();
});

function loadDataTable() {
    var dataTable = $('#tblData').DataTable({
        "ajax": {
            url: '/admin/product/getall',
            error: function (xhr, error, code) {
                console.log("An error occurred while fetching data: ", error);
            }
        },        "columns": [
            { data: 'title', "width": "15%" },
            { data: 'description', "width": "15%" },
            { data: 'color', "width": "10%" },
            { data: 'size', "width": "15%" },
            { data: 'listPrice', "width": "10%" },
            { data: 'category.name', "width": "10%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
					<a href="/admin/product/createupdate?id=${data}" class="btn btn-primary mx-2"><i class="fa-solid fa-pen-to-square"></i> Edit </a>
					<a onClick="Delete('/admin/product/delete?id=${data}')" class="btn btn-danger mx-2"> <i class="fa-solid fa-trash"></i> Delete </a>
					</div>`
                },
               "width": "25%"
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
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        $('#tblData').DataTable().ajax.reload();
                        toastr.success(data.message);
                    } else {
                        toastr.error(data.message);
                    }
                },
                error: function (xhr, status, error) {
                    toastr.error("Error deleting product");
                }
            });
        }
    });
}