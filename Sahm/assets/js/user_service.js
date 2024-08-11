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

//function MakePayment() {
//    var userData = {
//        userId: $('#userId').val(),
//        amount: $('#selectedAmountInput').val()
//    }
//    $.ajax({
//        type: 'post',
//        url: '/Home/MakePayment',
//        data: userData,
//        success: function () {
//            Swal.fire({
//                title: "Good job!",
//                text: "payment successfully completed",
//                icon: "success"
//            });
//        },
//        error: function () {
//            console.log("There is an Error ")
//        }
//    })
//}

function showPaymentOptions() {
    // Show the payment options modal
    var paymentModal = new bootstrap.Modal(document.getElementById('paymentOptionsModal'));
    paymentModal.show();
}


function MakePayment(paymentMethod) {
    var amount = $('#selectedAmountInput').val();

    // Validate the amount
    if (!amount || isNaN(amount) || Number(amount) <= 0) {
        Swal.fire({
            title: "Error!",
            text: "Please enter a valid amount.",
            icon: "error"
        });
        return;
    }

    var userData = {
        userId: $('#userId').val(),
        amount: amount,
        paymentMethod: paymentMethod 
    };

    $.ajax({
        type: 'post',
        url: '/Home/MakePayment',
        data: userData,
        success: function () {
            Swal.fire({
                title: "Good job!",
                text: "Payment successfully completed",
                icon: "success"
            });
            var paymentModal = bootstrap.Modal.getInstance(document.getElementById('paymentOptionsModal'));
            if (paymentModal) {
                paymentModal.hide();
            }
        },
        error: function () {
            console.log("There is an Error ");
            Swal.fire({
                title: "Error!",
                text: "There was an error processing your payment.",
                icon: "error"
            });
        }
    });
}

function showCreditCardModal() {
    var creditCardModal = new bootstrap.Modal(document.getElementById('creditCardModal'));
    creditCardModal.show();
}
// Function to format credit card number input
function formatCreditCardNumber(input) {
    const value = input.value.replace(/\D/g, ''); // Remove all non-digit characters
    const formattedValue = value.replace(/(\d{4})(?=\d)/g, '$1 '); // Add a space after every 4 digits
    input.value = formattedValue;
}

document.getElementById('creditCardNumber').addEventListener('input', function (e) {
    formatCreditCardNumber(this);
});

function validateCreditCardForm() {
    var form = document.getElementById('creditCardForm');

    if (form.checkValidity()) {
        var expiryDate = new Date($('#expiryDate').val() + "-01");
        var currentDate = new Date();
        currentDate.setDate(1);

        if (expiryDate < currentDate) {
            Swal.fire({
                title: "Error!",
                text: "The expiry date cannot be in the past.",
                icon: "error"
            });
            return;
        }

        var cardholderName = $('#cardholderName').val();
        var cardNumber = $('#creditCardNumber').val();
        var cvv = $('#cvv').val();

        // Display success message
        Swal.fire({
            title: "Payment Successful!",
            text: "Thank you, " + cardholderName + ". Your payment has been processed successfully.",
            icon: "success"
        });

        // Optionally, close the modal
        var creditCardModal = bootstrap.Modal.getInstance(document.getElementById('creditCardModal'));
        if (creditCardModal) {
            creditCardModal.hide();
        }

        // Reset the form for the next input
        form.reset();
        form.classList.remove('was-validated');
    } else {
        // Show validation feedback
        form.classList.add('was-validated');
    }
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
