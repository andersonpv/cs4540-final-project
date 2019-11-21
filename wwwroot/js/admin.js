
function change_role(e, nameCB, userEmail, role) {
    console.log("in Change_role function");
    //set initial state.

    var addremove = "-1";

    if ($('#' + nameCB).prop('checked')) {
        addremove = "Add";
    } else {
        addremove = "Remove";
    }

    Swal.fire({
        title: addremove + ' Role?',
        text: 'Change ' + userEmail + "\'s role?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, change it!'
    }).then((result) => {

        if (result.value) {
            $.ajax({
                method: "POST",
                url: "/Admin/ChangeRole",
                data: {
                    Username: userEmail,
                    Role: role,
                    AddRemove: addremove
                },
            }
            ).done(function (result) {

                if (result.success) {
                    Swal.fire({
                        title: 'Role Changed!',
                        text: userEmail + '\'s role has Changed!',
                        type: 'success' });
                }
                else {
                    handleError(nameCB, userEmail, role, result.errorMessage);
                }

            }).fail(function (jqXHR, textStatus, errorThrown) {
                handleError(nameCB, userEmail, role, errorThrown);
            }).always(function (dataOrjqXHR, textStatus, jqXHRorErrorThrown) {
                console.log("action taken:" + result);
            });
        }
        else { //Canceled
            // undo 'checked' state
            if ($('#' + nameCB).prop('checked')) {
                $('#' + nameCB).prop('checked', false);
            }
            else {
                $('#' + nameCB).prop('checked', true);
            }
        }
    })

    console.log(e);
}

function handleError(nameCB, userEmail, role, error) {
    Swal.fire({
        type: 'error',
        title: 'Something went wrong!',
        html:
            userEmail + '\'s Role was not changed. <br >'+
            '' + error,                
    });
    // undo 'checked' state
    if ($('#' + nameCB).prop('checked')) {
        $('#' + nameCB).prop('checked', false);
    }
    else {
        $('#' + nameCB).prop('checked', true);
    }
}