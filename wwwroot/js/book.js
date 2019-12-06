function change_schedule(e, id, time, date, workerID, scheduleID) {

    e.preventDefault();

    Swal.fire({
        title: "Are you sure you want to book this appointment?",
        text: "Book for " + date + ' at ' + time + "?",
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

                    $('#' + id).prop('checked', true);
                    $('#' + id).prop("disabled", true);

                    Swal.fire({
                        title: 'Booked!',
                        text: 'Appointment scheduled for ' + date + ' at ' + time + '.',
                        type: 'success'
                    });
                }
                else {
                    handleError(id, result.errorMessage);
                }

            }).fail(function (jqXHR, textStatus, errorThrown) {
                
            });
        }
        else { //Canceled
            // undo 'checked' state
            if ($('#' + id).prop('checked')) {
                $('#' + id).prop('checked', false);
            }
            else {
                $('#' + nameCB).prop('checked', true);
            }
        }
    })


}