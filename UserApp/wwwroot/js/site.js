$(document).ready(function() {
    // loadUsers();
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
                console.log(response);
                $.each(response, function(key, value) {
                    html += '<tr>';
                    html += '<td>'+ value.firstName + '</td>';
                    html += '<td>' + value.lastName + '</td>';
                    html += '<td>' + value.phoneNumber + '</td>';
                    html += '<td>' + value.email + '</td>';  
                    var id = value.id;
                    var firstName = value.firstName;
                    var lastName = value.lastName;
                    var email = value.email;
                    var phoneNumber = value.phoneNumber;
                    let me ={id, firstName, lastName, email, phoneNumber};
                    html += '<td><input type="button" class="btn btn-primary" value="Details" onclick="viewUser('+id+')" /><input type="button" class="btn btn-secondary mx-1" value="Edit User" onclick="editUser('+me+')" /></td>' 
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
       alert($('input[name="FirstName"]').val());
       alert($('input[name="LastName"]').val());
       alert($('input[name="PhoneNumber"]').val());
       alert($('input[name="Email"]').val());
    $.ajax({
        // async:true,
        method: 'POST',
        // url: '/Users/Register',
        url: 'Users/RegisterJson',
        data: {
            FirstName: $('input[name="FirstName"]').val(),
            LastName : $('input[name="LastName"]').val(),
            PhoneNumber : $('input[name="PhoneNumber"]').val(),
            Email : $('input[name="Email"]').val()
        },
        success: function(response) {
            loadUsers();
            alert('added user');
        },
        error: function(){
            alert('Error adding user');
        }


    });
  }

function viewUser(id) {
    console.log(id);
    $.ajax({
        method: 'POST',
        url: '/Users/JsonReportDetails',
        data: {id:id},
        success:function(response) {
            console.log(response);
            $('#detailsFirstName').text(response.firstName);
            $('#detailsLastName').text(response.lastName);
            $('#detailsPhoneNumber').text(response.phoneNumber);
            $('#detailsEmail').text(response.email);

            $('#detailsModal').modal('show');
            
            
        },
        error: function(){
            console.log("Error getting the report");
        }
    });
}

function editUser(id,firstName,lastName,phoneNumber,email) {
    console.log(id);
    console.log(firstName);
    console.log(lastName);
    console.log(phoneNumber);
    console.log(email);

    $('#idInput').val(id);
    $('#firstNameInput').val(firstName);
    $('#lastNameInput').val(lastName);
    $('#phoneNumberInput').val(phoneNumber);
    $('#emailInput').val(email);
    $('#editModal').modal('show');   
}
function submitEdit(){
    console.log($('#idInput').val());
    console.log( $('#firstNameInput').val());
    console.log($('#lastNameInput').val());
    console.log($('#phoneNumberInput').val());
    console.log($('#emailInput').val());

    $.ajax({
        method: 'POST',
        url: '/Users/UpdateUser',
        data:{
            Id: $('#idInput').val(),
            FirstName: $('#firstNameInput').val(),
            LastName: $('#lastNameInput').val(),
            PhoneNumber: $('#phoneNumberInput').val(),
            Email: $('#emailInput').val(),
        },
        success: function (response) {
            console.log(response);
        },
        error: function(){
            console.log("Error updating user");
        }
    });
   
}

// let deleteid = 
function openDelete(id){
    $('#userToDelete').text(id);
    console.log($('#userToDelete').val());
    $('#deleteModal').modal('show');
}
let 