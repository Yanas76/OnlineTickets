let users = [];

async function addItem() {

    let nameInput = document.getElementById("add-name");
    let emailAddressInput = document.getElementById("add-emailAddress");
    let phoneNumberInput = document.getElementById("add-phoneNumber");
    let dateOfBirthInput = document.getElementById("add-dateOfBirth");

    let newUser = {
        name: nameInput.value,
        emailAddress: emailAddressInput.value,
        phoneNumber: phoneNumberInput.value,
        dateOfBirth: dateOfBirthInput.value,
    };

    nameInput.value = "";
    emailAddressInput.value = "";
    phoneNumberInput.value = "";
    dateOfBirthInput.value = "";

    try {
        const response = await fetch('/api/users', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(newUser)
        });

        if (!response.ok) {
            throw new Error('Network response was not ok');
        }

        getUsers();

    } catch (error) {
        console.error('Unable to add user', error);
    }
}

async function editItem(id) {

    let nameInput = document.getElementById("edit-name");
    let emailAddressInput = document.getElementById("edit-emailAddress");
    let phoneNumberInput = document.getElementById("edit-phoneNumber");
    let dateOfBirthInput = document.getElementById("edit-dateOfBirth");

    let updatedUser = {
        id: id,
        name: nameInput.value,
        emailAddress: emailAddressInput.value,
        phoneNumber: phoneNumberInput.value,
        dateOfBirth: dateOfBirthInput.value
    };

    let idInput = document.getElementById("insert-id");
    idInput.value = "";

    nameInput.value = "";
    emailAddressInput.value = "";
    phoneNumberInput.value = "";
    dateOfBirthInput.value = "";

    try {

        const response = await fetch(`/api/users/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(updatedUser)
        })

        if (!response.ok) {
            throw new Error('Network response was not ok');
        }

        getUsers();
        
    } catch (error){
        console.error('Unable to edit user', error);
    }
    
}

async function deleteUser(userId) {

    try {

        let idInput = document.getElementById("delete-id");
        idInput.value = "";

        const response = await fetch(`/api/users/${userId}`, {
            method: 'DELETE',
        });

        if (!response.ok) {
            throw new Error('Network response was not ok');      
        }

        getUsers();

    } catch (error) {
        console.error(`Unable to delete user`, error);
    }
}


async function getUsers() {

    try {

        const response = await fetch('/api/users')
            .then(response => response.json())
            .then(data => {
                let rows = '';
                rows += '<tr> <td>Id </td>  <td>Name </td> <td>Email Address </td> <td>Phone Number </td> <td>Date of Birth </td> </tr>'
                data.forEach(user => {
                    rows += `<tr>
                   
                    <td>${user.id}</td>
                    <td>${user.name}</td>
                    <td>${user.emailAddress}</td>
                    <td>${user.phoneNumber}</td>
                    <td>${user.dateOfBirth}</td>

                 </tr>`;
                });
                document.querySelector('table tbody').innerHTML = rows;
                document.getElementById('counter').innerHTML = `Total Users: ${data.length}`;
            });

        if (!response.ok) {
            throw new Error('Network response was not ok');
        }

    } catch(error){
        console.error('Unable to display users', error);
    }
    
}

