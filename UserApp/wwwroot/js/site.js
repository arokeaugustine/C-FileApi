// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// $(document).ready(function() {
//     openModal(firstName, lastName, email, phoneNumber);
//     closeModal();
//     add();

// });

// function openModal(firstName, lastName, email, phoneNumber) {
//     $('#firstName').text(firstName);
//     $('#lastName').text(lastName);
//     $('#email').text(email);
//     $('#phoneNumber').text(phoneNumber);
//     $('#detailModal').modal('show');

// }

// function closeModal() {
//     $('#detailModal').modal('hide');
// }

// function add()
// {
//     var newuserModel = {
//         FirstName: $('#myfirstName').val(),
//         LastName: $('#mylastName').val(),
//         PhoneNumber: $('my#phoneNumber').val(),
//         Email: $('#myemail').val()
//         }

//     $.ajax({
//         async:true,
//         url: '/Users/Register',
//         type: 'POST',
//         contentType: 'application/json;charset=utf-8',
//         dataType: 'JSON',
//         data: JSON.stringify(newuserModel),
//         success: function(response) {
//             alert('added user');
//         },
//         error: function(){
//             alert('Error adding user');
//         }
//     });
// }

$(document).ready(function() {
    loadUsers();
   });
    function loadUsers() {
        $.ajax({
            async:true,
            url: 'Users/GetJson',
            type: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "JSON",
            success: function(response) {
                var html = '';
                $.each(response, function(key, value) {
                    html += '<tr>';
                    html += '<td>'+ value.FirstName + '</td>';
                    html += '<td>' + value.LastName + '</td>';
                    html += '<td>' + value.PhoneNumber + '</td>';
                    html += '<td>' + value.Email + '</td>';
                    html += '</tr>';

                });
                  $('.tbody').html(html)
            },
            error: function(){
                alert("There was an error getting the data from the server");
            }
            
        });
    }

    
function openModal(firstName, lastName, email, phoneNumber) {
    $('#firstName').text(firstName);
    $('#lastName').text(lastName);
    $('#email').text(email);
    $('#phoneNumber').text(phoneNumber);
    $('#detailModal').modal('show');

}

function closeModal() {
    $('#detailModal').modal('hide');
}


function openAdd(){
    $('#addModal').modal('show');
}



function addUser()
{
       
    $.ajax({
        // async:true,
        method: 'POST',
        url: '/Users/Register',
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        data: {
            FirstName: $('#myFirstName').val(),
            LastName : $('#myLastName').val(),
            PhoneNumber : $('#myPhoneNumber').val(),
            Email : $('#myEmail').val()
        },
        success: function(response) {
            alert('added user');
        },
        error: function(){
            alert('Error adding user');
        }


    });
    console.log(newuserModel);
}
console.log(newuserModel);