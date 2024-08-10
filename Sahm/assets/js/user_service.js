function showDiv(divId) {
    document.getElementById('payDiv').style.display = 'none';
    document.getElementById('requestCarDiv').style.display = 'none';
    document.getElementById('evaluationDiv').style.display = 'none';
    document.getElementById(divId).style.display = 'block';
}

function selectEmoji(element) {
    document.querySelectorAll('.emoji').forEach(emoji => {
        emoji.classList.remove('selected');
    });
    element.classList.add('selected');
    document.getElementById('SelectedEmoji').value = element.getAttribute('data-value');
}

function MakePayment() {
    var userData = {
        userId: $('#userId').val(),
        amount: $('#amount').val()
    }
    $.ajax({
        type: 'post',
        url: '/Home/MakePayment',
        data: userData,
        success: function () {
            Swal.fire({
                title: "Good job!",
                text: "payment successfully completed",
                icon: "success"
            });
        },
        error: function () {
            console.log("There is an Error ")
        }
    })
}

function RequestCar() {
    var userData = {
        userId: $('#userId').val(),
    }
    $.ajax({
        type: 'post',
        url: '/Home/RequestCar',
        data: userData,
        success: function () {
            Swal.fire({
                title: "Good job!",
                text: "Car Requested!",
                icon: "success"
            });
        },
        error: function () {
            console.log("There is an Error ")
        }
    })
}


function Evaluate() {
    var userData = {
        userId: $('#userId').val(),
        comment: $('#comment').val(),
        Emoji: $('#SelectedEmoji').val(),
    }
    $.ajax({
        type: 'post',
        url: '/Home/Evaluate',
        data: userData,
        success: function () {
            Swal.fire({
                title: "Good job!",
                text: "successfully completed",
                icon: "success"
            });
        },
        error: function () {
            console.log("There is an Error ")
        }
    })
}
