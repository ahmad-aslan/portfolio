function confirmDelete(id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            deleteProduct(id);
        }
    });
}

function deleteProduct(id) {
    $.ajax({
        url: '/admin/social/delete?id=' + id,
        type: 'DELETE',
        success: function (response) {
            if (response.success) {
                Swal.fire(
                    'Deleted!',
                    response.message,
                    'success'
                ).then(() => {
                    location.reload();
                });
            } else {
                Swal.fire(
                    'Error!',
                    response.message,
                    'error'
                );
            }
        },
        error: function () {
            Swal.fire(
                'Error!',
                'An error occurred while deleting the product.',
                'error'
            );
        }
    });
}