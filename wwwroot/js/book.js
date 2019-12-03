function change_schedule(e, id, time, date, workerID, scheduleID) {

    Swal.fire({
        title: "Book?",
        text: "Are you sure you want to book this appointment?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, book it!'
    }).then((result) => {

        if (result.value) {
            $.ajax({
                method: "POST",
                url: "/Workers/BookAppointment",
                data: {
                    id: workerID,
                    scheduleID: scheduleID,
                    time: time,
                    date: date
                },
            }
            ).done(function (result) {

                if (result.success) {
                    Swal.fire({
                        title: 'Booked!',
                        text: 'Appointment scheduled for ' + date + 'at ' + time + '.',
                        type: 'success'
                    });
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


}