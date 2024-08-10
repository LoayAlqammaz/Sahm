function addUser() {
    var userData = {
        name: $('#userName').val()
    };

    $.ajax({
        type: 'POST',
        url: '/Home/AddUser',
        data: userData,
        success: function (response) {
            if (response.success) {
                Swal.fire({
                    title: "Good job!",
                    text: response.message,
                    icon: "success"
                });
                location.reload();
            } else {
                Swal.fire({
                    title: "Error!",
                    text: response.message,
                    icon: "error"
                });
            }
        },
        error: function () {
            console.log("There is an Error");
        }
    });
    $('#addUserModal').modal('hide');
}

function editUser(userId) {
    $.ajax({
        type: 'GET',
        url: '/Home/GetUser',
        data: { userId: userId },
        success: function (user) {
            Swal.fire({
                title: 'Edit User Name',
                input: 'text',
                inputLabel: 'User Name',
                inputValue: user.Name,
                showCancelButton: true,
                confirmButtonText: 'Save Changes',
                cancelButtonText: 'Cancel',
                preConfirm: (newName) => {
                    if (!newName) {
                        Swal.showValidationMessage('Please enter a name');
                    }
                    return newName;
                }
            }).then((result) => {
                if (result.isConfirmed) {
                    saveEditUser(userId, result.value);
                }
            });
        },
        error: function () {
            console.log("Error fetching user data.");
        }
    });
}

function saveEditUser(userId, newName) {
    var userData = {
        userId: userId,
        name: newName
    };

    $.ajax({
        type: 'POST',
        url: '/Home/EditUser',
        data: userData,
        success: function (response) {
            if (response.success) {
                Swal.fire({
                    title: "Good job!",
                    text: response.message,
                    icon: "success"
                });
                location.reload();
            } else {
                Swal.fire({
                    title: "Error!",
                    text: response.message,
                    icon: "error"
                });
            }
        },
        error: function () {
            console.log("There is an Error");
        }
    });
}

function deleteUser(userId) {
    Swal.fire({
        title: 'Are you sure?',
        text: "This will permanently delete the user!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: 'No, cancel!',
    }).then((result) => {
        if (result.isConfirmed) {
            performDeleteUser(userId);
        }
    });
}

function performDeleteUser(userId) {
    $.ajax({
        type: 'POST',
        url: '/Home/DeleteUser',
        data: { userId: userId },
        success: function (response) {
            if (response.success) {
                Swal.fire({
                    title: "Deleted!",
                    text: response.message,
                    icon: "success"
                });
                location.reload();
            } else {
                Swal.fire({
                    title: "Error!",
                    text: response.message,
                    icon: "error"
                });
            }
        },
        error: function () {
            console.log("There is an Error");
        }
    });
}
